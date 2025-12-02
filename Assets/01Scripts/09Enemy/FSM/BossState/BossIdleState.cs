using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIdleState : EnemyStateBase
{
    private float timer;
    private EnemyBoss Boss;

    public BossIdleState(Enemy enemy, EnemyStateMachine stateMachine, string AniName) : base(enemy, stateMachine, AniName)
    {
        Boss = enemy as EnemyBoss;
    }

    public override void Enter()
    {
        base.Enter();
        timer = 0;
    }
    public override void FrameUpdate()
    {
        base.FrameUpdate();
        timer += Time.deltaTime;
        if (timer >= Boss.Data.IdleTime)
        {
            //切换到追逐状态
            state.ChangeState(Boss.BossChaseState);
        }
    }
}
