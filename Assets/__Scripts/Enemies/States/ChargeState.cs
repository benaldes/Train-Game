using UnityEngine;

public class ChargeState : State
{
    protected D_ChargeState stateData;

    protected bool isPlayerInMinAgroRange;
    protected bool isDetectingLedge;
    protected bool isDetectingWall;
    protected bool isChargeTimeOver;
    protected bool performCloseRangeAction;
    
    protected Movement movement;
    protected CollisionSenses collisionSenses;
    public ChargeState(Entity entity, StateMachine stateMachine, string animName, D_ChargeState stateData) : base(entity, stateMachine, animName)
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
        isChargeTimeOver = false;
        movement.SetVelocityX(stateData.ChargeSpeed * movement.FacingDirection);
        
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        movement.SetVelocityX(stateData.ChargeSpeed * movement.FacingDirection);
        
        if (Time.time >= startTime + stateData.ChargeTime)
        {
            isChargeTimeOver = true;
        }
    }

    public override void DoChecks()
    {
        base.DoChecks();
        
        isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
        isDetectingLedge = collisionSenses.CheckIfTouchingVerticalLedge();
        isDetectingWall = collisionSenses.CheckIfTouchingWall();
        performCloseRangeAction = entity.CheckPlayerInCloseRangeAction();
    }


}
