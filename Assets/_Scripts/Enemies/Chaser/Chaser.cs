using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaser : Entity
{
    public Chaser_IdleState IdleState { get; private set; }
    public Chaser_MoveState MoveState { get; private set; }
    public Chaser_JumpState JumpState { get; private set; }

    public override void Awake()
    {
        base.Awake();
        IdleState = new Chaser_IdleState(this, StateMachine, "Idle", newEntityData);
        MoveState = new Chaser_MoveState(this, StateMachine, "Move", newEntityData);
        JumpState = new Chaser_JumpState(this, StateMachine, "Jump", newEntityData);
    }

    private void Start()
    {
        StateMachine.Initialize(IdleState);
    }

    public override void OnDrawGizmos()
    {
        
    }
}
