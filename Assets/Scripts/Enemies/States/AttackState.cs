using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    protected Transform attackPosition;

    protected bool isAnimationFinish;
    protected bool isPlayerInMinAgroRange;
    public AttackState(Entity entity, StateMachine stateMachine, string animName, Transform attackPosition) : base(entity, stateMachine, animName)
    {
        this.attackPosition = attackPosition;
    }

    public override void Enter()
    {
        base.Enter();
        Entity.AnimToStateMachine.AttackState = this;
        isAnimationFinish = false;
        core.Movement.SetVelocityX(0);
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isPlayerInMinAgroRange = Entity.CheckPlayerInMinAgroRange();
    }

    public virtual void TriggerAttack()
    {
        
    }

    public virtual void FinishAttack()
    {
        isAnimationFinish = true;
    }
}
