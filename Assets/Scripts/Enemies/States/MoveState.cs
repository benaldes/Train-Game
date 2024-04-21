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
        Entity.SetVelocity(stateData.movementSpeed);

    }
    

    public override void DoChecks()
    {
        base.DoChecks();
        
        isDetectingLedge = Entity.CheckLedge();
        isDetectingWall = Entity.CheckWall();
        IsPlayerInMinAgroRange = Entity.CheckPlayerInMinAgroRange();
    }
}
