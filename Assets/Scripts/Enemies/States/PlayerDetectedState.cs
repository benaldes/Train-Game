using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetectedState : State
{
    protected D_PlayerDetected stateData;

    protected bool isPlayerInMinAgroRange;
    protected bool isPlayerInMaxAgroRange;
    protected bool PerformLongRangeAction;
    public PlayerDetectedState(Entity entity, StateMachine stateMachine, string animName,D_PlayerDetected stateData) : base(entity, stateMachine, animName)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();

        PerformLongRangeAction = false;
        
        Entity.SetVelocity(0f);

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.time >= startTime + stateData.LongRangeActionTime)
        {
            PerformLongRangeAction = true;
        }
    }

    public override void DoChecks()
    {
        base.DoChecks();
                
        isPlayerInMinAgroRange = Entity.CheckPlayerInMinAgroRange();
        isPlayerInMaxAgroRange = Entity.CheckPlayerInMaxAgroRange();
    }
    
}