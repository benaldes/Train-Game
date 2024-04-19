using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyState
{
    protected D_IdleState stateData;
    protected bool flipAfterIdle;
    protected bool isIdleTimeOver;
    protected float idleTime;
    
    public EnemyIdleState(Enemy enemy, EnemyStateMachine stateMachine, string animName,D_IdleState stateData) : base(enemy, stateMachine, animName)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.SetVelocity(0f);
        isIdleTimeOver = false;

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (Time.time >= startTime + idleTime)
        {
            isIdleTimeOver = true;
        }
    }

    public override void Exit()
    {
        base.Exit();
        if (flipAfterIdle)
        {
            enemy.Flip();
        }
    }

    public void SetFlipAfterIdle(bool flip)
    {
        flipAfterIdle = flip;
    }

    private void SetRandomIdleTime()
    {
        idleTime = Random.Range(stateData.MinIdleTime, stateData.MaxIdleTime);
    }
}
