using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour,Idamageble
{
    [SerializeField] private int Health;
    [SerializeField] private int MaxHealth;
    private Node myNode;

    private void Awake()
    {
        myNode = PathFinding.FindClosestNode(NodeGraph.Instance.Nodes, gameObject.transform.position);
        NodeGraph.Instance.SetHeartNode(myNode);
    }

    private void FixedUpdate()
    {
        myNode = PathFinding.FindClosestNode(NodeGraph.Instance.Nodes, gameObject.transform.position);
        NodeGraph.Instance.SetHeartNode(myNode);
    }

    [ContextMenu("destroy Heart")]
    public void HeartDestroyed()
    {
        LevelManager.HeartDestroyed.Invoke();
        Destroy(gameObject);
    }


    public void Damage(float damage)
    {
        Health -= (int)damage;
        if (Health < 0)
        {
            Health = 0;
            HeartDestroyed();
        }
    }
}
