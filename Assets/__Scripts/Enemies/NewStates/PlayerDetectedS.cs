

    public class PlayerDetectedS : State
    {
        protected D_EntityData entityData;
        protected bool isPlayerInMinAgroRange;
        protected bool isPlayerInMaxAgroRange;
        protected bool performCloseRangeAction;

        protected Movement movement;
        protected CollisionSenses collisionSenses;
        public PlayerDetectedS(Entity entity, StateMachine stateMachine, string animName,D_EntityData entityData) : base(entity, stateMachine, animName)
        {
            this.entityData = entityData;
        }

        public override void initializeState()
        {
            base.initializeState();
            movement = core.GetCoreComponent(typeof(Movement)) as Movement;
            collisionSenses = core.GetCoreComponent(typeof(CollisionSenses)) as CollisionSenses;
        }

        public override void DoChecks()
        {
            base.DoChecks();
            isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
            isPlayerInMaxAgroRange = entity.CheckPlayerInMaxAgroRange();
            performCloseRangeAction = entity.CheckPlayerInCloseRangeAction();
        }
    }
