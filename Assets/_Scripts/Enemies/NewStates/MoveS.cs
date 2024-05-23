
    public class MoveS : State
    {
        protected D_EntityData entityData;
        protected bool IsPlayerInMinAgroRange;
        public MoveS(Entity entity, StateMachine stateMachine, string animName,D_EntityData entityData) : base(entity, stateMachine, animName)
        {
            this.entityData = entityData;
        }
        public override void DoChecks()
        {
            base.DoChecks();
            IsPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
        }
    }
