using UnityEngine;

public class AnimationToStateMachine : MonoBehaviour
{
    public AttackState AttackState;
    public AttackS AttackS;
    
    // TODO: need to delaet the attackState variable after i'm done with the new enemy type
    private void TriggerAttack()
    {
        if(AttackState != null)
            AttackState.TriggerAttack();
        else
            AttackS.TriggerAttack();
        
    }
    private void FinishAttack()
    {
        if(AttackState != null)
            AttackState.FinishAttack();
        else
            AttackS.FinishAttack();
    }
}
