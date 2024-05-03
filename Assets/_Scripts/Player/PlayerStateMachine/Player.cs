using UnityEngine;

public class Player : MonoBehaviour
{
    #region State Variables
    public PlayerStateMachine StateMachine { get; private set; }
    public PlayerIdleState IdleState{ get; private set; }
    public PlayerMoveState MoveState{ get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerInAirState InAirState { get; private set; }
    public PlayerLandState LandState { get; private set; }
    public PlayerWallClimbState WallClimbState { get; private set; }
    public PlayerWallGrabState WallGrabState { get; private set; }
    public PlayerWallSlideState WallSlideState { get; private set; }
    public PlayerWallJumpState WallJumpState { get; private set; }
    public PlayerLedgeClimbState LedgeClimbState { get; private set; }
    public PlayerRollState RollState { get; private set; }
    public PlayerAttackState PrimaryAttackState { get; private set; }
    public PlayerAttackState SecondaryAttackState { get; private set; }
    
    #endregion
    
    #region Components
    public Core Core { get; private set; }
    public Animator Animator { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
    public Rigidbody2D Rb { get; private set; }
    [SerializeField] 
    private PlayerData PlayerData;
    public PlayerInventory Inventory { get; private set; }
    #endregion

    #region Other Variables
    private Vector2 workspace;
    #endregion

    #region Unity Callback Functions
    private void Awake()
    {
        Core = GetComponentInChildren<Core>();
        
        // turn Animation names to hashcode!!!!
        StateMachine = new PlayerStateMachine();
        IdleState = new PlayerIdleState(this, StateMachine, PlayerData, "Idle");
        MoveState = new PlayerMoveState(this, StateMachine, PlayerData, "Move");
        JumpState = new PlayerJumpState(this, StateMachine, PlayerData, "InAir");
        InAirState = new PlayerInAirState(this, StateMachine, PlayerData, "InAir");
        LandState = new PlayerLandState(this, StateMachine, PlayerData, "Land");
        WallClimbState = new PlayerWallClimbState(this, StateMachine, PlayerData, "WallClimb");
        WallGrabState = new PlayerWallGrabState(this, StateMachine, PlayerData, "WallGrab");
        WallSlideState = new PlayerWallSlideState(this, StateMachine, PlayerData, "WallSlide");
        WallJumpState = new PlayerWallJumpState(this, StateMachine, PlayerData, "InAir");
        LedgeClimbState = new PlayerLedgeClimbState(this, StateMachine, PlayerData, "LedgeClimbState");
        RollState = new PlayerRollState(this, StateMachine, PlayerData, "RollState");
        PrimaryAttackState = new PlayerAttackState(this, StateMachine, PlayerData, "Attack");
        PrimaryAttackState = new PlayerAttackState(this, StateMachine, PlayerData, "Attack");

    }

    private void Start()
    {
        Animator = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
        Rb = GetComponent<Rigidbody2D>();
        Inventory = GetComponent<PlayerInventory>();
        Inventory.SendAttackState(PrimaryAttackState);
        
        PrimaryAttackState.SetWeapon(Inventory.weapons[(int)CombatInputs.primary]);
        
        StateMachine.Initialize(IdleState);
        
    }

    private void Update()
    {
        Core.LogicUpdate();
        StateMachine.CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }
    #endregion
    
    #region Other Functions

    
    private void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();
    private void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();
    
    
    #endregion
    
}