using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CombatTestDummy : MonoBehaviour,Idamageble
{
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void Damage(float value)
    {
        Debug.Log(value);
    }
}
