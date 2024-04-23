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

    private Movement movement;
    private CollisionSenses collisionSenses;
    private void Awake()
    {
        Movement = GetComponentInChildren<Movement>();
        CollisionSenses = GetComponentInChildren<CollisionSenses>();
    }

    public void LogicUpdate()
    {
        Movement.LogicUpdate();
        
    }
}
