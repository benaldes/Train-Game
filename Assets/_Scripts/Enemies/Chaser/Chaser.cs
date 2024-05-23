using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaser : Entity
{
    public Chaser_IdleState IdleState { get; private set; }
    public Chaser_MoveState MoveState { get; private set; }
    public Chaser_JumpState JumpState { get; private set; }
    public Chaser_PlayerDetectedState PlayerDetectedState { get; private set; }
    public Chaser_ChargeState ChargeState { get; private set; }
    public Chaser_MeleeAttackState MeleeAttackState { get; private set; }

    [SerializeField] private Transform meleeAttackPosition;
    public override void Awake()
    {
        base.Awake();
        IdleState = new Chaser_IdleState(this, StateMachine, "Idle", newEntityData);
        MoveState = new Chaser_MoveState(this, StateMachine, "Move", newEntityData);
        JumpState = new Chaser_JumpState(this, StateMachine, "Jump", newEntityData);
        PlayerDetectedState = new Chaser_PlayerDetectedState(this, StateMachine, "PlayerDetected", newEntityData);
        ChargeState = new Chaser_ChargeState(this, StateMachine, "Charge", newEntityData);
        MeleeAttackState = new Chaser_MeleeAttackState(this, StateMachine, "MeleeAttack", newEntityData, meleeAttackPosition);

    }

    private void Start()
    {
        StateMachine.Initialize(IdleState);
    }

    public override void OnDrawGizmos()
    {
        
    }
}
