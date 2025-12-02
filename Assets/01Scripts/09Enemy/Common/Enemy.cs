using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 敌人基类
/// 敌人有两种，近程和远程
/// </summary>
public class Enemy : MonoBehaviour, IEnemyHurt
{
    [Header("怪物数据")]
    public EnemyData_SO Data;
    [Header("关于任务的敌人")]
    public TaskEnum TaskEnemy;

    #region 组件
    public Rigidbody2D rig { get; private set; }
    public Animator ani { get; private set; }
    #endregion

    #region 基本状态机
    public EnemyStateMachine state;
    public EnemyIdleState IdleState { get; set; }
    public EnemyPatrolState PatrolState { get; set; }
    public EnemyChaseState ChaseState { get; set; }
    public EnemyReactState ReactState { get; set; }
    public EnemyAttackState AttackState { get; set; }
    public EnemyHurtState HurtState { get; set; }
    public EnemyDieState DieState { get; set; }

    #endregion

    #region 状态

    public Transform Center;//怪物中心位置
    public float MaxHeathl { get; set; }//最大血量
    private float _CurrentHeathl;
    public float CurrentHeathl
    {
        get => _CurrentHeathl;
        set
        {
            _CurrentHeathl = value;
            BleedSlider.value = _CurrentHeathl / MaxHeathl;
        }
    }
    public Vector3 Position => transform.position;//位置
    public bool IsFaceRight { get; private set; }//是否面朝右边
    public int Dir { get; set; }//方向
    public bool IsAttack { get; set; }//是否正在攻击
    #endregion

    public Transform LineCheckPos;//检测前方是否有墙
    public Transform GroundCheckPos;//检测前方是否有落差
    public Slider BleedSlider;//血条
    public bool IsDie { get; private set; }//是否死亡
    protected virtual void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        ani = GetComponentInChildren<Animator>();
        if (ani == null)
        {
            ani = GetComponent<Animator>();
        }
        state = new EnemyStateMachine();
        MaxHeathl = Data.MaxHeathl;
        CurrentHeathl = MaxHeathl;
        BleedSlider.value = CurrentHeathl / MaxHeathl;
        IsFaceRight = true;
        Dir = 1;
        IsCanHurt = true;
        InitState();
    }
    protected virtual void InitState()
    {
        //状态机初始化
    }
    public virtual void Update()
    {
        state.CurrentState.FrameUpdate();
    }
    private void FixedUpdate()
    {
        state.CurrentState.FixUpdate();
    }

    /// <summary>
    /// 检测墙
    /// </summary>
    /// <returns>是否检测的到</returns>
    public bool CheckWall()
    {
        RaycastHit2D hit;
        hit = Physics2D.Raycast(LineCheckPos.position, transform.right, 0.5f, LayerMask.GetMask("Ground"));
        if (hit.collider != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    /// <summary>
    /// 检测前方是否是地面
    /// </summary>
    /// <returns></returns>
    public bool CheckGround()
    {
        RaycastHit2D hit;
        hit = Physics2D.Raycast(GroundCheckPos.position, -transform.up, 0.2f, LayerMask.GetMask("Ground"));
        if (hit.collider != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    /// <summary>
    /// 检测玩家
    /// </summary>
    /// <returns></returns>
    public GameObject CheckPlayer()
    {
        RaycastHit2D hit;
        hit = Physics2D.Raycast(transform.position, transform.right, Data.CheckRadius, LayerMask.GetMask("Player"));
        if (hit.collider != null)
        {
            //检测到玩家
            return hit.collider.gameObject;
        }
        else
        {
            return null;
        }
    }
    /// <summary>
    /// 检测是否可以攻击
    /// </summary>
    /// <returns>是否</returns>
    public virtual bool CheckIsCanAttack()
    {
        return true;
    }
    //转向
    public void Flip(bool IsFlipRight)
    {
        if (IsFlipRight && !IsFaceRight)
        {
            IsFaceRight = true;
            Dir = 1;
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            BleedSlider.transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        else if (!IsFlipRight && IsFaceRight)
        {
            IsFaceRight = false;
            Dir = -1;
            transform.localRotation = Quaternion.Euler(0, 180, 0);
            BleedSlider.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }
    /// <summary>
    /// 设置X方向速度
    /// </summary>
    /// <param name="value"></param>
    public void SetVelocityX(float value)
    {
        rig.velocity = new Vector2(value, rig.velocity.y);
    }
    /// <summary>
    /// 设置Y方向速度
    /// </summary>
    /// <param name="value"></param>
    public void SetVelocityY(float value)
    {
        rig.velocity = new Vector2(rig.velocity.x, value);
    }
    public SpriteRenderer EnemySpriteRenderer()
    {
        return GetComponent<SpriteRenderer>() == null ? GetComponentInChildren<SpriteRenderer>() : GetComponent<SpriteRenderer>();
    }
    #region 受伤
    protected bool IsCanHurt { get; set; }
    public virtual void Hurt(Transform pos, float Value)
    {
        if (!IsCanHurt)
            return;
        IsCanHurt = false;
        TimerSystem.Instance.AddTask(0.25f, () => { IsCanHurt = true; });
        CurrentHeathl -= Value;
        if (CurrentHeathl <= 0)
        {
            HurtToDie();
        }
        else
        {
            HurtToBack(pos);
        }
    }

    protected virtual void HurtToDie()
    {
        //死亡
        Destroy(BleedSlider.gameObject);
        state.ChangeState(DieState);
        if (Data.HitEffect != null)
            Instantiate(Data.HitEffect, Center.position, Quaternion.identity);
    }
    protected virtual void HurtToBack(Transform pos)
    {
        //受伤
        state.ChangeState(HurtState);
        if (Data.HitEffect != null)
            Instantiate(Data.HitEffect).transform.position = Center.position;
        Vector2 Back = (transform.position - pos.position).normalized;
        Back = new Vector2(Back.x * 100, 30);
        rig.AddForce(Back, ForceMode2D.Impulse);
    }
    #endregion

    #region 死亡
    /// <summary>
    /// 死亡
    /// </summary>
    public void Die()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        rig.simulated = false;
        IsDie = true;
        ResService.Instance.CreatCoin(Data.FallCoin, Center.position);
        TaskSystem.Instance.SubmitTask(TaskEnemy);
        TimerSystem.Instance.AddTask(1, () =>
        {
            Destroy(gameObject);
            int Value = Random.Range(10, Data.MaxEXP);
            PlayerSystem.Instance.CurrentEXP += Value;
            TimerSystem.Instance.AddTask(0.2f, () =>
            {
                LevelSystem.Instance.CheckIsHaveEnemy();
            });

        });
    }
    #endregion

    /// <summary>
    /// 动画事件
    /// </summary>
    public void AniEvent()
    {
        state.CurrentState.AniEvent();
    }
}
