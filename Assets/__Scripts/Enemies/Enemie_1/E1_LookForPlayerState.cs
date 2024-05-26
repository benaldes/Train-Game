
    public class E1_LookForPlayerState: LookForPlayerState
    {
        private Enemy1 enemy;
        public E1_LookForPlayerState(Entity entity, StateMachine stateMachine, string animName, D_LookForPlayerState stateData, Enemy1 enemy) : base(entity, stateMachine, animName, stateData)
        {
            this.enemy = enemy;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (isPlayerInMinAgroRange)
            {
                stateMachine.SwitchState(enemy.PlayerDetectedState);
            }
            else if (isAllTurnsTimeDone)
            {
                stateMachine.SwitchState(enemy.MoveState);
            }
        }
    }
