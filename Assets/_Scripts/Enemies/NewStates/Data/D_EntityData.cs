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
    
    [Header("Melee Attack")] 
    public float AttackDamage = 10;
    public float AttackRadius = 0.5f;
    public float KnockbackStrength = 10;
    public Vector2 KnockbackAngle = Vector2.one;
    public LayerMask whatIsPlayer;
}
