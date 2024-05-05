
using UnityEngine;

public class State
{
    protected StateMachine stateMachine;
    protected Entity entity;
    protected Core core;

    protected float startTime;

    protected string animName;

    public State(Entity entity, StateMachine stateMachine,string animName)
    {
        this.stateMachine = stateMachine;
        this.entity = entity;
        this.animName = animName;
        core = entity.core;
    }

    public virtual void Enter()
    {
        startTime = Time.time;
        entity.Animator.SetBool(animName,true);
        DoChecks();
    }

    public virtual void Exit()
    {
        entity.Animator.SetBool(animName,false);
    }

    public virtual void LogicUpdate()
    {
        
    }

    public virtual void PhysicsUpdate()
    {
        DoChecks();
    }
    public virtual void DoChecks(){}
    
}
