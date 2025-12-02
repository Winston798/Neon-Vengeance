using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSPAttackState : EnemyStateBase
{
    protected EnemyBoss Boss;
    public BossSPAttackState(Enemy enemy, EnemyStateMachine stateMachine, string AniName) : base(enemy, stateMachine, AniName)
    {
        Boss = enemy as EnemyBoss;
    }
    public override void Enter()
    {
        base.Enter();
        Boss.UseSPAttack();
    }
}
