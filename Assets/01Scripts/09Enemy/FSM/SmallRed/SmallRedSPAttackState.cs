using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallRedSPAttackState : BossSPAttackState
{
    public SmallRedSPAttackState(Enemy enemy, EnemyStateMachine stateMachine, string AniName) : base(enemy, stateMachine, AniName)
    {
    }
    private int SPContinueTime = 2;//SP攻击持续时间
    private float timer;
    public override void Enter()
    {
        base.Enter();
        timer = 0;
    }
    public override void Exit()
    {
        base.Exit();
        Boss.SetVelocityX(0);
    }
    public override void FrameUpdate()
    {
        base.FrameUpdate();
        Boss.SetVelocityX(Boss.Dir * 5);
        timer += Time.deltaTime;
        if (timer >= SPContinueTime)
        {
            state.ChangeState(Boss.BossIdleState);
            return;
        }
    }
    public override void AniEvent()
    {
        //物理检测
        Collider2D coll = Physics2D.OverlapBox(Boss.BossSPAttackPos.position, Boss.BossSPAttackRadius, LayerMask.GetMask("Player"));
        if (coll != null)
        {
            coll.GetComponent<IHurt>()?.Hurt(enemy.transform, enemy.Data.Attack);
        }
    }
}
