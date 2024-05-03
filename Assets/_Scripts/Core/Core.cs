using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour
{
    public Movement Movement
    {
        get
        {
            if (movement != null)
            {
                return movement;
            }
            Debug.LogError("No movement core component on" + transform.parent.name);
            return null;
        }
        private set { movement = value; }
        
    }
    public CollisionSenses CollisionSenses
    {
        get
        {
            if (movement != null)
            {
                return collisionSenses;
            }
            Debug.LogError("No collision Senses core component on" + transform.parent.name);
            return null;
        }
        private set { collisionSenses = value; }
    }
    public Combat Combat;
    public Stats Stats;
    public ParticleManager ParticleManager;
    
    private Movement movement;
    private CollisionSenses collisionSenses;

    private List<CoreComponent> components = new List<CoreComponent>();
    
    private void Awake()
    {
        Movement = GetComponentInChildren<Movement>();
        CollisionSenses = GetComponentInChildren<CollisionSenses>();
        Combat = GetComponentInChildren<Combat>();
        Stats = GetComponentInChildren<Stats>();
        ParticleManager = GetComponentInChildren<ParticleManager>();
    }

    public void LogicUpdate()
    {
        foreach (CoreComponent component in components)
        {
            component.LogicUpdate();
        }
    }

    public void AddComponent(CoreComponent component)
    {
        if (!components.Contains(component))
        {
            components.Add(component);
        }
    }
}