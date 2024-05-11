using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeGrid : MonoBehaviour
{
    public Rect rect;
    public Transform size;
    public List<Node> Nodes = new List<Node>();
    public float XNodeSpace = 1;
    public float YNodeSpace = 1;
    [SerializeField] private LayerMask whatIsGround; 
    private Vector2 currentPosition;
    private Vector2 minXYGridPosition;
    private Vector2 maxXYGridPosition;
    private bool drawGizmos = false;
    
    

    private void Start()
    {
        FillGrid();
    }
    [ContextMenu("fill grid")]
    public void FillGrid()
    {
        minXYGridPosition = new Vector2(size.position.x - size.lossyScale.x / 2, size.position.y + size.lossyScale.y / 2);
        maxXYGridPosition = new Vector2(size.position.x + size.lossyScale.x / 2, size.position.y - size.lossyScale.y / 2);
        
        Debug.Log(minXYGridPosition + "  " + maxXYGridPosition);
        
        currentPosition = new Vector2(minXYGridPosition.x + XNodeSpace, minXYGridPosition.y - YNodeSpace);

        int iteration = 0;
        while (currentPosition.x < maxXYGridPosition.x)
        {
            while (currentPosition.y > maxXYGridPosition.y)
            {
                Nodes.Add(new Node(currentPosition,whatIsGround));
                currentPosition.y -= YNodeSpace;
                iteration++;
            }
            
            currentPosition.y += YNodeSpace * iteration;
            currentPosition.x += XNodeSpace;
            
            iteration = 0;
        }
        
        foreach (var node in Nodes)
        {
            node.CheckTileType();
        }

        drawGizmos = true;
    }
    

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 0, 1, 0.2f);
        Gizmos.DrawCube(new Vector3(size.position.x,size.position.y),size.lossyScale);
        
        if(!drawGizmos)return;
        foreach (var node in Nodes)
        {
            if (node.Valid)
            {
                Gizmos.color = Color.yellow;
            }
            else
            {
                Gizmos.color = Color.red;
            }
            Gizmos.DrawSphere(node.Position,0.1f);
        }
       
    }
}
