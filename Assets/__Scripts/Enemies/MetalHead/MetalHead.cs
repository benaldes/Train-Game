using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetalHead : Entity
{
    public Collider2D Collider2D { get;private set; }
    public MetalHead_IdleState IdleState { get; private set; }
    public MetalHead_MoveState MoveState { get; private set; }
    public MetalHead_JumpState JumpState { get; private set; }
    
    [SerializeField] private Transform meleeAttackPosition;
    
    
    private void OnValidate()
    {
        Collider2D = GetComponent<Collider2D>();
    }

    public override void Awake()
    {
        base.Awake();
        stateList.Add(IdleState = new MetalHead_IdleState(this, StateMachine, "Idle", newEntityData));
        stateList.Add(MoveState = new MetalHead_MoveState(this, StateMachine, "Move", newEntityData));
        stateList.Add(JumpState = new MetalHead_JumpState(this, StateMachine, "Jump", newEntityData));

        
    }

    public override void Start()
    {
        base.Start();
        StateMachine.Initialize(IdleState);
    }
}
