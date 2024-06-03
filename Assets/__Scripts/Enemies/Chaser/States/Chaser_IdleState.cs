
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

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        if(CheckIfSwitchToMoveState()) return;
        if(CheckIfSwitchToJumpState()) return;
        
    }


    private bool CheckIfSwitchToMoveState()
    {
        if (pathFinding.Direction.x != 0)
        {
            stateMachine.SwitchState(chaser.MoveState);
            return true;
        }

        return false;
    }
    private bool CheckIfSwitchToJumpState()
    {
        if (pathFinding.Direction.y != 0 && pathFinding.CheckIfNextNodeIsInAir()  && pathFinding.CheckIfTargetNodeIsHigher(pathFinding.ReturnNodeToJumpTo()))
        {
            stateMachine.SwitchState(chaser.JumpState);
            return true;
        }

        return false;
    }
}
