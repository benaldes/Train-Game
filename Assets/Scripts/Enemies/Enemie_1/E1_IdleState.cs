using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_IdleState : IdleState
{
    private Enemy1 enemy;
    public E1_IdleState(Entity entity, StateMachine stateMachine, string animName, D_IdleState stateData,Enemy1 enemy) : base(entity, stateMachine, animName, stateData)
    {
        this.enemy = enemy;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isPlayerInMinAgroRange)
        {
            stateMachine.SwitchState(enemy.PlayerDetectedState);
        }
        else if (isIdleTimeOver)
        {
            stateMachine.SwitchState(enemy.MoveState);
        }
    }
}
