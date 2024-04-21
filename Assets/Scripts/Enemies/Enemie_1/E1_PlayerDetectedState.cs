using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_PlayerDetectedState : PlayerDetectedState
{
    private Enemy1 enemy;
    public E1_PlayerDetectedState(Entity entity, StateMachine stateMachine, string animName, D_PlayerDetected stateData, Enemy1 enemy) : base(entity, stateMachine, animName, stateData)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (performCloseRangeAction)
        {
            stateMachine.SwitchState(enemy.MeleeAttackState);
        }
        else if (performLongRangeAction)
        {
            stateMachine.SwitchState(enemy.ChargeState);
        }
        else if (!isPlayerInMaxAgroRange)
        {
            stateMachine.SwitchState(enemy.LookForPlayerState);
        }
        
        
    }
}
