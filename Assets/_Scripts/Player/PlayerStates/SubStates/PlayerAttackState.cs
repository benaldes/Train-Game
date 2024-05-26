
public class PlayerAttackState : PlayerAbilityState
{
    #region Variables

    private Weapon weapon;
    
    private float velocityToSet;
    
    private bool setVelocity;
    private bool shouldCheckFlip;

    #endregion
    
    public PlayerAttackState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animName) 
        : base(player, stateMachine, playerData, animName)
    {
    }

    
    public override void Enter()
    {
        base.Enter();

        setVelocity = false;
        weapon.EnterWeapon();
    }

    public override void Exit()
    {
        base.Exit();
        weapon.ExitWeapon();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        if (shouldCheckFlip)
        {
            movement.CheckIfShouldFlip(xInput);
        }
        
        if (setVelocity)
        {
            movement.SetVelocityX(velocityToSet * movement.FacingDirection);
        }
    }

    public void SetWeapon(Weapon weapon)
    {
        this.weapon = weapon;
        weapon.InitializeWeapon(this,core);
    }

    public void SetPlayerVelocity(float velocity)
    {
        movement.SetVelocityX(velocity * movement.FacingDirection);
        velocityToSet = velocity;
        setVelocity = true;
    }

    public void SetFlipCheck(bool value)
    {
        shouldCheckFlip = value;
    }

    #region Animation Triggers

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        isAbilityDone = true;
    }

    #endregion
}
