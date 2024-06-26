using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class Core : MonoBehaviour
{
	/*public Combat Combat => combat;
	public Stats Stats => stats;
	public ParticleManager ParticleManager => particleManager;
    public Movement Movement => movement;
    public CollisionSenses CollisionSenses => collisionSenses;
    public PathFindingComponent PathFindingComponent => pathFindingComponent;*/

	[SerializeField, HideInInspector] private Combat combat;
	[SerializeField, HideInInspector] private Stats stats;
	[SerializeField, HideInInspector] private ParticleManager particleManager;
	[SerializeField, HideInInspector] private Movement movement;
	[SerializeField, HideInInspector] private CollisionSenses collisionSenses;
    [SerializeField, HideInInspector] private PathFindingComponent pathFindingComponent;
    
    private List<CoreComponent> components = new List<CoreComponent>();
    
    private void OnValidate()
    {
        movement = GetComponentInChildren<Movement>();
        collisionSenses = GetComponentInChildren<CollisionSenses>();
		combat = GetComponentInChildren<Combat>();
        stats = GetComponentInChildren<Stats>();
        particleManager = GetComponentInChildren<ParticleManager>();
        pathFindingComponent = GetComponentInChildren<PathFindingComponent>();
    }

    public void InitializeCoreComponents()
    {
        foreach (CoreComponent component in components)
        {
            component.InitializeCoreComponent();
        }
    }
    public void LogicUpdate()
    {
        foreach (CoreComponent component in components)
        {
            component.LogicUpdate();
        }
    }

    public void PhysicsUpdate()
    {
        foreach (CoreComponent component in components)
        {
            component.PhysicsUpdate();
        }
    }

    public void AddComponent(CoreComponent component)
    {
        if (!components.Contains(component))
        {
            components.Add(component);
        }
    }

    public CoreComponent GetCoreComponent(Type coreType)
    {
        foreach (CoreComponent component in components)
        {
            if (component.GetType() == coreType)
            {
                return component;
            }
        }

        return null;
    }
    
}
