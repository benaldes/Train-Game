using UnityEngine;

public class PlayerLedgeClimbState : PlayerState
{
    #region Variables
    
    private Vector2 detectedPos;
    private Vector2 cornerPos;
    private Vector2 startPos;
    private Vector2 stopPos;
    private Vector2 workspace;

    private bool isHanging;
    private bool isClimbing;
    
    private static readonly int ClimbLedge = Animator.StringToHash("ClimbLedge");
    
    private Movement movement;
    protected CollisionSenses collisionSenses;

    #endregion
    public PlayerLedgeClimbState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, 
        string animName) : base(player, stateMachine, playerData, animName) { }

    #region State Callback Functions

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
        
        cornerPos = DetermineCornerPosition();
        startPos.Set(cornerPos.x - (movement.FacingDirection * playerData.startOffSet.x),cornerPos.y - playerData.startOffSet.y);
        stopPos.Set(cornerPos.x + (movement.FacingDirection * playerData.stopOffSet.x), cornerPos.y + playerData.stopOffSet.y);

        player.transform.position = startPos;
        
        //TODO: need to replace this
        isClimbing = true;
        player.Animator.SetBool(ClimbLedge,true);
        //

    }

    public override void Exit()
    {
        base.Exit();
        isHanging = false;
        if (isClimbing)
        {
            player.transform.position = stopPos;
            isClimbing = false;
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(CheckIfSwitchToIdleState())return;
        
        SetPLayerPosAndVelocityToFitAnimation();

        if(CheckIfClimbOverLedge()) return;
        if(CheckIfDropFromLedge()) return;
        if(CheckIfSwitchToJumpState()) return;
        
    }
    
    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
        isHanging = true;
    }
    
    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        player.Animator.SetBool(ClimbLedge, false);
    }

    #endregion

    #region State Callback Functions

    private bool CheckIfSwitchToIdleState()
    {
        if (isAnimationFinished)
        {
            stateMachine.SwitchState(player.IdleState);
            return true;
        }

        return false;
    }
    
    private bool CheckIfClimbOverLedge()
    {
        if ((xInput == movement.FacingDirection || yInput == 1) && isHanging && !isClimbing)
        {
            isClimbing = true;
            player.Animator.SetBool(ClimbLedge,true);
            return true;
        }

        return false;
    }

    private bool CheckIfDropFromLedge()
    {
        if((yInput == -1 || xInput == -movement.FacingDirection) && isHanging && !isClimbing)
        {
            stateMachine.SwitchState(player.InAirState);
            return true;
        }

        return false;
    }
    
    private bool CheckIfSwitchToJumpState()
    {
        if(jumpInput && !isClimbing)
        {
            player.WallJumpState.DetermineWallJumpDirection(true);
            stateMachine.SwitchState(player.WallJumpState);
            return true;
        }
        return false;
    }
    
    #endregion

    #region other
    
    private void SetPLayerPosAndVelocityToFitAnimation()
    {
        movement.SetVelocityZero();
        player.transform.position = startPos;
    }
    
    public void SetDetectedPosition(Vector2 pos) => detectedPos = pos;
    
    public Vector2 DetermineCornerPosition()
    {
        var position = collisionSenses.WallCheck.position;
        var position1 = collisionSenses.HorizontalLedgeCheck.position;
        
        RaycastHit2D xHit = Physics2D.Raycast(position, Vector2.right * movement.FacingDirection,
            collisionSenses.WallCheckDistance, collisionSenses.WhatIsGround);
        float xDist = xHit.distance;
        
        workspace.Set((xDist + 0.1f) * movement.FacingDirection,0f);
        
        RaycastHit2D yHit = Physics2D.Raycast(position1 + (Vector3)(workspace), Vector2.down,
            position1.y - position.y + 0.1f , collisionSenses.WhatIsGround);
        float yDist = yHit.distance;
        
        workspace.Set(position.x + (xDist * movement.FacingDirection),position1.y - yDist);
        
        return workspace;
    }
    
    #endregion
    
    
}
