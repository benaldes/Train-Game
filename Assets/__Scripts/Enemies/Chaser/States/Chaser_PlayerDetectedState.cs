
    using UnityEngine;

    public class Chaser_PlayerDetectedState : PlayerDetectedS
    {
        private Chaser chaser;
        private Chaser_Data chaserData;
        
        public Chaser_PlayerDetectedState(Entity entity, StateMachine stateMachine, string animName, D_EntityData entityData) : base(entity, stateMachine, animName, entityData)
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

        public override void Enter()
        {
            base.Enter();
            movement.SetVelocityZero();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if(CheckIfSwitchToMeleeAttackState()) return;
            if(CheckIfSwitchToIdleState()) return;
            //if(CheckIfSwitchToChargeState())return;
        }

        private bool CheckIfSwitchToMeleeAttackState()
        {
            if (performCloseRangeAction)
            {
                stateMachine.SwitchState(chaser.MeleeAttackState);
                return true;
            }
            return false;
        }
        private bool CheckIfSwitchToChargeState()
        {
            if (isPlayerInMaxAgroRange && Time.time > chaserData.DetectedStateTime + startTime && collisionSenses.CheckIfGrounded())
            {
                stateMachine.SwitchState(chaser.ChargeState);
                return true;
            }
            return false;
        }

        private bool CheckIfSwitchToIdleState()
        {
            if (!isPlayerInMaxAgroRange && Time.time > chaserData.DetectedStateTime + startTime)
            {
                stateMachine.SwitchState(chaser.IdleState);
            }
            return false;
        }
    }
