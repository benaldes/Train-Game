using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    #region Variables
    
    private bool isGrounded;
    private bool isTouchingWalls;
    private bool isTouchingLedge;

    protected Movement movement;
    protected CollisionSenses collisionSenses;
    protected Combat combat;

    #endregion

    public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animName)
        : base(player, stateMachine, playerData, animName)
    {
    }

    public override void initializeState()
    {
        base.initializeState();
        movement = core.GetCoreComponent(typeof(Movement)) as Movement;
        collisionSenses = core.GetCoreComponent(typeof(CollisionSenses)) as CollisionSenses;
        combat = core.GetCoreComponent(typeof(Combat)) as Combat;
    }

    public override void Enter()
    {
        base.Enter();
        player.JumpState.ResetAmountsOfJumpsLeft();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (rollInput && player.RollState.IsRollReady() && !combat.isKnockbackActive)
        {
            stateMachine.SwitchState(player.RollState);
        }
        
        if(CheckIfSwitchToAttackState()) return;
        if(CheckIfSwitchToJumpState()) return;
        if(CheckIfSwitchToInAirState()) return;
        
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isGrounded = collisionSenses.CheckIfGrounded();
        isTouchingWalls = collisionSenses.CheckIfTouchingWall();
        isTouchingLedge = collisionSenses.CheckIfTouchingHorizontalLedge();
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

   
}



