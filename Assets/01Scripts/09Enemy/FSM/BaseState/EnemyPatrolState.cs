using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolState : EnemyStateBase
{
    public EnemyPatrolState(Enemy enemy, EnemyStateMachine stateMachine, string AniName) : base(enemy, stateMachine, AniName)
    {
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
        enemy.SetVelocityX(0);
    }
    public override void FrameUpdate()
    {
        base.FrameUpdate();
        if (enemy.CheckPlayer())
        {
            state.ChangeState(enemy.ChaseState);
            return;
        }
        if (Mathf.Abs(enemy.Position.y - PatrolPos[0].y) >= 0.2f)
        {
            CreatNewPatrolPos();
            return;
        }
        //右边走
        if (CurrentTargetPos.x > enemy.Position.x)
        {
            enemy.Flip(true);
            enemy.SetVelocityX(enemy.Data.MoveSpeed);
        }
        else
        {
            enemy.Flip(false);
            enemy.SetVelocityX(-enemy.Data.MoveSpeed);
        }
        if (Vector3.Distance(enemy.Position, CurrentTargetPos) <= 0.5f || enemy.CheckWall() || !enemy.CheckGround())
        {
            TargetIndex++;
            if (TargetIndex >= PatrolPos.Count)
            {
                TargetIndex = 0;
            }
            //切换到站立状态
            state.ChangeState(enemy.IdleState);
            return;
        }
    }
}
