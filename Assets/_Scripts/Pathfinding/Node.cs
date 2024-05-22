using System.Collections.Generic;
using UnityEngine;

public class Node 
{
    public bool isGround;
    public TileType TileType = TileType.NotValid;
    public Vector2 WorldPosition;
    public Vector2 GraphPosition;
    public LayerMask whatIsGround;
    public List<Node> Neighbours = new List<Node>();

    public Node(Vector2 worldPosition,LayerMask whatIsGround = default)
    {
        WorldPosition = worldPosition;
        this.whatIsGround = whatIsGround;
    }

    public void CheckIfTileIsGround()
    {
        Collider2D hit = Physics2D.OverlapCircle(WorldPosition, 0.05f, whatIsGround);
        
        if (hit != null)
        {
            isGround = true;
            TileType = TileType.Grounded;
        }
        else
        {
            isGround = false;
        }
    }

    public void SetNeighbours(List<Node> neighbours)
    {
        Neighbours = neighbours;
    }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }   

        Node otherNode = (Node)obj;
        return WorldPosition == otherNode.WorldPosition;
    }
}

public enum TileType
{
    NotValid,
    aboveGround,
    InAir,
    Grounded,
   
}
