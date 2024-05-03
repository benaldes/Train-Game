using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLandState : PlayerGroundedState
{
    public PlayerLandState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animName) : base(player, stateMachine, playerData, animName)
    {
    }
    
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(CheckIfSwitchToMoveState()) return;
        if(CheckIfSwitchToIdleState()) return;
        
    }

    private bool CheckIfSwitchToMoveState()
    {
        if (xInput != 0f)
        {
            stateMachine.SwitchState(player.MoveState);
            return true;
        }

        return false;
    }
    private bool CheckIfSwitchToIdleState()
    {
        if (isAnimationFinished)
        {

            stateMachine.SwitchState(player.IdleState);
            return true;
        }

        return false;
    }

 
}

