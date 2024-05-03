   public class E1_StunState : StunState
   {
       private Enemy1 enemy;
        public E1_StunState(Entity entity, StateMachine stateMachine, string animName, D_StunState state,Enemy1 enemy) : base(entity, stateMachine, animName, state)
        {
            this.enemy = enemy;
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (isStunTimeOver)
            {
                if (performCloseRangeAction)
                {
                    stateMachine.SwitchState(enemy.MeleeAttackState);
                }
                else if (isPlayerInMinAgroRange)
                {
                    stateMachine.SwitchState(enemy.ChargeState);
                }
                else
                {
                    enemy.LookForPlayerState.SetTurnImmediately(true);
                    stateMachine.SwitchState(enemy.LookForPlayerState);
                }
            }
        }
   }
