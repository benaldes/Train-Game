using UnityEngine;

    public class PlayerStepOverState : PlayerState
    {
        
        private Vector2 detectedPos;
        private Vector2 cornerPos;
        private Vector2 startPos;
        private Vector2 stopPos;
        private Vector2 workspace;
        
        private static readonly int StepOver = Animator.StringToHash("StepOver");
        
        private Movement movement;
        private CollisionSenses collisionSenses;
        
        public PlayerStepOverState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animName) : base(player, stateMachine, playerData, animName)
        {
        }

        public override void initializeState()
        {
            base.initializeState();
            movement = core.GetCoreComponent(typeof(Movement)) as Movement;
            collisionSenses = core.GetCoreComponent(typeof(CollisionSenses)) as CollisionSenses;
        }

        public override void Enter()
        {
            base.Enter();
            
            movement.SetVelocityZero();
            player.transform.position = detectedPos;
            player.StepOverState.SetCornerPosition();
            
            startPos.Set(cornerPos.x - (movement.FacingDirection * playerData.StepOverStartOffSet.x),cornerPos.y - playerData.StepOverStartOffSet.y);
            stopPos.Set(cornerPos.x + (movement.FacingDirection * playerData.StepOverStopOffSet.x), cornerPos.y + playerData.StepOverStopOffSet.y);

            player.transform.position = startPos;
            
        }

        public override void Exit()
        {
            base.Exit();
            player.transform.position = stopPos;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if(CheckIfSwitchToIdleState()) return;

            SetPLayerPosAndVelocityToFitAnimation();
        }

        public override void AnimationFinishTrigger()
        {
            base.AnimationFinishTrigger();
            player.Animator.SetBool(StepOver, false);
        }

        private bool CheckIfSwitchToIdleState()
        {
            if (isAnimationFinished)
            {
                stateMachine.SwitchState(player.IdleState);
                return true;
            }

            return false;
        }
        
        private void SetPLayerPosAndVelocityToFitAnimation()
        {
            movement.SetVelocityZero();
            player.transform.position = startPos;
        }

        public void SetCornerPosition()
        {
            cornerPos = DetermineCornerPosition();
        }

        public void SetDetectedPosition(Vector2 pos) => detectedPos = pos;
    
        public Vector2 DetermineCornerPosition()
        {
            var ledgeFeetPos = collisionSenses.LedgeFeetCheck.position;
            var wallPos = collisionSenses.WallCheck.position;
        
            RaycastHit2D xHit = Physics2D.Raycast(ledgeFeetPos, Vector2.right * movement.FacingDirection,
                collisionSenses.WallCheckDistance, collisionSenses.WhatIsGround);
            float xDist = xHit.distance;
            
            workspace.Set((xDist + 0.1f) * movement.FacingDirection,0f);
        
            RaycastHit2D yHit = Physics2D.Raycast(wallPos + (Vector3)(workspace), Vector2.down,
                wallPos.y - ledgeFeetPos.y + 0.1f, collisionSenses.WhatIsGround);
            
            float yDist = yHit.distance;
        
            workspace.Set(ledgeFeetPos.x + (xDist * movement.FacingDirection),wallPos.y - yDist);
            
            // for Edge check
            //core.CollisionSenses.TestBall.transform.position = workspace;
        
            return workspace;
        }
    }
