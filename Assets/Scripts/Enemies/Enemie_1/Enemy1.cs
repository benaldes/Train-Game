using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Enemy1 : Entity
{
    public E1_IdleState IdleState { get; private set; }
    public E1_MoveState MoveState { get; private set; }
    public E1_PlayerDetectedState PlayerDetectedState { get; private set; }
    public E1_ChargeState ChargeState { get; private set; }
    public E1_LookForPlayerState LookForPlayerState { get; private set; }
    public E1_MeleeAttackState MeleeAttackState { get; private set; }
    
    
    [SerializeField] private D_IdleState idleStateData;
    [SerializeField] private D_MoveState moveStateData;
    [SerializeField] private D_PlayerDetected playerDetectedStateData;
    [SerializeField] private D_ChargeState chargeStateData;
    [SerializeField] private D_LookForPlayerState lookForPlayerStateData;
    [SerializeField] private D_MeleeAttackData meleeAttackStateData;

    [SerializeField] private Transform meleeAttackPosition;
    

    public override void Start()
    {
        base.Start();
        MoveState = new E1_MoveState(this, StateMachine, "Move", moveStateData,this);
        IdleState = new E1_IdleState(this, StateMachine, "Idle", idleStateData, this);
        PlayerDetectedState = new E1_PlayerDetectedState(this, StateMachine, "PlayerDetected", playerDetectedStateData, this);
        ChargeState = new E1_ChargeState(this, StateMachine, "Charge", chargeStateData, this);
        LookForPlayerState = new E1_LookForPlayerState(this, StateMachine, "LookForPlayer", lookForPlayerStateData, this);
        MeleeAttackState = new E1_MeleeAttackState(this, StateMachine, "MeleeAttack", meleeAttackPosition,
            meleeAttackStateData, this);
        
        
        StateMachine.Initialize(MoveState);
    }


    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawWireSphere(meleeAttackPosition.position,meleeAttackStateData.AttackRadius);
    }
}
