﻿using UnityEngine;

public class Entity : MonoBehaviour 
{ 
    public D_Entity entityData;
    public D_EntityData newEntityData; // TODO: need to change its name to "entityData" after we stop using the current "entityData:
    public StateMachine StateMachine;
    public Animator Animator { get; private set; }
    public AnimationToStateMachine AnimToStateMachine { get; private set; }
    
    public int lastDamageDirection { get; private set; }
    
    public Core core{ get; private set; }
    
    [SerializeField] private Transform playerCheck;
    
    private float lastDamageTime;
    
    public virtual void Awake()
    {
        core = GetComponentInChildren<Core>();
        Animator = GetComponent<Animator>();
        AnimToStateMachine = GetComponent<AnimationToStateMachine>();
        StateMachine = new StateMachine();
    }
    
    public virtual void Update()
    {
        core.LogicUpdate();
        StateMachine.currentState.LogicUpdate();
        
        if (Time.time >= core.Combat.LastDamageTime + entityData.StunRecoveryTime)
        {
            core.Stats.ResetStunResistance();
        }
    }
    
    public virtual void FixedUpdate()
    {
        core.PhysicsUpdate();
        StateMachine.currentState.PhysicsUpdate();
    }
    
    public virtual bool CheckPlayerInMinAgroRange()
    {
        return Physics2D.Raycast(playerCheck.position, transform.right, entityData.MinAgroDistance,
            entityData.WhatIsPlayer);
    }
    
    public virtual bool CheckPlayerInMaxAgroRange()
    {
        return Physics2D.Raycast(playerCheck.position, transform.right, entityData.MaxAgroDistance,
            entityData.WhatIsPlayer);
    }
    
    public virtual bool CheckPlayerInCloseRangeAction()
    {
        return Physics2D.Raycast(playerCheck.position, transform.right, entityData.CloseRangeActionDistance,
            entityData.WhatIsPlayer);
    }
    
    public virtual void OnDrawGizmos()
    {
        if(core == null) return;
        var position = playerCheck.position;
        Gizmos.DrawWireSphere(position + (Vector3)(Vector2.right*  core.Movement.FacingDirection * entityData.CloseRangeActionDistance),0.2f);
        Gizmos.DrawWireSphere(position + (Vector3)(Vector2.right * core.Movement.FacingDirection * entityData.MinAgroDistance),0.2f);
        Gizmos.DrawWireSphere(position + (Vector3)(Vector2.right * core.Movement.FacingDirection * entityData.MaxAgroDistance),0.2f);
    }

    
}
