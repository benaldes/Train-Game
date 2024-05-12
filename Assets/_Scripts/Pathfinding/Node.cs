using System;using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node 
{
    public bool isGround;
    public TileType TileType = TileType.NotValid;
    public Vector2 Position;
    public LayerMask whatIsGround;

    public Node(Vector2 position,LayerMask whatIsGround)
    {
        Position = position;
        this.whatIsGround = whatIsGround;
    }

    public void CheckIfTileIsGround()
    {
        Collider2D hit = Physics2D.OverlapCircle(Position, 0.05f, whatIsGround);
        
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

  
}

public enum TileType
{
    NotValid,
    aboveGround,
    InAir,
    Grounded,
   
}
