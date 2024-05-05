using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : State
{
    protected D_MoveState stateData;

    protected bool isDetectingWall;
    protected bool isDetectingLedge;
    protected bool IsPlayerInMinAgroRange;
    public MoveState(Entity entity, StateMachine stateMachine, string animName,D_MoveState stateData) : base(entity, stateMachine, animName)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();
        core.Movement.SetVelocityX(stateData.movementSpeed * core.Movement.FacingDirection);

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        core.Movement.SetVelocityX(stateData.movementSpeed * core.Movement.FacingDirection);
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isDetectingLedge = core.CollisionSenses.CheckIfTouchingVerticalLedge();
        isDetectingWall = core.CollisionSenses.CheckIfTouchingWall();
        IsPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
    }
}
