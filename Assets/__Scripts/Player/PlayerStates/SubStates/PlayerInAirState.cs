using UnityEngine;

public class PlayerInAirState : PlayerState
{
    #region Variables
    

    private bool isGrounded;
    private bool isTouchingWall;
    private bool isTouchingWallBack;
    private bool oldIsTouchingWall;
    private bool oldIsTouchingWallBack;
    private bool coyoteTime;
    private bool wallJumpCoyoteTime;
    private bool isJumping;
    private bool isTouchingLedge;
    private bool isTouchingLedgeWithFeet;

    private float startWallJumpCoyoteTime;
    
    private static readonly int YVelocity = Animator.StringToHash("YVelocity");
    private static readonly int XVelocity = Animator.StringToHash("XVelocity");

    private Movement movement;
    protected CollisionSenses collisionSenses;
    protected Combat combat;

    #endregion
    public PlayerInAirState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animName) : base(player, stateMachine, playerData, animName) { }

    #region State Callback Functions

    public override void initializeState()
    {
        base.initializeState();
        movement = core.GetCoreComponent(typeof(Movement)) as Movement;
        collisionSenses = core.GetCoreComponent(typeof(CollisionSenses)) as CollisionSenses;
        combat = core.GetCoreComponent(typeof(Combat)) as Combat;
    }

    public override void Exit()
    {
        base.Exit();
        oldIsTouchingWall = false;
        oldIsTouchingWallBack = false;
        isTouchingWall = false;
        isTouchingWallBack = false;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        CheckCoyoteTime();
        CheckWallJumpCoyoteTime();
        CheckIfJumpInputIsHeld();
        
        if(CheckIfSwitchToAttackState()) return;
        if(CheckIfSwitchToRollState()) return;
        if(CheckIfSwitchToLandState())return;
        if(CheckIfSwitchToStepOverState()) return;
        if(CheckIfSwitchToLedgeClimbState()) return;
        if(CheckIfSwitchToWallJumpState()) return;
        if(CheckIfSwitchToJumpState()) return;
        if(CheckIfSwitchToWallSlideState())return;
        
        movement.CheckIfShouldFlip(xInput);
        movement.SetVelocityX(playerData.MovementVelocity * xInput);
        player.Animator.SetFloat(YVelocity, movement.CurrentVelocity.y);
        player.Animator.SetFloat(XVelocity, Mathf.Abs(movement.CurrentVelocity.x));
        
    }
    
    public override void DoChecks()
    {
        base.DoChecks();
        oldIsTouchingWall = isTouchingWall;
        oldIsTouchingWallBack = isTouchingWallBack;
        
        isGrounded = collisionSenses.CheckIfGrounded();
        isTouchingWall = collisionSenses.CheckIfTouchingWall();
        isTouchingWallBack = collisionSenses.CheckIfTouchingWallBack();
        isTouchingLedge = collisionSenses.CheckIfTouchingHorizontalLedge();
        isTouchingLedgeWithFeet = collisionSenses.CheckIfTouchingLedgeWithFeet();
        
        if(isTouchingLedgeWithFeet && !isTouchingWall)
        {
            player.StepOverState.SetDetectedPosition(player.transform.position);
        }
        else if (isTouchingWall && !isTouchingLedge)
        {
            player.LedgeClimbState.SetDetectedPosition(player.transform.position);
        }
        if (!wallJumpCoyoteTime && !isTouchingWall && !isTouchingWallBack &&
            (oldIsTouchingWall || oldIsTouchingWallBack))
        {
            StartWallJumpCoyoteTime();
        }
    }
    
    #endregion
    
    #region Switch States Checkes

    private bool CheckIfSwitchToAttackState()
    {
        if (player.InputHandler.AttackInputs[(int)CombatInputs.primary])
        {
            stateMachine.SwitchState(player.PrimaryAttackState);
            return true;
        }
        else if (player.InputHandler.AttackInputs[(int)CombatInputs.secondary])
        {
            stateMachine.SwitchState(player.SecondaryAttackState);
            return true;
        }

        return false;
    }

    private bool CheckIfSwitchToStepOverState()
    {
        if (isTouchingLedgeWithFeet && !isTouchingWall && !isTouchingLedge && !isGrounded)
        {
            stateMachine.SwitchState(player.StepOverState);
            return true;
        }

        return false;
    }
    
    private bool CheckIfSwitchToLandState()
    {
        if (isGrounded && movement.CurrentVelocity.y < 0.01f)
        {
            stateMachine.SwitchState((player.LandState));
            return true;
        }

        return false;
    }

    private bool CheckIfSwitchToRollState()
    {
        if (rollInput && player.RollState.IsRollReady() && !combat.isKnockbackActive)
        {
            stateMachine.SwitchState(player.RollState);
            return true;
        }

        return false;
    }

    private bool CheckIfSwitchToLedgeClimbState()
    {
        if(isTouchingWall && !isTouchingLedge && !isGrounded)
        {
            stateMachine.SwitchState(player.LedgeClimbState);
            return true;
        }

        return false;
    }

    private bool CheckIfSwitchToWallJumpState()
    {
        if(jumpInput && (isTouchingWall || isTouchingWallBack || wallJumpCoyoteTime))
        {
            StopWallJumpCoyoteTime();
            isTouchingWall = collisionSenses.CheckIfTouchingWall();
            player.WallJumpState.DetermineWallJumpDirection(isTouchingWall);
            stateMachine.SwitchState(player.WallJumpState);
            return true;
        }

        return false;
    }

    private bool CheckIfSwitchToJumpState()
    {
        if (jumpInput && player.JumpState.canJump())
        {
            coyoteTime = false;
            stateMachine.SwitchState(player.JumpState);
            return true;
        }

        return false;
    }

    private bool CheckIfSwitchToWallSlideState()
    {
        if(isTouchingWall && xInput == movement.FacingDirection && movement.CurrentVelocity.y <= 0)
        {
            stateMachine.SwitchState(player.WallSlideState);
            return true;
        }
        return false;
    }

    #endregion
    
    #region other Checks

    private void CheckIfJumpInputIsHeld()
    {
        if (isJumping)
        {
            if (jumpInputStop)
            {
                movement.SetVelocityY(movement.CurrentVelocity.y * playerData.variableJumpHeightMultiplier);
                isJumping = false;
            }
            else if(movement.CurrentVelocity.y <= 0)
            {
                isJumping = false;
            }
        }
        
    }
    
    private void CheckCoyoteTime()
    {
        if (coyoteTime && Time.time > StartTime + playerData.coyoteTime)
        {
            coyoteTime = false;
            player.JumpState.DecreaseAmountOfJumpsLeft();
        }
    }

    private void CheckWallJumpCoyoteTime()
    {
        if (wallJumpCoyoteTime && Time.time > startWallJumpCoyoteTime + playerData.coyoteTime)
        {
            wallJumpCoyoteTime = false;
        }
    }

    #endregion

    #region Other

    public void StartWallJumpCoyoteTime()
    {
        wallJumpCoyoteTime = true;
        startWallJumpCoyoteTime = Time.time;
    }
    public void StartCoyoteTime() => coyoteTime = true;
    
    public void StopWallJumpCoyoteTime() => wallJumpCoyoteTime = false;
    
    public void SetIsJumping() => isJumping = true;

    #endregion
    
}
