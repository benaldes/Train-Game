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
    public StunState(Entity entity, StateMachine stateMachine, string animName,D_StunState state) : base(entity, stateMachine, animName)
    {
        this.State = state;
    }

    public override void Enter()
    {
        base.Enter();
        isStunTimeOver = false;
        isMovementStop = false;
        core.Movement.SetVelocity(State.StunKnockbackSpeed,State.StunKnockbackAngle, Entity.lastDamageDirection);
        
    }

    public override void Exit()
    {
        base.Exit();
        //Entity.ResetStunResistance();
        core.Stats.ResetStunResistance();
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
            core.Movement.SetVelocityX(0);
        }
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isGrounded = core.CollisionSenses.CheckIfGrounded();
        performCloseRangeAction = Entity.CheckPlayerInCloseRangeAction();
        isPlayerInMinAgroRange = Entity.CheckPlayerInMinAgroRange();
    }
}
