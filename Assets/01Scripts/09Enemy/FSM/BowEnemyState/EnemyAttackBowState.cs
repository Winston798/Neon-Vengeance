using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackBowState : EnemyAttackState
{
    private EnemyBow EnemyBow;
    public EnemyAttackBowState(Enemy enemy, EnemyStateMachine stateMachine, string AniName) : base(enemy, stateMachine, AniName)
    {
        EnemyBow = enemy as EnemyBow;
    }
    public override void AniEvent()
    {
        base.AniEvent();
        GameObject go = GameObject.Instantiate(EnemyBow.ArrowPrefab);
        go.transform.position = EnemyBow.ArrowPos.position;
        go.GetComponent<EnemyArrow>().Init(enemy.transform.right, enemy.Data.Attack);
    }
}
