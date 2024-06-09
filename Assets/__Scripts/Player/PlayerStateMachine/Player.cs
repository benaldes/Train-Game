using System.Collections.Generic;
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
    public PlayerWallSlideState WallSlideState { get; private set; }
    public PlayerWallJumpState WallJumpState { get; private set; }
    public PlayerLedgeClimbState LedgeClimbState { get; private set; }
    public PlayerRollState RollState { get; private set; }
    public PlayerStepOverState StepOverState { get; private set; }
    public PlayerAttackState PrimaryAttackState { get; private set; }
    public PlayerAttackState SecondaryAttackState { get; private set; }
    
    #endregion
    
    #region Components
    [field: SerializeField, HideInInspector] public Core Core { get; private set; }
	[field: SerializeField, HideInInspector] public Animator Animator { get; private set; }
	[field: SerializeField, HideInInspector] public PlayerInputHandler InputHandler { get; private set; }
	[field: SerializeField, HideInInspector] public Rigidbody2D Rb { get; private set; }
    [SerializeField] private PlayerData PlayerData;
	[field: SerializeField, HideInInspector] public PlayerInventory Inventory { get; private set; }

	private List<PlayerState> stateList = new List<PlayerState>();

	private PathFindingComponent pathFinding;
    #endregion

    #region Other Variables
    private Vector2 workspace;
	#endregion

	#region Unity Callback Functions
	private void OnValidate()
	{
		Core = GetComponentInChildren<Core>();
		Animator = GetComponent<Animator>();
		InputHandler = GetComponent<PlayerInputHandler>();
		Rb = GetComponent<Rigidbody2D>();
		Inventory = GetComponent<PlayerInventory>();
	}

	private void Awake()
    {
	    StateMachine = new PlayerStateMachine();
        stateList.Add(IdleState = new PlayerIdleState(this, StateMachine, PlayerData, "Idle"));
        stateList.Add(MoveState = new PlayerMoveState(this, StateMachine, PlayerData, "Move"));
        stateList.Add(JumpState = new PlayerJumpState(this, StateMachine, PlayerData, "InAir"));
        stateList.Add(InAirState = new PlayerInAirState(this, StateMachine, PlayerData, "InAir"));
        stateList.Add(LandState = new PlayerLandState(this, StateMachine, PlayerData, "Land"));
        stateList.Add(WallSlideState = new PlayerWallSlideState(this, StateMachine, PlayerData, "WallSlide"));
        stateList.Add(WallJumpState = new PlayerWallJumpState(this, StateMachine, PlayerData, "InAir"));
        stateList.Add(LedgeClimbState = new PlayerLedgeClimbState(this, StateMachine, PlayerData, "LedgeClimbState"));
        stateList.Add(RollState = new PlayerRollState(this, StateMachine, PlayerData, "RollState"));
        stateList.Add(StepOverState = new PlayerStepOverState(this, StateMachine, PlayerData, "StepOver"));
        stateList.Add(PrimaryAttackState = new PlayerAttackState(this, StateMachine, PlayerData, "Attack"));
        stateList.Add(PrimaryAttackState = new PlayerAttackState(this, StateMachine, PlayerData, "Attack"));
        
    }

    private void Start()
    {
	    InitializeStates();

	    Core.InitializeCoreComponents();
	    
	    UpdateCurrentNode();
	    
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
	    Core.PhysicsUpdate();
        StateMachine.CurrentState.PhysicsUpdate();
        UpdateCurrentNode();
    }
    #endregion
    
    #region Other Functions

    private void InitializeStates()
    {
	    foreach (PlayerState state in stateList)
	    {
		    state.initializeState();
	    }
	    
	    pathFinding = Core.GetCoreComponent(typeof(PathFindingComponent)) as PathFindingComponent;
    }

    
    private void UpdateCurrentNode()
    {
	    float distance = Vector3.Distance(pathFinding.CurrentNode.WorldPosition, gameObject.transform.position);
	    if(distance < 0.5f) return;
	    
	    Node playerNode = pathFinding.FindClosestNode(gameObject);
	    if (playerNode.TileType is TileType.aboveGround )
	    {
		    NodeGraph.Instance.SetPlayerNode(playerNode);
	    }
	    
    }
    private void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();
    private void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();
    
    
    #endregion
    
}
