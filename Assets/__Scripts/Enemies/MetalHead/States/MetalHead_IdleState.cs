
    public class MetalHead_IdleState : IdleS
    {
        private MetalHead metalHead;
        private MetalHead_Data metalHeadData;
        public MetalHead_IdleState(Entity entity, StateMachine stateMachine, string animName, D_EntityData entityData) : base(entity, stateMachine, animName, entityData)
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
            pathFinding.SetTarget(NodeGraph.Instance.PlayerNode);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            pathFinding.SetTarget(NodeGraph.Instance.PlayerNode);
            if(CheckIfSwitchToMoveState()) return;
            if(CheckIfSwitchToJumpState()) return;
        }

        private bool CheckIfSwitchToJumpState()
        {
            if (pathFinding.Direction.y != 0 && pathFinding.CheckIfNextNodeIsInAir()  && pathFinding.CheckIfTargetNodeIsHigher(pathFinding.ReturnNodeToJumpTo()))
            {
                stateMachine.SwitchState(metalHead.JumpState);
            }
            return false;
        }

        private bool CheckIfSwitchToMoveState()
        {
            if (PathFinding.FindPath(pathFinding.CurrentNode, pathFinding.target) != null)
            {
                stateMachine.SwitchState(metalHead.MoveState);
                return true;
                
            }
            return false;
            
        }
    }