using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallGrabState : PlayerTouchinWallState
{
    private Vector2 holdPosition;
    public PlayerWallGrabState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animName) : base(player, stateMachine, playerData, animName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        holdPosition = player.transform.position;
        HoldPosition();

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        if (isExitingState) return;
        
        HoldPosition();
        if (yInput > 0)
        {
            stateMachine.SwitchState(player.WallClimbState);
        }
        else if (yInput < 0 || !grabInput)
        {
            stateMachine.SwitchState(player.WallSlideState);
        }
        
        
    }

    private void HoldPosition()
    {
        player.transform.position = holdPosition;
        core.Movement.SetVelocityZero();
    }

  
}
