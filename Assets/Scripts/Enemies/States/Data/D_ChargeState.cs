using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewChargeStateData",menuName = "Data/State Data/Charge State Data")]
public class D_ChargeState : ScriptableObject
{
    public float ChargeSpeed = 6;
    public float ChargeTime = 2;
}
