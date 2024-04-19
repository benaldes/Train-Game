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
        
        core.Movement.SetVelocity(playerData.rollVelocity, playerData.rollAngle, core.Movement.FacingDirection);
    }

    public override void Exit()
    {
        base.Exit();
        core.Movement.SetVelocityZero();
    }

  

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(SwitchToJumpState()) return;
        if (isAbilityDone && IsGrounded)
        {
            stateMachine.SwitchState(player.IdleState);
                
        }
        else if (isAbilityDone && !IsGrounded)
        {
            stateMachine.SwitchState(player.InAirState);
        }
        
    }

    private bool SwitchToJumpState()
    {
        if (jumpInput && player.JumpState.canJump())
        {
            stateMachine.SwitchState(player.JumpState);
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
