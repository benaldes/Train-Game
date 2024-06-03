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
            pathFinding.FindPath(pathFinding.CurrentNode, NodeGraph.Instance.PlayerNode);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if(CheckIfSwitchToJumpState()) return;
            if(CheckIfSwitchToMeleeAttackState())return;
            if(CheckIfSwitchToIdleState()) return;
            //if(CheckIfSwitchToPlayerDetectedState())return;
            
            movement.SetVelocityX(chaserData.MovementSpeed * direction.x);
            movement.CheckIfShouldFlip(direction.x);
        }
        private bool CheckIfSwitchToMeleeAttackState()
        {
            if (performCloseRangeAction)
            {
                stateMachine.SwitchState(chaser.MeleeAttackState);
                return true;
            }
            return false;
        }

        private bool CheckIfSwitchToJumpState()
        {
            if (pathFinding.CheckIfNextNodeIsInAir()  && pathFinding.CheckIfTargetNodeIsHigher(pathFinding.ReturnNodeToJumpTo())&& direction.y != 0 && collisionSenses.CheckIfGrounded())
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

        public override void DoChecks()
        {
            base.DoChecks();
            direction = pathFinding.ReturnLastNodeDirection();
        }

        private bool CheckIfSwitchToIdleState()
        {
            if (direction is { x: 0, y: 0 } && collisionSenses.CheckIfGrounded())
            {
                stateMachine.SwitchState(chaser.IdleState);
                return true;
            }
            return false;
        }

        
    }
