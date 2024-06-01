using UnityEngine;
    public class Chaser_JumpState : JumpS
    {
        private Chaser chaser;
        private Chaser_Data chaserData;

        private Node jumpTarget;
        private Vector2 direction;

        private float timeToPeek;
        
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
            movement.SetVelocityX(0);
            jumpTarget = pathFinding.ReturnNodeToJumpTo();
            timeToPeek = Time.time;
            timeToPeek += pathFinding.JumpToNode(jumpTarget,movement.RB);
            chaser.Collider2D.isTrigger = true;
            pathFinding.FindPath(pathFinding.CurrentNode, NodeGraph.Instance.PlayerNode);
            movement.CheckIfEnemyShouldFlip();
        }

        public override void Exit()
        {
            base.Exit();
            chaser.Collider2D.isTrigger = false;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (timeToPeek < Time.time)
            {
                CheckIfAboutToLend();
                if(CheckIfSwitchToMoveState()) return;
                if(CheckIfSwitchToIdleState()) return;
            }

        }

        private void CheckIfAboutToLend()
        {
            if (movement.CurrentVelocity.y <= 0)
            {
                chaser.Collider2D.isTrigger = false;
            }
        }

        private bool CheckIfSwitchToMoveState()
        {
            if (collisionSenses.CheckIfGrounded() && Mathf.Abs(movement.CurrentVelocity.y) < 0.015f)
            {
                stateMachine.SwitchState(chaser.MoveState);
                return true;
            }
            return false;
        }

        private bool CheckIfSwitchToIdleState()
        {
            if (pathFinding.Direction is { y: 0, x: 0 } && collisionSenses.CheckIfGrounded()&& Mathf.Abs(movement.CurrentVelocity.y) < 0.015f)
            {
                stateMachine.SwitchState(chaser.IdleState);
                return true;
            }
            return false;
        }
    }
