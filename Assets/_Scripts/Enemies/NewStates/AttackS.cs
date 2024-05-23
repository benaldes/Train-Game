using UnityEngine;


public class AttackS : State
{
    protected D_EntityData entityData;
    protected Transform attackPosition;

    protected bool isAnimationFinish;
    protected bool isPlayerInMinAgroRange;
    
    public AttackS(Entity entity, StateMachine stateMachine, string animName, D_EntityData entityData, Transform attackPosition) : base(entity, stateMachine, animName)
    {
        this.entityData = entityData;
        this.attackPosition = attackPosition;
    }
    public override void Enter()
    {
        base.Enter();
        entity.AnimToStateMachine.AttackS = this;
        isAnimationFinish = false;
        core.Movement.SetVelocityX(0);
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        core.Movement.SetVelocityX(0);
    }

    public virtual void TriggerAttack() { }

    public virtual void FinishAttack()
    {
        isAnimationFinish = true;
    }
}
