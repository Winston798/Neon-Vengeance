using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseNormalState : EnemyChaseState
{
    private EnemyNormal EnemyNormal;
    public EnemyChaseNormalState(Enemy enemy, EnemyStateMachine stateMachine, string AniName) : base(enemy, stateMachine, AniName)
    {
        EnemyNormal = enemy as EnemyNormal;
    }
    public override bool CheckIsCanAttack()
    {
        return EnemyNormal.CheckIsCanAttack();
    }
}
