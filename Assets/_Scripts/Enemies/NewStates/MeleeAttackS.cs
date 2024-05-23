
    using UnityEngine;

    public class MeleeAttackS : AttackS
    {
        private D_EntityData entityData;
        public MeleeAttackS(Entity entity, StateMachine stateMachine, string animName, D_EntityData entityData, Transform attackPosition) : base(entity, stateMachine, animName, entityData, attackPosition)
        {
            this.entityData = entityData;
        }
        public override void TriggerAttack()
        {
            base.TriggerAttack();
        
            Collider2D[] detectedObject = Physics2D.OverlapCircleAll(attackPosition.position, entityData.AttackRadius, entityData.whatIsPlayer);

            foreach (Collider2D  collider in detectedObject)
            {
                Idamageble damageable = collider.GetComponent<Idamageble>();

                if (damageable != null)
                {
                    damageable.Damage(entityData.AttackDamage);
                    
                }
            
                IKnockbackable knockbackable = collider.GetComponent<IKnockbackable>();

                if (knockbackable != null)
                {
                    knockbackable.Knockback(entityData.KnockbackAngle,entityData.KnockbackStrength,core.Movement.FacingDirection);
                }
            }
        }
    }
