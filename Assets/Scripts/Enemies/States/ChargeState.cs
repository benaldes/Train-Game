using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeState : State
{
    protected D_ChargeState stateData;

    protected bool isPlayerInMinAgroRange;
    protected bool isDetectingLedge;
    protected bool isDetectingWall;
    protected bool isChargeTimeOver;
    protected bool performCloseRangeAction;
    
    public ChargeState(Entity entity, StateMachine stateMachine, string animName, D_ChargeState stateData) : base(entity, stateMachine, animName)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();
        isChargeTimeOver = false;
        core.Movement.SetVelocityX(stateData.ChargeSpeed * core.Movement.FacingDirection);
        
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (Time.time >= startTime + stateData.ChargeTime)
        {
            isChargeTimeOver = true;
        }
    }

    public override void DoChecks()
    {
        base.DoChecks();
        
        isPlayerInMinAgroRange = Entity.CheckPlayerInMinAgroRange();
        isDetectingLedge = core.CollisionSenses.CheckIfTouchingVerticalLedge();
        isDetectingWall = core.CollisionSenses.CheckIfTouchingWall();
        performCloseRangeAction = Entity.CheckPlayerInCloseRangeAction();
    }


}
