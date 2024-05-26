using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animName) : base(player, stateMachine, playerData, animName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        movement.SetVelocityX(0f);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(CheckIfSwitchToMoveState()) return;
    }
    
    private bool CheckIfSwitchToMoveState()
    {
        if(xInput != 0f && !isExitingState)
        {
            stateMachine.SwitchState(player.MoveState);
            return true;
        }

        return false;
    }
}
