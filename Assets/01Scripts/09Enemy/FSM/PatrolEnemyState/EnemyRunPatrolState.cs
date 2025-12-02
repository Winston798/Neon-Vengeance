using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRunPatrolState : EnemyStateBase
{
    private EnemyPatrol enemyPatrol;
    public EnemyRunPatrolState(Enemy enemy, EnemyStateMachine stateMachine, string AniName) : base(enemy, stateMachine, AniName)
    {
        enemyPatrol = enemy as EnemyPatrol;
    }
    //巡逻点
    private List<Vector3> PatrolPos;
    //当前巡逻点
    private Vector3 CurrentTargetPos;
    //巡逻点Index
    private int TargetIndex;
    public override void Enter()
    {
        base.Enter();
        if (PatrolPos == null)
        {
            CreatNewPatrolPos();
        }
        CurrentTargetPos = PatrolPos[TargetIndex];
    }
    private void CreatNewPatrolPos()
    {
        PatrolPos = new List<Vector3>();
        float RandomDistance = Random.Range(1, enemy.Data.PatrolDistance + 1);
        PatrolPos.Add(enemy.Position + enemy.transform.right * RandomDistance);
        PatrolPos.Add(enemy.Position + -enemy.transform.right * RandomDistance);
        TargetIndex = 0;
        TargetIndex = Random.Range(0, PatrolPos.Count);
        CurrentTargetPos = PatrolPos[TargetIndex];
    }
    public override void Exit()
    {
        base.Exit();
        enemyPatrol.SetVelocityX(0);
    }
    public override void FrameUpdate()
    {
        base.FrameUpdate();
        if (Mathf.Abs(enemyPatrol.Position.y - PatrolPos[0].y) >= 0.2f)
        {
            CreatNewPatrolPos();
            return;
        }
        //右边走
        if (CurrentTargetPos.x > enemyPatrol.Position.x)
        {
            enemyPatrol.Flip(true);
            enemyPatrol.SetVelocityX(enemyPatrol.Data.MoveSpeed);
        }
        else
        {
            enemyPatrol.Flip(false);
            enemyPatrol.SetVelocityX(-enemyPatrol.Data.MoveSpeed);
        }
        if (Vector3.Distance(enemyPatrol.Position, CurrentTargetPos) <= 0.5f || enemyPatrol.CheckWall() || !enemyPatrol.CheckGround())
        {
            TargetIndex++;
            if (TargetIndex >= PatrolPos.Count)
            {
                TargetIndex = 0;
            }
            //切换到站立状态
            state.ChangeState(enemyPatrol.PatrolIdleState);
            return;
        }
    }
}
