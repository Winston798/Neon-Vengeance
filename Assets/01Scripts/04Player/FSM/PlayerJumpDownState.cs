using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpDownState : PlayerStateBase
{
    public PlayerJumpDownState(Player player, PlayerStateMachine state, string AniName) : base(player, state, AniName)
    {
    }
    public override void Enter()
    {
        base.Enter();
    }
    public override void FrameUpdate()
    {
        base.FrameUpdate();
        //冲刺
        if (InputShift && player.IsCanDash)
        {
            state.ChangeState(player.DashState);
            return;
        }
        //攻击
        if (InputJ)
        {
            state.ChangeState(player.AttackState);
            return;
        }
        //降落到地面
        if (player.IsOnGround && InputX == 0)
        {
            state.ChangeState(player.IdleState);
            return;
        }
        else if (player.IsOnGround && InputX != 0)
        {
            state.ChangeState(player.RunState);
            return;
        }
        player.SetVelocityX(PlayerSystem.Instance.MoveSpeed * InputX);
        if (InputX >= 0)
            player.Flip(true);
        else
            player.Flip(false);
    }
}
