using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPauseState : PlayerStateBase
{
    public PlayerPauseState(Player player, PlayerStateMachine state, string AniName) : base(player, state, AniName)
    {
    }
    public override void Enter()
    {
        player.SetVelocity(Vector2.zero);
    }
    public override void Exit()
    {
        player.SetVelocity(Vector2.zero);
    }
}
