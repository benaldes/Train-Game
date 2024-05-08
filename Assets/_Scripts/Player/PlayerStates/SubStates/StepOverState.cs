using UnityEngine;

    public class StepOverState : PlayerState
    {
        private Vector2 detectedPos;
        private Vector2 cornerPos;
        private Vector2 startPos;
        private Vector2 stopPos;
        private Vector2 workspace;
        
        private static readonly int ClimbLedge = Animator.StringToHash("StepOver");
        
        public StepOverState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animName) : base(player, stateMachine, playerData, animName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            core.Movement.SetVelocityZero();
            
        }
    }
