using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreComponent : MonoBehaviour
{
    protected Core core;

    protected virtual void Awake()
    {
        core = transform.parent.GetComponent<Core>();

        if (core == null) { Debug.LogError("didnt find the core on parent"); }
        
        core.AddComponent(this);
    }

    public virtual void LogicUpdate() { }

    public virtual void PhysicsUpdate() { }
}
