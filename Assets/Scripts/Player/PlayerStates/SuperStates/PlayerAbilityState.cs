using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityState : PlayerState
{
    protected bool isAbilityDone;
    protected bool IsGrounded;
    public PlayerAbilityState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animName) : base(player, stateMachine, playerData, animName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        isAbilityDone = false;
    }
    
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        if (!isAbilityDone) return;
        
        if(CheckIfSwitchToIdleState()) return;
        
        stateMachine.SwitchState(player.InAirState);

    }
    
    public override void DoChecks()
    {
        base.DoChecks();
        IsGrounded = core.CollisionSenses.CheckIfGrounded();
    }

    private bool CheckIfSwitchToIdleState()
    {
        if (IsGrounded && core.Movement.CurrentVelocity.y < 0.01f)
        {
            stateMachine.SwitchState(player.IdleState);
            return true;
        }

        return false;
    }
}
