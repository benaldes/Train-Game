

public class E1_DeadState : DeadState
{
    private Enemy1 enemy;
    public E1_DeadState(Entity entity, StateMachine stateMachine, string animName, D_DeadState stateData,Enemy1 enemy) : base(entity, stateMachine, animName, stateData)
    {
        this.enemy = enemy;
    }
}
