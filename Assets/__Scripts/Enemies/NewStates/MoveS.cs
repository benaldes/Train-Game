
    public class MoveS : State
    {
        protected D_EntityData entityData;
        protected bool IsPlayerInMinAgroRange;
        
        protected Movement movement;
        protected CollisionSenses collisionSenses;
        protected PathFindingComponent pathFinding;
        public MoveS(Entity entity, StateMachine stateMachine, string animName,D_EntityData entityData) : base(entity, stateMachine, animName)
        {
            this.entityData = entityData;
        }

        public override void initializeState()
        {
            base.initializeState();
            movement = core.GetCoreComponent(typeof(Movement)) as Movement;
            collisionSenses = core.GetCoreComponent(typeof(CollisionSenses)) as CollisionSenses;
            pathFinding = core.GetCoreComponent(typeof(PathFindingComponent)) as PathFindingComponent;
        }

        public override void DoChecks()
        {
            base.DoChecks();
            IsPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
        }
    }
