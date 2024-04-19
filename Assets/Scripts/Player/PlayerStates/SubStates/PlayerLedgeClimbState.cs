using System.Collections;
using System.Collections.Generic;
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

    #endregion
    public PlayerLedgeClimbState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, 
        string animName) : base(player, stateMachine, playerData, animName)
    {
    }

    #region State Callback Functions

    public override void Enter()
    {
        base.Enter();
        
        core.Movement.SetVelocityZero();
        player.transform.position = detectedPos;
        
        cornerPos = DetermineCornerPosition();
        startPos.Set(cornerPos.x - (core.Movement.FacingDirection * playerData.startOffSet.x)
            ,cornerPos.y - playerData.startOffSet.y);
        stopPos.Set(cornerPos.x + (core.Movement.FacingDirection * playerData.stopOffSet.x)
            , cornerPos.y + playerData.stopOffSet.y);

        player.transform.position = startPos;

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
        player.Animator.SetBool(nameof(ClimbLedge), false);
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
        if ((xInput == core.Movement.FacingDirection || yInput == 1) && isHanging && !isClimbing)
        {
            isClimbing = true;
            player.Animator.SetBool(ClimbLedge,true);
            return true;
        }

        return false;
    }

    private bool CheckIfDropFromLedge()
    {
        if((yInput == -1 || xInput == -core.Movement.FacingDirection) && isHanging && !isClimbing)
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
        core.Movement.SetVelocityZero();
        player.transform.position = startPos;
    }
    
    public void SetDetectedPosition(Vector2 pos) => detectedPos = pos;
    
    public Vector2 DetermineCornerPosition()
    {
        var position = core.CollisionSenses.WallCheck.position;
        RaycastHit2D xHit = Physics2D.Raycast(position, Vector2.right * core.Movement.FacingDirection,
            core.CollisionSenses.WallCheckDistance, core.CollisionSenses.WhatIsGround);
        float xDist = xHit.distance;
        workspace.Set(xDist * core.Movement.FacingDirection,0f);
        var position1 = core.CollisionSenses.LedgeCheck.position;
        RaycastHit2D yHit = Physics2D.Raycast(position1 + (Vector3)(workspace), Vector2.down,
            position1.y - position.y, core.CollisionSenses.WhatIsGround);
        float yDist = yHit.distance;
        workspace.Set(position.x + (xDist * core.Movement.FacingDirection),position1.y - yDist);
        return workspace;
    }
    
    #endregion
    
    
}
