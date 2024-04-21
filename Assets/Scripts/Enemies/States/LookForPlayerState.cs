
using UnityEngine;

public class LookForPlayerState : State
{
    protected D_LookForPlayerState stateData;

    protected bool TurnImmediately;
    protected bool isPlayerInMinAgroRange;
    protected bool isAllTurnsDone;
    protected bool isAllTurnsTimeDone;
    
    protected float lastTurnTime;
    
    protected int amountOfTurnsDone;
    
    
    
    public LookForPlayerState(Entity entity, StateMachine stateMachine, string animName,D_LookForPlayerState stateData) : base(entity, stateMachine, animName)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();

        isAllTurnsDone = false;
        isAllTurnsTimeDone = false;

        lastTurnTime = startTime;
        amountOfTurnsDone = 0;
        
        Entity.SetVelocity(0f);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (TurnImmediately)
        {
            Entity.Flip();
            lastTurnTime = Time.time;
            amountOfTurnsDone++;
            TurnImmediately = false;
        }
        else if(Time.time >= lastTurnTime + stateData.TimeBetweenTurns && !isAllTurnsDone)
        {
            Entity.Flip();
            lastTurnTime = Time.time;
            amountOfTurnsDone++;
        }

        if (amountOfTurnsDone >= stateData.AmountOfTurns)
        {
            isAllTurnsDone = true;

        }

        if (Time.time >= lastTurnTime + stateData.TimeBetweenTurns && isAllTurnsDone)
        {
            isAllTurnsTimeDone = true;
        }
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isPlayerInMinAgroRange = Entity.CheckPlayerInMinAgroRange();
    }

    public void SetTurnImmediately(bool flip)
    {
        TurnImmediately = flip;
    }
}
