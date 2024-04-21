using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_MoveState : MoveState
{
    private Enemy1 enemyType;
    public E1_MoveState(Entity entity, StateMachine stateMachine, string animName, D_MoveState stateData,Enemy1 enemyType) : base(entity, stateMachine, animName, stateData)
    {
        this.enemyType = enemyType;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isDetectingWall || !isDetectingLedge)
        {
            enemyType.IdleState.SetFlipAfterIdle(true);
            stateMachine.SwitchState(enemyType.IdleState);
        }
    }
}

