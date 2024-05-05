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
        
        core.Movement.SetVelocityX(stateData.ChargeSpeed * core.Movement.FacingDirection);
        
        if (Time.time >= startTime + stateData.ChargeTime)
        {
            isChargeTimeOver = true;
        }
    }

    public override void DoChecks()
    {
        base.DoChecks();
        
        isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
        isDetectingLedge = core.CollisionSenses.CheckIfTouchingVerticalLedge();
        isDetectingWall = core.CollisionSenses.CheckIfTouchingWall();
        performCloseRangeAction = entity.CheckPlayerInCloseRangeAction();
    }


}
