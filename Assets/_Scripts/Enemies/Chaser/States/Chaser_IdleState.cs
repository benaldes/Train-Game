
using System.Collections.Generic;
using UnityEngine;

public class Chaser_IdleState : IdleS
{
    private Chaser chaser;
    private Chaser_Data chaserData;

    private float lookForNewPathCooldown = 1;
    private float LookForNewPathTimer;
    
    public Chaser_IdleState(Entity entity, StateMachine stateMachine, string animName, D_EntityData entityData) : base(entity, stateMachine, animName, entityData)
    {
        if (entity.GetType() == typeof(Chaser))
        {
            chaser = (Chaser)entity;
        }

        if (entityData.GetType() == typeof(Chaser_Data))
        {
            chaserData = (Chaser_Data)entityData;
        }
     
    }

    public override void Enter()
    {
        base.Enter();
        
        Debug.Log("Enter Idle State");
        CheckIfSwitchToMoveState();
        LookForNewPathTimer = Time.time;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (Time.time > LookForNewPathTimer + lookForNewPathCooldown)
        {
            if(CheckIfSwitchToMoveState()) return;
            LookForNewPathTimer = Time.time;
        }
        
    }

    private bool CheckIfSwitchToMoveState()
    {
        if (core.PathFindingComponent.FindPath(entity.gameObject, NodeGraph.Instance.PlayerNode) != null)
        {
            stateMachine.SwitchState(chaser.MoveState);
            return true;
        }

        return false;
    }
}
