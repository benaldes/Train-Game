using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : PlayerAbilityState
{
    private int wallJumpDirection;
    private int lastWallDirection;
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
        core.Movement.SetVelocity(playerData.wallJumpVelocity,playerData.wallJumpAngle,wallJumpDirection);
        core.Movement.CheckIfShouldFlip(wallJumpDirection);
        player.JumpState.DecreaseAmountOfJumpsLeft();
        
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        player.Animator.SetFloat(YVelocity,core.Movement.CurrentVelocity.y);
        player.Animator.SetFloat(XVelocity,core.Movement.CurrentVelocity.x);
        

        if (Time.time >= StartTime + playerData.wallJumpTime)
        {
            isAbilityDone = true;
        }

    }

    public void DetermineWallJumpDirection(bool isTouchingWall)
    {
        if (isTouchingWall)
        {
            wallJumpDirection = -core.Movement.FacingDirection;
        }
        else
        {
            wallJumpDirection = core.Movement.FacingDirection;
        }
    }
    public bool TouchWallOnSameSide()
    {
        if (lastWallDirection == -core.Movement.FacingDirection)
        {
            return true;
        }

        return false;
    }
}
