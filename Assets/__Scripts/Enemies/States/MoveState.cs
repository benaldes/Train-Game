using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : State
{
    protected D_MoveState stateData;

    protected bool isDetectingWall;
    protected bool isDetectingLedge;
    protected bool IsPlayerInMinAgroRange;
    
    protected Movement movement;
    protected CollisionSenses collisionSenses;
    public MoveState(Entity entity, StateMachine stateMachine, string animName,D_MoveState stateData) : base(entity, stateMachine, animName)
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
        movement.SetVelocityX(stateData.movementSpeed * movement.FacingDirection);

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        movement.SetVelocityX(stateData.movementSpeed * movement.FacingDirection);
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isDetectingLedge = collisionSenses.CheckIfTouchingVerticalLedge();
        isDetectingWall = collisionSenses.CheckIfTouchingWall();
        IsPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
    }
}
