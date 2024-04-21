using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "NewEntityData",menuName = "Data/Entity Data/Base Data")]
public class D_Entity : ScriptableObject
{
   public float WallCheckDistance = 0.2f;
   public float LedgeCheckDistance = 0.4f;

   public float MinAgroDistance = 3f;
   public float MaxAgroDistance = 4f;
   
   public float CloseRangeActionDistance = 1;
   
   [FormerlySerializedAs("WahtIsPlayer")] public LayerMask WhatIsPlayer;
   public LayerMask WhatIsGround;
}
