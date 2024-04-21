using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : State
{
    protected D_MoveState stateData;

    protected bool isDetectingWall;
    protected bool isDetectingLedge;
    public MoveState(Entity entity, StateMachine stateMachine, string animName,D_MoveState stateData) : base(entity, stateMachine, animName)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();
        Entity.SetVelocity(stateData.movementSpeed);
        isDetectingLedge = Entity.CheckLedge();
        isDetectingWall = Entity.CheckWall();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        isDetectingLedge = Entity.CheckLedge();
        isDetectingWall = Entity.CheckWall();
    }
}
