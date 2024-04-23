using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackState : AttackState
{
    protected D_MeleeAttack State;

    protected AttackDetails AttackDetails;
 
    public MeleeAttackState(Entity entity, StateMachine stateMachine, string animName, Transform attackPosition, D_MeleeAttack state) : base(entity, stateMachine, animName, attackPosition)
    {
        this.State = state;
    }

    public override void Enter()
    {
        base.Enter();
        AttackDetails.DamageAmount = State.AttackDamage;
        AttackDetails.Position = Entity.transform.position;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void TriggerAttack()
    {
        base.TriggerAttack();
        Collider2D[] detectedObject =
            Physics2D.OverlapCircleAll(attackPosition.position, State.AttackRadius, State.whatIsPlayer);

        foreach (Collider2D  collider in detectedObject)
        {
            
        }
    }
}
