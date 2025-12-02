using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//反应状态
public class EnemyReactState : EnemyStateBase
{
    public EnemyReactState(Enemy enemy, EnemyStateMachine stateMachine, string AniName) : base(enemy, stateMachine, AniName)
    {
    }
    private float Timer;
    public override void Enter()
    {
        base.Enter();
        Timer = 0;
    }
    public override void FrameUpdate()
    {
        base.FrameUpdate();
        Timer += Time.deltaTime;
        if (Timer >= enemy.Data.ReactTime)
        {
            state.ChangeState(enemy.AttackState);
        }
    }
}
