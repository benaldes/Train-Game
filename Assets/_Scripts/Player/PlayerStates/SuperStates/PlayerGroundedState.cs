using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    #region Variables
    
    private bool isGrounded;
    private bool isTouchingWalls;
    private bool isTouchingLedge;

    #endregion

    public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animName)
        : base(player, stateMachine, playerData, animName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.JumpState.ResetAmountsOfJumpsLeft();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        //InputChecks();

        if (rollInput && player.RollState.IsRollReady() && !core.Combat.isKnockbackActive)
        {
            stateMachine.SwitchState(player.RollState);
        }
        
        if(CheckIfSwitchToAttackState()) return;
        if(CheckIfSwitchToJumpState()) return;
        if(CheckIfSwitchToInAirState()) return;
        if(CheckIfSwitchToWallGrabState()) return;
        
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isGrounded = core.CollisionSenses.CheckIfGrounded();
        isTouchingWalls = core.CollisionSenses.CheckIfTouchingWall();
        isTouchingLedge = core.CollisionSenses.CheckIfTouchingHorizontalLedge();
    }


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
    private bool CheckIfSwitchToJumpState()
    {
        if (jumpInput && player.JumpState.canJump())
        {
            stateMachine.SwitchState(player.JumpState);
            return true;
        }

        return false;
    }

    private bool CheckIfSwitchToInAirState()
    {
        if (!isGrounded)
        {
            player.InAirState.StartCoyoteTime();
            stateMachine.SwitchState(player.InAirState);
            return true;
        }

        return false;
    }

    private bool CheckIfSwitchToWallGrabState()
    {
        if (isTouchingWalls && grabInput && isTouchingLedge)
        {
            stateMachine.SwitchState(player.WallGrabState);
            return true;
        }

        return false;
    }
}



