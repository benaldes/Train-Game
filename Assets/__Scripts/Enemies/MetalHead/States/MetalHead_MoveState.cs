using UnityEngine;
    public class MetalHead_MoveState : MoveS
    {
        private MetalHead metalHead;
        private MetalHead_Data metalHeadData;
        private Vector2 direction;
        public MetalHead_MoveState(Entity entity, StateMachine stateMachine, string animName, D_EntityData entityData) : base(entity, stateMachine, animName, entityData)
        {
            if (entity.GetType() == typeof(MetalHead))
            {
                metalHead = (MetalHead)entity;
            }
            if (entityData.GetType() == typeof(MetalHead_Data))
            {
                metalHeadData = (MetalHead_Data)entityData;
            }
        }

        public override void Enter()
        {
            base.Enter();
            pathFinding.FindPath(pathFinding.CurrentNode, pathFinding.target);
            pathFinding.SetTarget(NodeGraph.Instance.HeartNode);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            pathFinding.SetTarget(NodeGraph.Instance.HeartNode);
            if(CheckIfSwitchToJumpState()) return;
            //if(CheckIfSwitchToMeleeAttackState())return;
            if(CheckIfSwitchToIdleState()) return;
            //if(CheckIfSwitchToPlayerDetectedState())return;
            
            movement.SetVelocityX(metalHeadData.MovementSpeed * direction.x);
            movement.CheckIfShouldFlip(direction.x);
        }
        public override void DoChecks()
        {
            base.DoChecks();
            direction = pathFinding.ReturnLastNodeDirection();
        }
        
        private bool CheckIfSwitchToJumpState()
        {
            if (pathFinding.CheckIfNextNodeIsInAir()  && pathFinding.CheckIfTargetNodeIsHigher(pathFinding.ReturnNodeToJumpTo())&& direction.y != 0 && collisionSenses.CheckIfGrounded())
            {
                stateMachine.SwitchState(metalHead.JumpState);
                return true;
            }

            return false;
        }
        private bool CheckIfSwitchToIdleState()
        {
            if (direction is { x: 0, y: 0 } && collisionSenses.CheckIfGrounded())
            {
                stateMachine.SwitchState(metalHead.IdleState);
                return true;
            }
            return false;
        }
    }
