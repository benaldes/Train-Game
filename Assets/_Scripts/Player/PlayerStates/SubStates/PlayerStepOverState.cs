using UnityEngine;

    public class PlayerStepOverState : PlayerState
    {
        
        private Vector2 detectedPos;
        private Vector2 cornerPos;
        private Vector2 startPos;
        private Vector2 stopPos;
        private Vector2 workspace;
        
        private static readonly int StepOver = Animator.StringToHash("StepOver");
        
        public PlayerStepOverState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animName) : base(player, stateMachine, playerData, animName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            
            core.Movement.SetVelocityZero();
            player.transform.position = detectedPos;
            player.StepOverState.SetCornerPosition();
            
            startPos.Set(cornerPos.x - (core.Movement.FacingDirection * playerData.StepOverStartOffSet.x),cornerPos.y - playerData.StepOverStartOffSet.y);
            stopPos.Set(cornerPos.x + (core.Movement.FacingDirection * playerData.StepOverStopOffSet.x), cornerPos.y + playerData.StepOverStopOffSet.y);

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
            core.Movement.SetVelocityZero();
            player.transform.position = startPos;
        }

        public void SetCornerPosition()
        {
            cornerPos = DetermineCornerPosition();
        }

        public void SetDetectedPosition(Vector2 pos) => detectedPos = pos;
    
        public Vector2 DetermineCornerPosition()
        {
            var ledgeFeetPos = core.CollisionSenses.LedgeFeetCheck.position;
            var wallPos = core.CollisionSenses.WallCheck.position;
        
            RaycastHit2D xHit = Physics2D.Raycast(ledgeFeetPos, Vector2.right * core.Movement.FacingDirection,
                core.CollisionSenses.WallCheckDistance, core.CollisionSenses.WhatIsGround);
            float xDist = xHit.distance;
            
            workspace.Set((xDist + 0.1f) * core.Movement.FacingDirection,0f);
        
            RaycastHit2D yHit = Physics2D.Raycast(wallPos + (Vector3)(workspace), Vector2.down,
                wallPos.y - ledgeFeetPos.y + 0.1f, core.CollisionSenses.WhatIsGround);
            
            float yDist = yHit.distance;
        
            workspace.Set(ledgeFeetPos.x + (xDist * core.Movement.FacingDirection),wallPos.y - yDist);
            
            // for Edge check
            //core.CollisionSenses.TestBall.transform.position = workspace;
        
            return workspace;
        }
    }
