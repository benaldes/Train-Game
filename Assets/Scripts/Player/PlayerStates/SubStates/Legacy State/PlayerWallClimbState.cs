using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallClimbState : PlayerTouchinWallState
{
    public PlayerWallClimbState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animName) : base(player, stateMachine, playerData, animName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(isExitingState) return;
        core.Movement.SetVelocityY(playerData.wallClimbVelocity);
        if (yInput != 1 && !isExitingState)
        {
            stateMachine.SwitchState(player.WallGrabState);
        }
    }
}
