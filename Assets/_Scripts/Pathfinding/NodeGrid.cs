using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeGrid : MonoBehaviour
{
    
    public Transform size;
    //public List<Node> Nodes = new List<Node>();
    private List<List<Node>> Nodes = new List<List<Node>>();
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
        initializeNodeList();
        
        currentPosition = new Vector2(minXYGridPosition.x + XNodeSpace/2, minXYGridPosition.y - YNodeSpace/2);
        
        while (currentPosition.x < maxXYGridPosition.x)
        {
            int iteration = 0;
            
            while (currentPosition.y > maxXYGridPosition.y)
            {
                Nodes[iteration].Add(new Node(currentPosition,whatIsGround));
                currentPosition.y -= YNodeSpace;
                iteration++;
            }
            
            currentPosition.y += YNodeSpace * iteration;
            currentPosition.x += XNodeSpace;
        }

        foreach (List<Node> nodeRow in Nodes)
        {
            foreach (Node node in nodeRow)
            {
                node.CheckIfTileIsGround();
            }
        }
        
        drawGizmos = true;
        
        checkGridNodesAboveGround();
    }

    private void initializeNodeList()
    {
        var position = size.position;
        var lossyScale = size.lossyScale;
        
        minXYGridPosition = new Vector2(position.x - lossyScale.x / 2, position.y + lossyScale.y / 2);
        maxXYGridPosition = new Vector2(position.x + lossyScale.x / 2, position.y - lossyScale.y / 2);

        //int numberOfNodeRows = (int)Mathf.Abs((maxXYGridPosition.y - minXYGridPosition.y - 1);
        
        int numberOfNodeRows = Mathf.CeilToInt(Mathf.Abs(maxXYGridPosition.y - minXYGridPosition.y) / YNodeSpace) ;

        for (int i = 0; i < numberOfNodeRows; i++)
        {
            Nodes.Add(new List<Node>());
        }
    }

    public void checkGridNodesAboveGround()
    {
        for (int i = 0; i < Nodes.Count ; i++)
        {
            for (int j = 0; j < Nodes[i].Count; j++)
            {
                try
                {
                    if (Nodes[i + 1][j].TileType == TileType.Grounded && Nodes[i][j].TileType != TileType.Grounded)
                    {
                        Nodes[i][j].TileType = TileType.aboveGround;
                    }
                }
                catch (ArgumentOutOfRangeException){}
            }
        }

       checkGridNodesInAir();
    }
    public void checkGridNodesInAir()
    {
        for (int i = 0; i < Nodes.Count ; i++)
        {
            for (int j = 0; j < Nodes[i].Count; j++)
            {
                try
                {
                    if((Nodes[i][j + 1].TileType == TileType.aboveGround || Nodes[i][j - 1].TileType == TileType.aboveGround) && Nodes[i][j].TileType == TileType.NotValid)
                    {
                        
                        for (int k = i; k < Nodes.Count; k++)
                        {
                            if (Nodes[k][j].TileType == TileType.NotValid)
                            {
                                Nodes[k][j].TileType = TileType.InAir;
                                
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }
                catch (ArgumentOutOfRangeException){}
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 0, 1, 0.2f);
        Gizmos.DrawCube(new Vector3(size.position.x,size.position.y),size.lossyScale);
        
        if(!drawGizmos)return;
        
        foreach (var nodeRow in Nodes)
        {
            foreach (var node in nodeRow)
            {
                if (node.TileType == TileType.aboveGround)
                {
                    Gizmos.color = Color.yellow;
                }
                else if (node.TileType == TileType.InAir)
                {
                    Gizmos.color = Color.red;
                }
                else if (node.TileType == TileType.Grounded)
                {
                    Gizmos.color = Color.black;
                }
                else
                {
                    Gizmos.color = Color.clear;
                }
            
                Gizmos.DrawSphere(node.Position,0.1f);
            }
           
        }
    }
}
