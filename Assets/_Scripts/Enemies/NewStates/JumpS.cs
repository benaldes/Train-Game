
    public class JumpS : State
    {
        protected D_EntityData entityData;
        public JumpS(Entity entity, StateMachine stateMachine, string animName,D_EntityData entityData) : base(entity, stateMachine, animName)
        {
            this.entityData = entityData;
        }
    }
