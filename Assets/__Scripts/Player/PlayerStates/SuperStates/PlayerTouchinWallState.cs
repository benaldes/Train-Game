using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTouchinWallState : PlayerState
{
    protected bool isGrounded;
    protected bool isTouchingWall;
    protected bool isTouchingLedge;

    protected Movement movement;
    protected CollisionSenses collisionSenses;
    
    public PlayerTouchinWallState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animName) : base(player, stateMachine, playerData, animName)
    {
    }

    public override void initializeState()
    {
        base.initializeState();
        movement = core.GetCoreComponent(typeof(Movement)) as Movement;
        collisionSenses = core.GetCoreComponent(typeof(CollisionSenses)) as CollisionSenses;
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
        isGrounded = collisionSenses.CheckIfGrounded();
        isTouchingWall = collisionSenses.CheckIfTouchingWall();
        isTouchingLedge = collisionSenses.CheckIfTouchingHorizontalLedge();

        if (isTouchingWall && !isTouchingLedge)
        {
            player.LedgeClimbState.SetDetectedPosition(player.transform.position);
        }
    }
    


    private bool CheckIfSwitchToWallJumpState()
    {
        if (jumpInput && !player.WallJumpState.TouchWallOnSameSide())
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
        if(!isTouchingWall || (xInput != movement.FacingDirection && !grabInput))
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
