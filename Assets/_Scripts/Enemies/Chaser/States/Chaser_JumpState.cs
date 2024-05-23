using UnityEngine;
    public class Chaser_JumpState : JumpS
    {
        private Chaser chaser;
        private Chaser_Data chaserData;
        
        private Vector2 direction;
        
        public Chaser_JumpState(Entity entity, StateMachine stateMachine, string animName, D_EntityData entityData) : base(entity, stateMachine, animName, entityData)
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
            core.Movement.SetVelocityX(0);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if(CheckIfSwitchToMoveState()) return;
            if(CheckIfSwitchToIdleState()) return;
            
            core.Movement.SetVelocityY(chaserData.JumpVelocity * direction.y);
        }
        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            //TODO : need to change it so you dont have to calculate a new path every time you want a direction
            core.PathFindingComponent.FindPath(entity.gameObject, NodeGraph.Instance.PlayerNode);
            direction = core.PathFindingComponent.ReturnNextNodeDirection();
        }

        private bool CheckIfSwitchToMoveState()
        {
            if (direction.y == 0 && direction.x != 0)
            {
                stateMachine.SwitchState(chaser.MoveState);
                return true;
            }
            return false;
        }

        private bool CheckIfSwitchToIdleState()
        {
            if (direction is { y: 0, x: 0 })
            {
                stateMachine.SwitchState(chaser.IdleState);
                return true;
            }
            return false;
        }
    }
