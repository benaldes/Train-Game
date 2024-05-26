public class PlayerJumpState : PlayerAbilityState
{
    private int amountsOfJumpsLeft;
    public PlayerJumpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animName) : base(player, stateMachine, playerData, animName)
    {
        amountsOfJumpsLeft = playerData.amountsOfJumps;
    }

    public override void Enter()
    {
        base.Enter();
        movement.SetVelocityY(playerData.JumpVelocity);
        isAbilityDone = true;
        DecreaseAmountOfJumpsLeft();
        player.InputHandler.UseJumpInput();
        player.InAirState.SetIsJumping();
    }
    

    public bool canJump() => amountsOfJumpsLeft > 0;
    
    public void ResetAmountsOfJumpsLeft() => amountsOfJumpsLeft = playerData.amountsOfJumps;
    
    public void DecreaseAmountOfJumpsLeft() => amountsOfJumpsLeft--;
    
}
