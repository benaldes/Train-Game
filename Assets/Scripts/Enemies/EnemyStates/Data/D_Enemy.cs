using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D_Enemy : ScriptableObject
{
   public float WallCheckDistance;
   public float LedgeCheckDistance;

   public LayerMask WhatIsGround;
}
