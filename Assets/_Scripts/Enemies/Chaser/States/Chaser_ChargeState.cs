using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaser_ChargeState : ChargeS
{
    private Chaser chaser;
    private Chaser_Data chaserData;
    
    public Chaser_ChargeState(Entity entity, StateMachine stateMachine, string animName, D_EntityData entityData) : base(entity, stateMachine, animName, entityData)
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
        core.Movement.SetVelocityZero();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(CheckIfSwitchToMeleeAttackState()) return;
        
        core.Movement.SetVelocityX(chaserData.ChargeSpeed * core.Movement.FacingDirection);
    }

    private bool CheckIfSwitchToMeleeAttackState()
    {
        if (performCloseRangeAction)
        {
            stateMachine.SwitchState(chaser.MeleeAttackState);
            return true;
        }
        return false;
    }
}
