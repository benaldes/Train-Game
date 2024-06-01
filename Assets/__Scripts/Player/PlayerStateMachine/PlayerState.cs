using UnityEngine;

public class PlayerState
{
    #region Variables

    protected Core core;
    protected Player player;
    protected PlayerStateMachine stateMachine;
    protected PlayerData playerData;
    
    protected bool isAnimationFinished;
    protected bool isExitingState;
    
    protected int animName;
    
    protected float StartTime;

    //Input Variables
    protected int xInput;
    protected int yInput;
    protected bool jumpInput;
    protected bool rollInput;
    protected bool jumpInputStop;
    
    #endregion

    public PlayerState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animName)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.playerData = playerData;
        this.animName = Animator.StringToHash(animName);
        core = player.Core;
    }
    public virtual void initializeState() { }
    public virtual void Enter()
    {
        DoChecks();
        player.Animator.SetBool(animName, true);
        StartTime = Time.time;
        isAnimationFinished = false;
        isExitingState = false;
    }
    public virtual void Exit()
    {
        player.Animator.SetBool(animName,false);
        isExitingState = true;
    }

    public virtual void LogicUpdate()
    {
        InputUpdateCheck();
    }

    public virtual void PhysicsUpdate()
    {
        DoChecks();
    }
    public virtual void DoChecks(){}
    public virtual void AnimationTrigger(){}
    public virtual void AnimationFinishTrigger() => isAnimationFinished = true;

    public void InputUpdateCheck()
    {
        xInput = player.InputHandler.NormInputX;
        yInput = player.InputHandler.NormInputY;
        jumpInput = player.InputHandler.JumpInput;
        rollInput = player.InputHandler.RollInput;
        jumpInputStop = player.InputHandler.JumpInputStop;
    }


}
