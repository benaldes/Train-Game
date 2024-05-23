

    public class PlayerDetectedS : State
    {
        protected D_EntityData entityData;
        protected bool isPlayerInMinAgroRange;
        protected bool isPlayerInMaxAgroRange;
        public PlayerDetectedS(Entity entity, StateMachine stateMachine, string animName,D_EntityData entityData) : base(entity, stateMachine, animName)
        {
            this.entityData = entityData;
        }

        public override void DoChecks()
        {
            base.DoChecks();
            isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
            isPlayerInMaxAgroRange = entity.CheckPlayerInMaxAgroRange();
        }
    }
