using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NewDeadStateData",menuName = "Data/State Data/Dead State Data")]

public class D_DeadState : ScriptableObject
{
    public GameObject DeathChunkParticle;
    public GameObject DeathBloodParticle;
}
