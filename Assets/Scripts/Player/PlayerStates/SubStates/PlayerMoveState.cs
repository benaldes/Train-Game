using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animName) : base(player, stateMachine, playerData, animName)
    {
    }
    
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        core.Movement.CheckIfShouldFlip(xInput);
        core.Movement.SetVelocityX(playerData.MovementVelocity * xInput);
        if (xInput == 0f && !isExitingState)
        {
            stateMachine.SwitchState(player.IdleState);
        }
    }


}
