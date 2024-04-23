
    using UnityEngine;

    public class E1_MeleeAttackState : MeleeAttackState
    {
        private Enemy1 enemy;
        public E1_MeleeAttackState(Entity entity, StateMachine stateMachine, string animName, Transform attackPosition, D_MeleeAttack state,Enemy1 enemy) : base(entity, stateMachine, animName, attackPosition, state)
        {
            this.enemy = enemy;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (isAnimationFinish)
            {
                if (isPlayerInMinAgroRange)
                {
                    stateMachine.SwitchState(enemy.PlayerDetectedState);
                }
                else
                {
                    stateMachine.SwitchState(enemy.LookForPlayerState);
                }
            }
        }
        
    }
