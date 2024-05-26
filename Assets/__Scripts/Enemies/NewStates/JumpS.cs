
    public class JumpS : State
    {
        protected D_EntityData entityData;

        protected Movement movement;
        protected PathFindingComponent pathFinding;
        protected CollisionSenses collisionSenses;
        public JumpS(Entity entity, StateMachine stateMachine, string animName,D_EntityData entityData) : base(entity, stateMachine, animName)
        {
            this.entityData = entityData;
        }

        public override void initializeState()
        {
            base.initializeState();
            movement = core.GetCoreComponent(typeof(Movement)) as Movement;
            pathFinding = core.GetCoreComponent(typeof(PathFindingComponent)) as PathFindingComponent;
            collisionSenses = core.GetCoreComponent(typeof(CollisionSenses)) as CollisionSenses;
        }
    }
