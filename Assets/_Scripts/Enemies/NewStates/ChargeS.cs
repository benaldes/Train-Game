
    public class ChargeS : State
    {
        protected D_EntityData entityData;
        protected bool performCloseRangeAction;
        public ChargeS(Entity entity, StateMachine stateMachine, string animName, D_EntityData entityData) : base(entity, stateMachine, animName)
        {
            this.entityData = entityData;
        }

        public override void DoChecks()
        {
            base.DoChecks();
            performCloseRangeAction = entity.CheckPlayerInCloseRangeAction();
        }
    }
