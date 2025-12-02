using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerStateBase
{
    public PlayerIdleState(Player player, PlayerStateMachine state, string AniName) : base(player, state, AniName)
    {
    }
    public override void Enter()
    {
        base.Enter();
        player.SetVelocityX(0);
    }
    public override void FrameUpdate()
    {
        base.FrameUpdate();
        //回血
        if (Input.GetKeyDown(KeyCode.L))
        {
            PlayerSystem.Instance.UseRecover();
        }
        //切换状态
        //跳跃
        if (InputSpace && player.IsOnGround)
        {
            state.ChangeState(player.JumpUpState);
            return;
        }
        //下落
        if (!player.IsOnGround && player.IsFalling)
        {
            state.ChangeState(player.JumpDownState);
            return;
        }
        //攻击
        if (InputJ)
        {
            state.ChangeState(player.AttackState);
            return;
        }
        //技能
        if (InputK)
        {
            state.ChangeState(player.HeavyAttackState);
            return;
        }
        //冲刺
        if (InputShift && player.IsCanDash)
        {
            state.ChangeState(player.DashState);
            return;
        }
        //移动
        if (InputX != 0)
        {
            state.ChangeState(player.RunState);
            return;
        }
    }
}
