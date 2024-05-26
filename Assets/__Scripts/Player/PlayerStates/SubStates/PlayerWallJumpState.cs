using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : PlayerAbilityState
{
    private int wallJumpDirection;
    private int lastWallDirection;

    private float workSpace;
    private static readonly int YVelocity = Animator.StringToHash("YVelocity");
    private static readonly int XVelocity = Animator.StringToHash("XVelocity");

    public PlayerWallJumpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animName) 
        : base(player, stateMachine, playerData, animName) { }

    public override void Enter()
    {
        base.Enter();
        lastWallDirection = wallJumpDirection;
        player.InputHandler.UseJumpInput();
        player.JumpState.ResetAmountsOfJumpsLeft();
        movement.SetVelocity(playerData.wallJumpVelocity,playerData.wallJumpAngle,wallJumpDirection);
        movement.CheckIfShouldFlip(wallJumpDirection);
        player.JumpState.DecreaseAmountOfJumpsLeft();
        
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        player.Animator.SetFloat(YVelocity,movement.CurrentVelocity.y);
        player.Animator.SetFloat(XVelocity,movement.CurrentVelocity.x);
        
        
        if (Time.time >= StartTime + playerData.wallJumpTime)
        {
            isAbilityDone = true;
        }

    }

    public void DetermineWallJumpDirection(bool isTouchingWall)
    {
        if (isTouchingWall)
        {
            wallJumpDirection = -movement.FacingDirection;
        }
        else
        {
            wallJumpDirection = movement.FacingDirection;
        }
    }
    public bool TouchWallOnSameSide()
    {
        if (lastWallDirection == -movement.FacingDirection)
        {
            return true;
        }

        return false;
    }
}
