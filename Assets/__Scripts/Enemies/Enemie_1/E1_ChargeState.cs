

public class E1_ChargeState : ChargeState
{
    private Enemy1 enemy;
    public E1_ChargeState(Entity entity, StateMachine stateMachine, string animName, D_ChargeState stateData, Enemy1 enemy) : base(entity, stateMachine, animName, stateData)
    {
        this.enemy = enemy;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (performCloseRangeAction)
        {
            stateMachine.SwitchState(enemy.MeleeAttackState);
        }
        else if (!isDetectingLedge || isDetectingWall)
        {
            stateMachine.SwitchState(enemy.LookForPlayerState);
        }
        else if (isChargeTimeOver)
        {
            
            if (isPlayerInMinAgroRange)
            {
                stateMachine.SwitchState(enemy.PlayerDetectedState);
            }
            else
            {
                stateMachine.SwitchState(enemy.LookForPlayerState);
            }
        }
    }
}
