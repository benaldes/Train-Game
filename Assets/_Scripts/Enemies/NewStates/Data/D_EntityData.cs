using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D_EntityData : ScriptableObject
{
    [Header("Idle State")] 
    public float MinIdleTime = 1;
    public float MaxIdleTime = 1;
    
    [Header("Move State")] 
    public float MovementSpeed = 3;
    
    [Header("jump State")] 
    public float JumpVelocity = 5;
    public int amountsOfJumps = 1;
    
    [Header("Stun State")]
    public float StunResistance = 3;
    public float StunRecoveryTime = 2;
}
