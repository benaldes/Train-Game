using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunState : State
{
    protected D_StunState State;
    protected bool isGrounded;
    protected bool isMovementStop;
    protected bool performCloseRangeAction;
    protected bool isPlayerInMinAgroRange;

    protected bool isStunTimeOver;
    
    protected Movement movement;
    protected CollisionSenses collisionSenses;
    protected Stats stats;
    public StunState(Entity entity, StateMachine stateMachine, string animName,D_StunState state) : base(entity, stateMachine, animName)
    {
        this.State = state;
    }
    public override void initializeState()
    {
        base.initializeState();
        movement = core.GetCoreComponent(typeof(Movement)) as Movement;
        collisionSenses = core.GetCoreComponent(typeof(CollisionSenses)) as CollisionSenses;
        stats = core.GetCoreComponent(typeof(Stats)) as Stats;

    }
    public override void Enter()
    {
        base.Enter();
        isStunTimeOver = false;
        isMovementStop = false;
        movement.SetVelocity(State.StunKnockbackSpeed,State.StunKnockbackAngle, entity.lastDamageDirection);
        
    }

    public override void Exit()
    {
        base.Exit();
        //Entity.ResetStunResistance();
        stats.ResetStunResistance();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (Time.time >= startTime + State.StunTime)
        {
            isStunTimeOver = true;
        }

        if (isGrounded && Time.time >= startTime + State.StunKnockbackTime && !isMovementStop)
        {
            isMovementStop = true;
            movement.SetVelocityX(0);
        }
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isGrounded = collisionSenses.CheckIfGrounded();
        performCloseRangeAction = entity.CheckPlayerInCloseRangeAction();
        isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
    }
}
