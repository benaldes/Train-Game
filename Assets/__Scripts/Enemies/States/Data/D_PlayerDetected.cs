using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "NewPlayerDetectedStateData",menuName = "Data/State Data/PlayerDetected State Data")]
public class D_PlayerDetected : ScriptableObject
{
  [FormerlySerializedAs("ActionTime")] public float LongRangeActionTime = 1.5f;
}
