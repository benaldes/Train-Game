using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveState : EnemyState
{
    protected D_MoveState stateData;

    protected bool isDetectingWall;
    protected bool isDetectingLedge;
    public EnemyMoveState(Enemy enemy, EnemyStateMachine stateMachine, string animName,D_MoveState stateData) : base(enemy, stateMachine, animName)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.SetVelocity(stateData.movementSpeed);
        isDetectingLedge = enemy.CheckLedge();
        isDetectingWall = enemy.CheckWall();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        isDetectingLedge = enemy.CheckLedge();
        isDetectingWall = enemy.CheckWall();
    }
}
