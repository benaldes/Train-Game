
using UnityEngine;

public class State
{
    protected StateMachine stateMachine;
    protected Entity Entity;
    protected Core core;

    protected float startTime;

    protected string animName;

    public State(Entity entity, StateMachine stateMachine,string animName)
    {
        this.stateMachine = stateMachine;
        this.Entity = entity;
        this.animName = animName;
        core = entity.core;
    }

    public virtual void Enter()
    {
        startTime = Time.time;
        Entity.Animator.SetBool(animName,true);
        DoChecks();
    }

    public virtual void Exit()
    {
        Entity.Animator.SetBool(animName,false);
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
