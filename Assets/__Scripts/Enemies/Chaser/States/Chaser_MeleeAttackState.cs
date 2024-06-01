
    using UnityEngine;

    public class Chaser_MeleeAttackState : MeleeAttackS
    {
        private Chaser chaser;
        private Chaser_Data chaserData;
        public Chaser_MeleeAttackState(Entity entity, StateMachine stateMachine, string animName, D_EntityData entityData, Transform attackPosition) : base(entity, stateMachine, animName, entityData, attackPosition)
        {
            if (entity.GetType() == typeof(Chaser))
            {
                chaser = (Chaser)entity;
            }
            if (entityData.GetType() == typeof(Chaser_Data))
            {
                chaserData = (Chaser_Data)entityData;
            }
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (isAnimationFinish)
            {
                stateMachine.SwitchState(chaser.IdleState);
            }
        }

        private bool CheckIfSwitchToPlayerDetectedState()
        {
            if (isPlayerInMinAgroRange)
            {
                stateMachine.SwitchState(chaser.PlayerDetectedState);
                return true;
            }
            return false;
        }

        private bool CheckIfSwitchToIdleState()
        {
            if (!isPlayerInMinAgroRange)
            {
                stateMachine.SwitchState(chaser.IdleState);
                return true;
            }

            return false;
        }
    }
