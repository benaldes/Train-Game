using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleS : State
{
    protected D_EntityData entityData;
    protected float idleTime;
    protected bool flipAfterIdle;
    protected bool pathFound;
    protected bool isIdleTimeOver;
    protected bool isPlayerInMinAgroRange;
    
    public IdleS(Entity entity, StateMachine stateMachine, string animName,D_EntityData entityData) : base(entity, stateMachine, animName)
    {
        this.entityData = entityData;
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
        
       // isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
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
        idleTime = Random.Range(entityData.MinIdleTime, entityData.MaxIdleTime);
    }
}
