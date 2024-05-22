
    public class MoveS : State
    {
        protected D_EntityData entityData;
        public MoveS(Entity entity, StateMachine stateMachine, string animName,D_EntityData entityData) : base(entity, stateMachine, animName)
        {
            this.entityData = entityData;
        }
    }
