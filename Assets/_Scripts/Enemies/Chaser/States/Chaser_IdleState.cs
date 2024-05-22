
using System.Collections.Generic;
using UnityEngine;

public class Chaser_IdleState : IdleS
{
    private Chaser chaser;
    private Chaser_Data chaserData;
    
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
        Debug.Log("Enter Idle State");
        base.Enter();
        if (core.PathFindingComponent.FindPath(entity.gameObject, NodeGraph.Instance.PlayerNode) != null)
        {
            stateMachine.SwitchState(chaser.MoveState);
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
    }
}
