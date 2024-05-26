using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetectedState : State
{
    protected D_PlayerDetected stateData;

    protected bool isPlayerInMinAgroRange;
    protected bool isPlayerInMaxAgroRange;
    protected bool performLongRangeAction;
    protected bool performCloseRangeAction;
    protected bool isDetectingLedge;

    protected Movement movement;
    protected CollisionSenses collisionSenses;
    public PlayerDetectedState(Entity entity, StateMachine stateMachine, string animName,D_PlayerDetected stateData) : base(entity, stateMachine, animName)
    {
        this.stateData = stateData;
    }
    public override void initializeState()
    {
        base.initializeState();
        movement = core.GetCoreComponent(typeof(Movement)) as Movement;
        collisionSenses = core.GetCoreComponent(typeof(CollisionSenses)) as CollisionSenses;
    }
    public override void Enter()
    {
        base.Enter();

        performLongRangeAction = false;
        
        movement.SetVelocityX(0f);

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        movement.SetVelocityX(0f);
        
        if (Time.time >= startTime + stateData.LongRangeActionTime)
        {
            performLongRangeAction = true;
        }
    }

    public override void DoChecks()
    {
        base.DoChecks();
                
        isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
        isPlayerInMaxAgroRange = entity.CheckPlayerInMaxAgroRange();
        performCloseRangeAction = entity.CheckPlayerInCloseRangeAction();
        isDetectingLedge = collisionSenses.CheckIfTouchingVerticalLedge();
    }
    
}
