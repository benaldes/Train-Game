using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D_EntityData : ScriptableObject
{
    [Header("Idle State")] 
    public float MinIdleTime = 1;
    public float MaxIdleTime = 1;
    [Header("Move State")] 
    public float JumpVelocity = 15f;
    public int amountsOfJumps = 1;
    [Header("Stun State")]
    public float StunResistance = 3;
    public float StunRecoveryTime = 2;
}
