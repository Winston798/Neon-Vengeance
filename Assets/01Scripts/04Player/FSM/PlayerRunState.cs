using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunState : PlayerStateBase
{
    public PlayerRunState(Player player, PlayerStateMachine state, string AniName) : base(player, state, AniName)
    {
    }
    public override void Exit()
    {
        base.Exit();
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
        //状态切换
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
        //重击
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
        //站立
        if (InputX == 0)
        {
            state.ChangeState(player.IdleState);
            return;
        }
        player.SetVelocityX(PlayerSystem.Instance.MoveSpeed * InputX);
        if (InputX >= 0)
            player.Flip(true);
        else
            player.Flip(false);
    }
}
