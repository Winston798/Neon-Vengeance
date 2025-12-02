using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpUpState : PlayerStateBase
{
    public PlayerJumpUpState(Player player, PlayerStateMachine state, string AniName) : base(player, state, AniName)
    {
    }
    private float timer;
    public override void Enter()
    {
        base.Enter();
        timer = 0;
        player.SetVelocityY(0);
        player.rig.AddForce(player.transform.up * 200, ForceMode2D.Impulse);
    }
    public override void FrameUpdate()
    {
        base.FrameUpdate();
        timer += Time.deltaTime;
        //下落状态
        if (!player.IsOnGround && player.IsFalling)
        {
            state.ChangeState(player.JumpDownState);
            return;
        }
        //上升状态落地
        if (player.IsOnGround && timer >= 0.1f)
        {
            state.ChangeState(player.IdleState);
            return;
        }
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
        player.SetVelocityX(PlayerSystem.Instance.MoveSpeed * InputX);
        if (InputX >= 0)
            player.Flip(true);
        else
            player.Flip(false);
    }
}
