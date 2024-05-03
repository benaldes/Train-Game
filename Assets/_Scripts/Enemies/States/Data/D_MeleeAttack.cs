using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NewMeleeAttackStateData",menuName = "Data/State Data/Melee Attack State Data")]
public class D_MeleeAttack : ScriptableObject
{
    public float AttackRadius = 0.5f;
    public float AttackDamage = 10;

    public Vector2 KnockbackAngle = Vector2.one;
    public float KnockbackStrength = 10;
    public LayerMask whatIsPlayer;
    
}
