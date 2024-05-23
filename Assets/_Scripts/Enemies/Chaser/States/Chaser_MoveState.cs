using UnityEngine;
    public class Chaser_MoveState : MoveS
    {
        private Chaser chaser;
        private Chaser_Data chaserData;
        private Vector2 direction;
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
            
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            core.Movement.CheckIfShouldFlip(direction.x);
            
            if(CheckIfSwitchToJumpState()) return;
            if(CheckIfSwitchToPlayerDetectedState())return;
            
            core.Movement.SetVelocityX(chaserData.MovementSpeed * direction.x);
        }


        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            //TODO : need to change it so you dont have to calculate a new path every time you want a direction
            core.PathFindingComponent.FindPath(entity.gameObject, NodeGraph.Instance.PlayerNode);
            direction = core.PathFindingComponent.ReturnNextNodeDirection();
        }

        private bool CheckIfSwitchToJumpState()
        {
            if (direction.y != 0)
            {
                stateMachine.SwitchState(chaser.JumpState);
                return true;
            }

            return false;
        }

        private bool CheckIfSwitchToPlayerDetectedState()
        {
            if (IsPlayerInMinAgroRange && core.CollisionSenses.CheckIfGrounded())
            {
                stateMachine.SwitchState(chaser.PlayerDetectedState);
                return true;
            }

            return false;
        }

        
    }
