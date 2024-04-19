using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTouchinWallState : PlayerState
{
    protected bool isGrounded;
    protected bool isTouchingWall;
    protected bool isTouchingLedge;
    
    public PlayerTouchinWallState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animName) : base(player, stateMachine, playerData, animName)
    {
    }
    
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        if(CheckIfSwitchToWallJumpState()) return;
        if(CheckIfSwitchToIdleState()) return;
        if(CheckIfSwitchToWallInAirState()) return;
        if(CheckIfSwitchToWallLedgeClimbState()) return;
    }
    
    public override void DoChecks()
    {
        base.DoChecks();
        isGrounded = core.CollisionSenses.CheckIfGrounded();
        isTouchingWall = core.CollisionSenses.CheckIfTouchingWall();
        isTouchingLedge = core.CollisionSenses.CheckIfTouchingLedge();

        if (isTouchingWall && !isTouchingLedge)
        {
            player.LedgeClimbState.SetDetectedPosition(player.transform.position);
        }
    }
    


    private bool CheckIfSwitchToWallJumpState()
    {
        if (jumpInput)
        {
            player.WallJumpState.DetermineWallJumpDirection(isTouchingWall);
            stateMachine.SwitchState(player.WallJumpState);
            return true;
        }

        return false;
    }

    private bool CheckIfSwitchToIdleState()
    {
        if (isGrounded &&!grabInput)
        {
            stateMachine.SwitchState(player.IdleState);
            return true;
        }

        return false;
    }

    private bool CheckIfSwitchToWallInAirState()
    {
        if(!isTouchingWall || (xInput != core.Movement.FacingDirection && !grabInput))
        {
            stateMachine.SwitchState(player.InAirState);
            return true;
        }

        return false;
    }

    private bool CheckIfSwitchToWallLedgeClimbState()
    {
        if(isTouchingWall && !isTouchingLedge)
        {
            stateMachine.SwitchState(player.LedgeClimbState);
            return true;
        }

        return false;
    }
    

}
