using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewIdleStateData",menuName = "Data/State Data/Idle State Data")]
public class D_IdleState : ScriptableObject
{
   public float MinIdleTime = 1;
   public float MaxIdleTime = 2;
}
