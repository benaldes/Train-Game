using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "NewEntityData",menuName = "Data/Entity Data/Base Data")]
public class D_Entity : ScriptableObject
{
   public float MaxHp = 30;

   public float damageHopSpeed = 3;
   
   public float WallCheckDistance = 0.2f;
   public float LedgeCheckDistance = 0.4f;
   public float groundCheckRadius = 0.3f;

   public float MinAgroDistance = 3f;
   public float MaxAgroDistance = 4f;

   public float StunResistance = 3;
   public float StunRecoveryTime = 2;
   
   public float CloseRangeActionDistance = 1;

   public GameObject HitParticle;
   
   [FormerlySerializedAs("WahtIsPlayer")] public LayerMask WhatIsPlayer;
   public LayerMask WhatIsGround;
}
