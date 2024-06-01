using System;
using UnityEngine;

public class Chaser : Entity
{
    public Collider2D Collider2D { get;private set; }
    public Chaser_IdleState IdleState { get; private set; }
    public Chaser_MoveState MoveState { get; private set; }
    public Chaser_JumpState JumpState { get; private set; }
    public Chaser_PlayerDetectedState PlayerDetectedState { get; private set; }
    public Chaser_ChargeState ChargeState { get; private set; }
    public Chaser_MeleeAttackState MeleeAttackState { get; private set; }

    [SerializeField] private Transform meleeAttackPosition;

    private void OnValidate()
    {
        Collider2D = GetComponent<Collider2D>();
    }

    public override void Awake()
    {
        base.Awake();
        stateList.Add(IdleState = new Chaser_IdleState(this, StateMachine, "Idle", newEntityData));
        stateList.Add(MoveState = new Chaser_MoveState(this, StateMachine, "Move", newEntityData)); 
        stateList.Add(JumpState = new Chaser_JumpState(this, StateMachine, "Jump", newEntityData));
        stateList.Add(PlayerDetectedState = new Chaser_PlayerDetectedState(this, StateMachine, "PlayerDetected", newEntityData));
        stateList.Add(ChargeState = new Chaser_ChargeState(this, StateMachine, "Charge", newEntityData));
        stateList.Add(MeleeAttackState = new Chaser_MeleeAttackState(this, StateMachine, "MeleeAttack", newEntityData, meleeAttackPosition));

    }

    public override void Start()
    {
        base.Start();
        StateMachine.Initialize(IdleState);
    }

    

 
}
