
using System;
using UnityEngine;
using Random = UnityEngine.Random;


public class Entity : MonoBehaviour 
{ 
    public D_Entity entityData;
    public StateMachine StateMachine;
    public Animator Animator { get; private set; }
    public AnimationToStateMachine AnimToStateMachine { get; private set; }

    [SerializeField] private Transform wallCheck;
    [SerializeField] private Transform ledgeCheck;
    [SerializeField] private Transform playerCheck;
    [SerializeField] private Transform groundCheck;


    private float lastDamageTime;

    public int lastDamageDirection { get; private set; }
    public Core core{ get; private set; }
    
    private Vector2 velocityWorkSpace;

    protected bool isStuned;
    protected bool isDead;

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
        
        //Animator.SetFloat("yVelocity",core.Movement.RB.velocity.y);
        if (Time.time >= core.Combat.LastDamageTime + entityData.StunRecoveryTime)
        {
            //ResetStunResistance();
            core.Stats.ResetStunResistance();
        }
    }

    public virtual void FixedUpdate()
    {
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

    public virtual void DamageHop(float velocity)
    {
        velocityWorkSpace.Set(core.Movement.RB.velocity.x,velocity);
        core.Movement.RB.velocity = velocityWorkSpace;
    }

    public virtual void ResetStunResistance()
    {
        isStuned = false;

    }
    
    public virtual void OnDrawGizmos()
    {
        if(core == null) return;
        Gizmos.DrawLine(wallCheck.position,wallCheck.position + (Vector3)(Vector2.right * core.Movement.FacingDirection * entityData.WallCheckDistance));
        Gizmos.DrawLine(ledgeCheck.position,ledgeCheck.position + (Vector3)(Vector2.down  * entityData.LedgeCheckDistance));
        
        Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right*  core.Movement.FacingDirection * entityData.CloseRangeActionDistance),0.2f);
        Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * core.Movement.FacingDirection * entityData.MinAgroDistance),0.2f);
        Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * core.Movement.FacingDirection * entityData.MaxAgroDistance),0.2f);
    }

    
}
