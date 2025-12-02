using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家控制脚本
/// </summary>
public class Player : MonoBehaviour, IFriendHurt
{
    #region 组件

    public Rigidbody2D rig { get; private set; }
    public Animator ani { get; private set; }

    #endregion 组件

    #region 状态机

    public PlayerStateMachine state;
    public PlayerIdleState IdleState;
    public PlayerRunState RunState;
    public PlayerJumpUpState JumpUpState;
    public PlayerJumpDownState JumpDownState;
    public PlayerAttackState AttackState;
    public PlayerHeavyAttackState HeavyAttackState;
    public PlayerPauseState PauseState;
    public PlayerHurtState HurtState;
    public PlayerDieState DieState;
    public PlayerDashState DashState;

    #endregion 状态机

    #region 跳跃

    public Transform JumpCheckTrans;//跳跃检测点
    public float JumpCheckRadius;//跳跃检测范围
    public bool IsOnGround { get; private set; }//是否在地面上
    public bool IsFalling { get; private set; }//是否正在下落

    #endregion 跳跃

    #region 状态标志

    public bool IsCanDash { get; set; }//是否可以翻滚
    public bool IsCanAirAttack { get; set; }//是否可以空中攻击
    private bool IsInit;
    private bool IsFaceRight = true;//是否朝向右边
    public int Dir => IsFaceRight ? 1 : -1;//方向

    public bool IsDash { get; set; }//是否再冲刺

    #endregion 状态标志

    #region 攻击

    public List<PlayerAttackData> AttackDatas = new List<PlayerAttackData>();
    public PlayerAttackData HeavyAttackData = new PlayerAttackData();

    #endregion 攻击

    public void Init()
    {
        //获取组件
        rig = GetComponent<Rigidbody2D>();
        ani = GetComponentInChildren<Animator>();
        state = new PlayerStateMachine();
        //初始化状态
        InitState();
        state.Init(IdleState);
        //初始化各种标志信息
        IsCanDash = true;
        IsCanAirAttack = true;
        IsCanHurt = true;
        IsInit = true;
    }

    //初始化状态
    public void InitState()
    {
        IdleState = new PlayerIdleState(this, state, "Idle");
        RunState = new PlayerRunState(this, state, "Run");
        JumpUpState = new PlayerJumpUpState(this, state, "JumpUp");
        JumpDownState = new PlayerJumpDownState(this, state, "JumpDown");
        AttackState = new PlayerAttackState(this, state, "Attack");
        HeavyAttackState = new PlayerHeavyAttackState(this, state, "HeavyAttack");
        PauseState = new PlayerPauseState(this, state, "Idle");
        HurtState = new PlayerHurtState(this, state, "Hurt");
        DieState = new PlayerDieState(this, state, "Die");
        DashState = new PlayerDashState(this, state, "Dash");
    }

    //如果游戏没有暂停，则每帧更新状态逻辑
    private void Update()
    {
        if (!IsInit)
            return;
        if (GameRoot.Instance.IsPause)
            return;
        state.CurrentState.FrameUpdate();
    }

    //更新物理逻辑
    private void FixedUpdate()
    {
        if (!IsInit)
            return;
        state.CurrentState.FixUpdate();
        //检测是否在地面上
        IsOnGround = Physics2D.OverlapCircle(JumpCheckTrans.position, JumpCheckRadius, LayerMask.GetMask("Ground"));
        //检测是否在下落
        IsFalling = rig.velocity.y < -0.1f;
    }

    //播放动画
    public void PlayAni(string AniName)
    {
        ani.Play(AniName);
    }

    //人物进入暂停状态
    public void PausePlayer()
    {
        state.ChangeState(PauseState);
    }

    //人物进入继续状态
    public void ContinuePlayer()
    {
        state.ChangeState(IdleState);
    }

    /// <summary>
    /// 转向
    /// </summary>
    /// <param name="Right">是否朝右</param>
    public void Flip(bool Right)
    {
        if (Right && !IsFaceRight)
        {
            IsFaceRight = true;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (!Right && IsFaceRight)
        {
            IsFaceRight = false;
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    //设置X方向速度
    public void SetVelocityX(int Value)
    {
        rig.velocity = new Vector2(Value, rig.velocity.y);
    }

    //设置Y方向速度
    public void SetVelocityY(int Value)
    {
        rig.velocity = new Vector2(rig.velocity.x, Value);
    }

    //设置速度
    public void SetVelocity(Vector2 Value)
    {
        rig.velocity = Value;
    }

    //动画事件
    public void AniEvent()
    {
        state.CurrentState.AniEvent();
    }

    //是否可以受伤，受伤后会有一段时间无敌
    private bool IsCanHurt;

    //实现受伤接口
    public void Hurt(Transform pos, float Value)
    {
        //如果不能受伤或者玩家处于无敌，则返回
        if (!IsCanHurt || PlayerSystem.Instance.IsImmunity || IsDash)
            return;
        AttackEffect.Instance.StopFrame(12);
        AudioService.Instance.PlayEffect("击打");
        //扣血
        PlayerSystem.Instance.CurrentHeathl -= Value;
        if (PlayerSystem.Instance.CurrentHeathl <= 0)
        {
            Die();
            return;
        }
        //改变状态
        state.ChangeState(HurtState);
        //计算击退向量
        Vector2 BackDir = transform.position - pos.position;
        BackDir = new Vector2(BackDir.normalized.x * 20, 70);
        //施加力
        rig.AddForce(BackDir, ForceMode2D.Impulse);
        IsCanHurt = false;
        //开启一个定时器
        TimerSystem.Instance.RemoveTask("PlayerHurt");
        TimerSystem.Instance.AddTask(1f, () => { IsCanHurt = true; }, "PlayerHurt");
    }

    //死亡
    private void Die()
    {
        state.ChangeState(DieState);
        rig.simulated = false;
        GetComponent<BoxCollider2D>().enabled = false;
        TimerSystem.Instance.AddTask(1, () =>
        {
            UIService.Instance.ShowPanel<DiePanel>();
        });
    }

    //测试用的
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(JumpCheckTrans.position, JumpCheckRadius);
        Gizmos.DrawWireCube(AttackDatas[0].AttackPos + transform.position, AttackDatas[0].AttackRaduis);
    }
}

//玩家攻击数据类
[System.Serializable]
public class PlayerAttackData
{
    public Vector3 AttackPos;//相对于玩家的位置
    public Vector3 AttackRaduis;//攻击范围
}