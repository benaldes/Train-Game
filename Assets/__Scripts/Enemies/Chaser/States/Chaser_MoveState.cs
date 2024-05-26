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
        
        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if(CheckIfSwitchToJumpState()) return;
            if(CheckIfSwitchToPlayerDetectedState())return;
            
            movement.SetVelocityX(chaserData.MovementSpeed * direction.x);
            movement.CheckIfShouldFlip(direction.x);
        }
        
        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            //TODO : need to change it so you dont have to calculate a new path every time you want a direction
            pathFinding.FindPath(pathFinding.currentNode, NodeGraph.Instance.PlayerNode);
            direction = pathFinding.ReturnNextNodeDirection();
        }

        private bool CheckIfSwitchToJumpState()
        {
            if (pathFinding.CheckIfNextNodeIsInAir() && direction.y != 0)
            {
                stateMachine.SwitchState(chaser.JumpState);
                return true;
            }

            return false;
        }

        private bool CheckIfSwitchToPlayerDetectedState()
        {
            if (IsPlayerInMinAgroRange && collisionSenses.CheckIfGrounded())
            {
                stateMachine.SwitchState(chaser.PlayerDetectedState);
                return true;
            }
            return false;
        }

        private bool CheckIfSwitchToIdleState()
        {
            if (direction.x == 0 && direction.y == 0)
            {
                stateMachine.SwitchState(chaser.IdleState);
                return true;
            }
            return false;
        }

        
    }
