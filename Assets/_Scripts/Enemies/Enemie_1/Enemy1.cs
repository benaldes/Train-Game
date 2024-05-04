using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Serialization;

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
    

    public override void Awake()
    {
        base.Awake();
        MoveState = new E1_MoveState(this, StateMachine, "Move", moveStateData,this);
        IdleState = new E1_IdleState(this, StateMachine, "Idle", idleStateData, this);
        PlayerDetectedState = new E1_PlayerDetectedState(this, StateMachine, "PlayerDetected", playerDetectedStateData, this);
        ChargeState = new E1_ChargeState(this, StateMachine, "Charge", chargeStateData, this);
        LookForPlayerState = new E1_LookForPlayerState(this, StateMachine, "LookForPlayer", lookForPlayerStateData, this);
        MeleeAttackState = new E1_MeleeAttackState(this, StateMachine, "MeleeAttack", meleeAttackPosition, meleeAttackState, this);
        StunState = new E1_StunState(this, StateMachine, "Stun", stunStateData, this);
        DeadState = new E1_DeadState(this, StateMachine, "Dead", deadStateData, this);
    }
    private void OnDisable()
    {
        core.Stats.OnStunned -= Stunned;
    }

    private void Stunned()
    {
        StateMachine.SwitchState(StunState);
    }

    private void Start()
    {
        Time.timeScale = 1;
        StateMachine.Initialize(MoveState);
        core.Stats.OnStunned += Stunned;
    }


    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawWireSphere(meleeAttackPosition.position,meleeAttackState.AttackRadius);
    }
    
    
}
