using System;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour 
{ 
    public D_Entity entityData;
    public D_EntityData newEntityData; // TODO: need to change its name to "entityData" after we stop using the current "entityData:
    public StateMachine StateMachine;
    public Animator Animator { get; private set; }
    public AnimationToStateMachine AnimToStateMachine { get; private set; }
    
    public int lastDamageDirection { get; private set; }
    
    public Core core{ get; private set; }
    protected List<State> stateList = new List<State>();
    
    private Movement movement;
    private Combat combat;
    private Stats stats;
    
    [SerializeField] private Transform playerCheck;
    
    private float lastDamageTime;
    
    public virtual void Awake()
    {
        core = GetComponentInChildren<Core>();
        Animator = GetComponent<Animator>();
        AnimToStateMachine = GetComponent<AnimationToStateMachine>();
        StateMachine = new StateMachine();
    }
    
    public virtual void Start()
    {
        foreach (State state in stateList)
        {
            state.initializeState();
        }
        
        core.InitializeCoreComponents();
        
        movement = core.GetCoreComponent(typeof(Movement)) as Movement;
        combat = core.GetCoreComponent(typeof(Combat)) as Combat;
        stats = core.GetCoreComponent(typeof(Stats)) as Stats;
    }

    public virtual void Update()
    {
        core.LogicUpdate();
        StateMachine.currentState.LogicUpdate();
        
        if (Time.time >= combat.LastDamageTime + entityData.StunRecoveryTime)
        {
            stats.ResetStunResistance();
        }
    }
    
    public virtual void FixedUpdate()
    {
        core.PhysicsUpdate();
        StateMachine.currentState.PhysicsUpdate();
    }
    
    public virtual bool CheckPlayerInMinAgroRange()
    {
        return Physics2D.Raycast(playerCheck.position, transform.right, newEntityData.MinAgroDistance,
            newEntityData.WhatIsPlayer);
    }
    
    public virtual bool CheckPlayerInMaxAgroRange()
    {
        return Physics2D.Raycast(playerCheck.position, transform.right, newEntityData.MaxAgroDistance,
            newEntityData.WhatIsPlayer);
    }
    
    public virtual bool CheckPlayerInCloseRangeAction()
    {
        return Physics2D.Raycast(playerCheck.position, transform.right, newEntityData.CloseRangeActionDistance,
            newEntityData.WhatIsPlayer);
    }
    
    public virtual void OnDrawGizmos()
    {
        if(core == null) return;
        var position = playerCheck.position;
        Gizmos.DrawWireSphere(position + (Vector3)(Vector2.right*  movement.FacingDirection * newEntityData.CloseRangeActionDistance),0.2f);
        Gizmos.DrawWireSphere(position + (Vector3)(Vector2.right * movement.FacingDirection * newEntityData.MinAgroDistance),0.2f);
        Gizmos.DrawWireSphere(position + (Vector3)(Vector2.right * movement.FacingDirection * newEntityData.MaxAgroDistance),0.2f);
    }

    
}
