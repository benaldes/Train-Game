using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class NodeGraph : MonoBehaviour
{
    public static NodeGraph Instance;
    public Transform size;
    public List<List<Node>> Nodes = new List<List<Node>>();
    public float XNodeSpace = 1;
    public float YNodeSpace = 1;

    public GameObject PathObject;
    public GameObject StartNode;
    public GameObject TargetNode;
    public Node PlayerNode { get; private set; }
    
    [SerializeField] private LayerMask whatIsGround; 
    
    private Vector2 currentPosition;
    private Vector2 minXYGridPosition;
    private Vector2 maxXYGridPosition;
    
    private bool drawGizmos = false;

    private List<GameObject> pathShowObject = new List<GameObject>();
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Destroy the new instance if an existing one is found
            return;
        }
        Instance = this;
        
        initializeNodeGraph();
    }

    [ContextMenu("test pathfinding")]
    public void test()
    {
        
        Node startNode = Nodes.FindClosestNode(StartNode.transform.position);
        Node endNode= Nodes.FindClosestNode(TargetNode.transform.position);;
        
        List<Node> path = PathFinding.FindPath(startNode,endNode);
        foreach (var pathObject in pathShowObject)
        {
            Destroy(pathObject);
        }
        pathShowObject.Clear();
        foreach (var VARIABLE in path)
        {
            pathShowObject.Add(Instantiate(PathObject,VARIABLE.WorldPosition,quaternion.identity));
            
        }

    }

    public void SetPlayerNode(Node Node)
    {
        PlayerNode = Node;
    }
    [ContextMenu("initialize Node Graph")]
    private void initializeNodeGraph()
    {
        initializeNodeRow();
        FillGraph();
        checkGraphNodesAboveGround();
        checkGridNodesInAir();
        CheckAllNodeNeighbours();
        StartCoroutine(TestPathfinding());
    }

    IEnumerator TestPathfinding()
    {
        yield return new WaitForSeconds(0.01f);
        test();
        StartCoroutine(TestPathfinding());
    }
    private void initializeNodeRow()
    {
        Nodes.Clear();
        
        var position = size.position;
        var lossyScale = size.lossyScale;
        
        minXYGridPosition = new Vector2(position.x - lossyScale.x / 2, position.y + lossyScale.y / 2);
        maxXYGridPosition = new Vector2(position.x + lossyScale.x / 2, position.y - lossyScale.y / 2);
        
        int numberOfNodeRows = Mathf.CeilToInt(Mathf.Abs(maxXYGridPosition.y - minXYGridPosition.y) / YNodeSpace) ;

        for (int i = 0; i < numberOfNodeRows; i++)
        {
            Nodes.Add(new List<Node>());
        }
    }
    private void FillGraph()
    {
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
    }
   
    private void checkGraphNodesAboveGround()
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
    }
    private void checkGridNodesInAir()
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

    private void CheckAllNodeNeighbours()
    {
        for (int i = 0; i < Nodes.Count; i++)
        {
            for (int j = 0; j < Nodes[i].Count; j++)
            {
                SetNodeNeighbours(i,j);
            }
        }
    }

    private void SetNodeNeighbours(int y, int x)
    {
        List<Node> neighbours = new List<Node>();
        
        try { if(CheckIfNodePassable(Nodes[y + 1][x])) neighbours.Add(Nodes[y + 1][x]); }
        catch (ArgumentOutOfRangeException) { }
        
        try { if(CheckIfNodePassable(Nodes[y - 1][x])) neighbours.Add(Nodes[y - 1][x]); }
        catch (ArgumentOutOfRangeException) { }
        
        try { if(CheckIfNodePassable(Nodes[y][x + 1])) neighbours.Add(Nodes[y][x + 1]); }
        catch (ArgumentOutOfRangeException) { }
        
        try { if(CheckIfNodePassable(Nodes[y][x - 1])) neighbours.Add(Nodes[y][x - 1]); }
        catch (ArgumentOutOfRangeException) { }
        
        Nodes[y][x].SetNeighbours(neighbours);
        
    }

    private bool CheckIfNodePassable(Node node)
    {
        if (node == null) return false;
        if (node.TileType is TileType.aboveGround or TileType.InAir)
        {
            return true;
        }
        return false;
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
            
                Gizmos.DrawSphere(node.WorldPosition,0.1f);
            }
           
        }
    }
}
