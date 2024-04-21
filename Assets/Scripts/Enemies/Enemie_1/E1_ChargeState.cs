using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_ChargeState : ChargeState
{
    private Enemy1 enemy;
    public E1_ChargeState(Entity entity, StateMachine stateMachine, string animName, D_ChargeState stateData, Enemy1 enemy) : base(entity, stateMachine, animName, stateData)
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
        if (!isDetectingLedge || isDetectingWall)
        {
            stateMachine.SwitchState(enemy.LookForPlayerState);
        }
        if (isChargeTimeOver)
        {
            if (isPlayerInMinAgroRange)
            {
                stateMachine.SwitchState(enemy.PlayerDetectedState);
            }
        }
    }
}
