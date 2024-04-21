
using System;
using UnityEngine;
using UnityEngine.Serialization;

public class Entity : MonoBehaviour
{ 
    public D_Entity entityData;
    public StateMachine StateMachine;
    public int FacingDirection { get; private set; }
    public Rigidbody2D RB { get; private set; }
    public Animator Animator { get; private set; }
    public GameObject aliveGO { get; private set; }

    [SerializeField] private Transform wallCheck;
    [SerializeField] private Transform ledgeCheck;
    private Vector2 velocityWorkSpace;

    public virtual void Start()
    {
        aliveGO = transform.Find("Alive").gameObject;
        RB = aliveGO.GetComponent<Rigidbody2D>();
        Animator = aliveGO.GetComponent<Animator>();

        StateMachine = new StateMachine();
    }

    public virtual void Update()
    {
        StateMachine.currentState.LogicUpdate();
    }

    public virtual void FixedUpdate()
    {
        StateMachine.currentState.PhysicsUpdate();
    }

    public virtual void SetVelocity(float velocity)
    {
        velocityWorkSpace.Set(FacingDirection* velocity,RB.velocity.y);
        RB.velocity = velocityWorkSpace;
    }

    public virtual bool CheckWall()
    {
        return Physics2D.Raycast(wallCheck.position, aliveGO.transform.right,
            entityData.WallCheckDistance, entityData.WhatIsGround);
    }

    public virtual bool CheckLedge()
    {
        return Physics2D.Raycast(ledgeCheck.position, Vector2.down,
            entityData.LedgeCheckDistance, entityData.WhatIsGround);
    }

    public virtual void Flip()
    {
        FacingDirection *= -1;
        aliveGO.transform.Rotate(0,180,0);
    }
}
