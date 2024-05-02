using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    protected D_IdleState stateData;
    protected bool flipAfterIdle;
    protected bool isIdleTimeOver;
    protected float idleTime;
    protected bool isPlayerInMinAgroRange;
    
    public IdleState(Entity entity, StateMachine stateMachine, string animName,D_IdleState stateData) : base(entity, stateMachine, animName)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();
        core.Movement.SetVelocityX(0);
        isIdleTimeOver = false;

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        core.Movement.SetVelocityX(0);
        
        if (Time.time >= startTime + idleTime)
        {
            isIdleTimeOver = true;
        }
    }



    public override void DoChecks()
    {
        base.DoChecks();
        
        isPlayerInMinAgroRange = Entity.CheckPlayerInMinAgroRange();
    }

    public override void Exit()
    {
        base.Exit();
        if (flipAfterIdle)
        {
            core.Movement.Flip();
        }
    }

    public void SetFlipAfterIdle(bool flip)
    {
        flipAfterIdle = flip;
    }

    private void SetRandomIdleTime()
    {
        idleTime = Random.Range(stateData.MinIdleTime, stateData.MaxIdleTime);
    }
}
