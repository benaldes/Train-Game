using UnityEngine;
    public class Chaser_MoveState : MoveS
    {
        private Chaser chaser;
        private Chaser_Data chaserData;
        public Chaser_MoveState(Entity entity, StateMachine stateMachine, string animName, D_EntityData entityData) : base(entity, stateMachine, animName, entityData)
        {
            if (entity.GetType() == typeof(Chaser))
            {
                chaser = (Chaser)entity;
            }
            if (entityData.GetType() == typeof(Chaser_Data))
            {
                chaserData = (Chaser_Data)entityData;
            }
        }

        public override void Enter()
        {
            base.Enter();
            Debug.Log("Enter Move State");
            
        }
    }
