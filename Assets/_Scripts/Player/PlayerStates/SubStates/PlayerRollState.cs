using UnityEngine;

public class PlayerRollState : PlayerAbilityState
{
    private bool rollIsReady = true;

    private float workSpace;
    public PlayerRollState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animName) 
        : base(player, stateMachine, playerData, animName) { }

    public override void Enter()
    {
        base.Enter();
        player.InputHandler.UseRollInput();
        
        combat.SetDamageImmune(true);
        combat.SetKnockbackImmune(true);
        //acore.Movement.SetVelocity(playerData.rollVelocity, playerData.rollAngle, core.Movement.FacingDirection);
    }

    public override void Exit()
    {
        base.Exit();
        movement.SetVelocityZero();
        combat.SetDamageImmune(false);
        combat.SetKnockbackImmune(false);
    }
    
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        workSpace = playerData.RollAngleCurve.Evaluate(Time.time - StartTime);
        movement.SetVelocity(playerData.rollVelocity, workSpace, movement.FacingDirection);
        
        if(CheckIfSwitchToJumpState()) return;
        if(CheckIfSwitchToIdleState()) return;
        if(CheckIfSwitchToInAirState()) return;
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
    
    private bool CheckIfSwitchToIdleState()
    {
        if (isAbilityDone && IsGrounded)
        {
            stateMachine.SwitchState(player.IdleState);
            return true;
        }
        return false;
    }
    
    private bool CheckIfSwitchToInAirState()
    {
        if (isAbilityDone && !IsGrounded)
        {
            stateMachine.SwitchState(player.InAirState);
            return true;
        }
        return false;
    }
    
    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        isAbilityDone = true;
        rollIsReady = false;
    }

    public bool IsRollReady()
    {
        if (rollIsReady) return true;
        else if (Time.time >= StartTime + playerData.rollCooldown)
        {
            rollIsReady = true;
            return true;
        }

        return false;
    }
}
