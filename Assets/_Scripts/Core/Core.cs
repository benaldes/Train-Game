using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    }
	public Combat Combat => _combat;
	public Stats Stats => _stats;
	public ParticleManager ParticleManager => _particleManager;

	[SerializeField, HideInInspector] private Combat _combat;
	[SerializeField, HideInInspector] private Stats _stats;
	[SerializeField, HideInInspector] private ParticleManager _particleManager;
	[SerializeField, HideInInspector] private Movement movement;
	[SerializeField, HideInInspector] private CollisionSenses collisionSenses;

    private List<CoreComponent> components = new List<CoreComponent>();
    
    private void OnValidate()
    {
        movement = GetComponentInChildren<Movement>();
        collisionSenses = GetComponentInChildren<CollisionSenses>();
		_combat = GetComponentInChildren<Combat>();
        _stats = GetComponentInChildren<Stats>();
        _particleManager = GetComponentInChildren<ParticleManager>();
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
