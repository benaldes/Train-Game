using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NewStunStateData",menuName = "Data/State Data/Stun State Data")]
public class D_StunState : ScriptableObject
{
    public float StunTime = 3;
    public float StunKnockbackTime = 0.2f;
    public float StunKnockbackSpeed = 20;
    
    public Vector2 StunKnockbackAngle;
}
