using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DeadState : State
{
    protected D_DeadState stateData;
    public DeadState(Entity entity, StateMachine stateMachine, string animName,D_DeadState stateData) : base(entity, stateMachine, animName)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();
        GameObject.Instantiate(stateData.DeathBloodParticle, Entity.transform.position,
            stateData.DeathBloodParticle.transform.rotation);
        GameObject.Instantiate(stateData.DeathChunkParticle, Entity.transform.position,
            stateData.DeathChunkParticle.transform.rotation);
        
        Entity.gameObject.SetActive(false);
    }
}
