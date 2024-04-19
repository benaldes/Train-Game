
using UnityEngine;

public class EnemyState
{
    protected EnemyStateMachine stateMachine;
    protected Enemy enemy;

    protected float startTime;

    protected string animName;

    public EnemyState(Enemy enemy, EnemyStateMachine stateMachine,string animName)
    {
        this.stateMachine = stateMachine;
        this.enemy = enemy;
        this.animName = animName;
    }

    public virtual void Enter()
    {
        startTime = Time.time;
        enemy.Animator.SetBool(animName,true);
    }

    public virtual void Exit()
    {
        enemy.Animator.SetBool(animName,false);
    }

    public virtual void LogicUpdate()
    {
        
    }

    public virtual void PhysicsUpdate()
    {
        
    }
    
}
