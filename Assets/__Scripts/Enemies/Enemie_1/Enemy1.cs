using UnityEngine;

public class Enemy1 : Entity
{
    public E1_IdleState IdleState { get; private set; }
    public E1_MoveState MoveState { get; private set; }
    public E1_PlayerDetectedState PlayerDetectedState { get; private set; }
    public E1_ChargeState ChargeState { get; private set; }
    public E1_LookForPlayerState LookForPlayerState { get; private set; }
    public E1_MeleeAttackState MeleeAttackState { get; private set; }
    public E1_StunState StunState { get; private set; }
    public E1_DeadState DeadState { get; private set; }
    
    
    [SerializeField] private D_IdleState idleStateData;
    [SerializeField] private D_MoveState moveStateData;
    [SerializeField] private D_PlayerDetected playerDetectedStateData;
    [SerializeField] private D_ChargeState chargeStateData;
    [SerializeField] private D_LookForPlayerState lookForPlayerStateData;
    [SerializeField] private D_MeleeAttack meleeAttackState;
    [SerializeField] private D_StunState stunStateData;
    [SerializeField] private D_DeadState deadStateData;
    
    [SerializeField] private Transform meleeAttackPosition;

    private Stats stats;
    

    public override void Awake()
    {
        base.Awake();
        stateList.Add(MoveState = new E1_MoveState(this, StateMachine, "Move", moveStateData,this));
        stateList.Add(IdleState = new E1_IdleState(this, StateMachine, "Idle", idleStateData, this));
        stateList.Add(PlayerDetectedState = new E1_PlayerDetectedState(this, StateMachine, "PlayerDetected", playerDetectedStateData, this));
        stateList.Add(ChargeState = new E1_ChargeState(this, StateMachine, "Charge", chargeStateData, this));
        stateList.Add(LookForPlayerState = new E1_LookForPlayerState(this, StateMachine, "LookForPlayer", lookForPlayerStateData, this));
        stateList.Add(MeleeAttackState = new E1_MeleeAttackState(this, StateMachine, "MeleeAttack", meleeAttackPosition, meleeAttackState, this));
        stateList.Add(StunState = new E1_StunState(this, StateMachine, "Stun", stunStateData, this));
        stateList.Add(DeadState = new E1_DeadState(this, StateMachine, "Dead", deadStateData, this));
    }
    private void OnDisable()
    {
        stats.OnStunned -= Stunned;
    }

    private void Stunned()
    {
        StateMachine.SwitchState(StunState);
    }

    public override void Start()
    {
        base.Start();
        StateMachine.Initialize(MoveState);
        stats = core.GetCoreComponent(typeof(Stats)) as Stats;
        stats.OnStunned += Stunned;
    }


    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawWireSphere(meleeAttackPosition.position,meleeAttackState.AttackRadius);
    }
    
    
}
