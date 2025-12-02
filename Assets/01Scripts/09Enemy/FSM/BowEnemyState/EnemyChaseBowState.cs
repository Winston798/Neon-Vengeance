using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseBowState : EnemyChaseState
{
    private EnemyBow EnemyBow;
    public EnemyChaseBowState(Enemy enemy, EnemyStateMachine stateMachine, string AniName) : base(enemy, stateMachine, AniName)
    {
        EnemyBow = enemy as EnemyBow;
    }
    public override bool CheckIsCanAttack()
    {
        return EnemyBow.CheckIsCanAttack();
    }
}
