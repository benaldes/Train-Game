using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackState : AttackState
{
    protected D_MeleeAttackData stateData;

    protected AttackDetails AttackDetails;
 
    public MeleeAttackState(Entity entity, StateMachine stateMachine, string animName, Transform attackPosition, D_MeleeAttackData stateData) : base(entity, stateMachine, animName, attackPosition)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();
        AttackDetails.DamageAmount = stateData.AttackDamage;
        AttackDetails.Position = Entity.aliveGO.transform.position;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void TriggerAttack()
    {
        base.TriggerAttack();
        Collider2D[] detectedObject =
            Physics2D.OverlapCircleAll(attackPosition.position, stateData.AttackRadius, stateData.whatIsPlayer);

        foreach (Collider2D  collider in detectedObject)
        {
            
        }
    }
}
