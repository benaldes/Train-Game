using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node 
{
    public bool Valid = false;
    public Vector2 Position;
    public LayerMask whatIsGround;

    public Node(Vector2 position,LayerMask whatIsGround)
    {
        Position = position;
        this.whatIsGround = whatIsGround;
    }

    public void CheckTileType()
    {
        Collider2D hit = Physics2D.OverlapCircle(Position, 0.05f, whatIsGround);
        
        if (hit != null)
        {
            Valid = true;
            
        }
        else
        {
            Valid = false;
        }
        Debug.Log(Valid);
    }
}
