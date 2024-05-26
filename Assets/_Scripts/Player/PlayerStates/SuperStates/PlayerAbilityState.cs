using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityState : PlayerState
{
    protected bool isAbilityDone;
    protected bool IsGrounded;

    protected Movement movement;
    protected CollisionSenses collisionSenses;
    protected Combat combat;
    public PlayerAbilityState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animName) : base(player, stateMachine, playerData, animName)
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
        IsGrounded = collisionSenses.CheckIfGrounded();
    }

    private bool CheckIfSwitchToIdleState()
    {
        if (IsGrounded && movement.CurrentVelocity.y < 0.01f)
        {
            stateMachine.SwitchState(player.IdleState);
            return true;
        }

        return false;
    }
}
