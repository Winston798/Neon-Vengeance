using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 巡逻敌人，比如野猪
/// </summary>
public class EnemyPatrol : Enemy
{
    public EnemyIdlePatrolState PatrolIdleState { get; private set; }
    public EnemyRunPatrolState PatrolPatrolState { get; private set; }
    public EnemyHurtPatrolState PatrolHurtState { get; private set; }
    public float AttackRaduis;
    protected override void InitState()
    {
        PatrolIdleState = new EnemyIdlePatrolState(this, state, "Idle");
        PatrolPatrolState = new EnemyRunPatrolState(this, state, "Run");
        PatrolHurtState = new EnemyHurtPatrolState(this, state, "Hurt");
        DieState = new EnemyDieState(this, state, "Die");
        state.Init(PatrolIdleState);
    }
    public override void Update()
    {
        base.Update();
        if (IsDie)
            return;
        Collider2D collider = Physics2D.OverlapCircle(transform.position, AttackRaduis, LayerMask.GetMask("Player"));
        if (collider != null)
        {
            //攻击玩家
            collider.GetComponent<IHurt>()?.Hurt(transform, Data.Attack);
        }
    }
    protected override void HurtToBack(Transform pos)
    {
        state.ChangeState(PatrolHurtState);
        if (Data.HitEffect != null)
            Instantiate(Data.HitEffect).transform.position = Center.position;
        Vector2 Back = (transform.position - pos.position).normalized;
        Back = new Vector2(Back.x * 100, 30);
        rig.AddForce(Back, ForceMode2D.Impulse);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, AttackRaduis);
    }
}
