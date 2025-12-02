using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 敌人状态基类
/// </summary>
public class EnemyStateBase
{
    public Enemy enemy;
    public EnemyStateMachine state;
    public string AniName;//动画名称
    protected bool AniComplete;//动画是否播放完成
    public EnemyStateBase(Enemy enemy, EnemyStateMachine stateMachine, string AniName)
    {
        this.enemy = enemy;
        this.state = stateMachine;
        this.AniName = AniName;
    }
    public virtual void Enter()
    {
        enemy.ani.Play("Default");
        enemy.ani.Play(AniName);
        AniComplete = false;
    }
    public virtual void Exit() { }
    public virtual void FrameUpdate()
    {
        //检测该状态是否正确播放对应动画
        AnimatorStateInfo info = enemy.ani.GetCurrentAnimatorStateInfo(0);
        if (!info.IsName(AniName))
        {
            enemy.ani.Play(AniName);
            return;
        }
        //检测动画是否播放完成
        if (info.normalizedTime >= 1f)
        {
            AniComplete = true;
        }
    }
    public virtual void FixUpdate() { }

    public virtual void AniEvent() { }
}

