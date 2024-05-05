using UnityEngine;

public class MeleeAttackState : AttackState
{
    protected D_MeleeAttack stateData;
    
    public MeleeAttackState(Entity entity, StateMachine stateMachine, string animName, Transform attackPosition, D_MeleeAttack stateData) : base(entity, stateMachine, animName, attackPosition)
    {
        this.stateData = stateData;
    }
    
    public override void TriggerAttack()
    {
        base.TriggerAttack();
        
        Collider2D[] detectedObject = Physics2D.OverlapCircleAll(attackPosition.position, stateData.AttackRadius, stateData.whatIsPlayer);

        foreach (Collider2D  collider in detectedObject)
        {
            Idamageble damageable = collider.GetComponent<Idamageble>();

            if (damageable != null)
            {
                damageable.Damage(stateData.AttackDamage);
                    
            }
            
            IKnockbackable knockbackable = collider.GetComponent<IKnockbackable>();

            if (knockbackable != null)
            {
                knockbackable.Knockback(stateData.KnockbackAngle,stateData.KnockbackStrength,core.Movement.FacingDirection);
            }
        }
    }
}
