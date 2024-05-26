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

    protected Movement movement;
    protected PathFindingComponent pathFinding;
    
    public IdleS(Entity entity, StateMachine stateMachine, string animName,D_EntityData entityData) : base(entity, stateMachine, animName)
    {
        this.entityData = entityData;
    }

    public override void initializeState()
    {
        base.initializeState();
        movement = core.GetCoreComponent(typeof(Movement)) as Movement;
        pathFinding = core.GetCoreComponent(typeof(PathFindingComponent)) as PathFindingComponent;
    }

    public override void Enter()
    {
        base.Enter();
        movement.SetVelocityX(0);
        isIdleTimeOver = false;

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        movement.SetVelocityX(0);
        
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
            movement.Flip();
            flipAfterIdle = false;
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
