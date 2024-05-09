using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : PlayerTouchinWallState
{
    public PlayerWallSlideState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animName)
        : base(player, stateMachine, playerData, animName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(isExitingState) return;
        core.Movement.SetVelocityY(-playerData.wallSlideVelocity * (Time.time - StartTime) * 4);
    }


}
