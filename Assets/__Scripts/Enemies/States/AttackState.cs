using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    protected Transform attackPosition;

    protected bool isAnimationFinish;
    protected bool isPlayerInMinAgroRange;
    
    protected Movement movement;
    public AttackState(Entity entity, StateMachine stateMachine, string animName, Transform attackPosition) : base(entity, stateMachine, animName)
    {
        this.attackPosition = attackPosition;
    }

    public override void initializeState()
    {
        base.initializeState();
        movement = core.GetCoreComponent(typeof(Movement)) as Movement;
    }

    public override void Enter()
    {
        base.Enter();
        entity.AnimToStateMachine.AttackState = this;
        isAnimationFinish = false;
        movement.SetVelocityX(0);
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        movement.SetVelocityX(0);
    }

    public virtual void TriggerAttack() { }

    public virtual void FinishAttack()
    {
        isAnimationFinish = true;
    }
}
