using UnityEngine;

public class IdleState : State
{
    protected D_IdleState stateData;
    protected bool flipAfterIdle;
    protected bool isIdleTimeOver;
    protected float idleTime;
    protected bool isPlayerInMinAgroRange;
    
    protected Movement movement;
    
    public IdleState(Entity entity, StateMachine stateMachine, string animName,D_IdleState stateData) : base(entity, stateMachine, animName)
    {
        this.stateData = stateData;
    }

    public override void initializeState()
    {
        base.initializeState();
        movement = core.GetCoreComponent(typeof(Movement)) as Movement;
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
        
        isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
    }

    public override void Exit()
    {
        base.Exit();
        if (flipAfterIdle)
        {
            movement.Flip();
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
