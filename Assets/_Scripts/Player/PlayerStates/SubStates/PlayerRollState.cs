using UnityEngine;

public class PlayerRollState : PlayerAbilityState
{
    private bool rollIsReady = true;
    
    
    public PlayerRollState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animName) 
        : base(player, stateMachine, playerData, animName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.InputHandler.UseRollInput();
        
        core.Combat.SetDamageImmune(true);
        core.Combat.SetKnockbackImmune(true);
        core.Movement.SetVelocity(playerData.rollVelocity, playerData.rollAngle, core.Movement.FacingDirection);
    }

    public override void Exit()
    {
        base.Exit();
        core.Movement.SetVelocityZero();
        core.Combat.SetDamageImmune(false);
        core.Combat.SetKnockbackImmune(false);
    }
    
    public override void LogicUpdate()
    {
        base.LogicUpdate();
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
