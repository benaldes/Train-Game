using UnityEngine;


public class AttackS : State
{
    protected D_EntityData entityData;
    protected Transform attackPosition;

    protected bool isAnimationFinish;
    protected bool isPlayerInMinAgroRange;

    protected Movement movement;
    
    public AttackS(Entity entity, StateMachine stateMachine, string animName, D_EntityData entityData, Transform attackPosition) : base(entity, stateMachine, animName)
    {
        this.entityData = entityData;
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
        entity.AnimToStateMachine.AttackS = this;
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
