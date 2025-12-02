using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine
{
    public PlayerStateBase CurrentState;
    public void Init(PlayerStateBase state)
    {
        CurrentState = state;
        CurrentState.Enter();
    }
    public void ChangeState(PlayerStateBase newState)
    {
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }
}
