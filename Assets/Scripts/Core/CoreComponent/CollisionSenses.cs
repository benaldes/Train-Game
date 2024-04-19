using UnityEngine;

public class CollisionSenses : CoreComponenet
{
    #region Variables
    public Transform GroundCheck => groundCheck;
    public Transform WallCheck => wallCheck;
    public Transform LedgeCheck => ledgeCheck;
    
    public float GroundCheckRadius => groundCheckRadius;
    public float WallCheckDistance => wallCheckDistance;
    public LayerMask WhatIsGround => whatIsGround;
    
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private Transform ledgeCheck;

    [SerializeField] private float groundCheckRadius;
    [SerializeField] private float wallCheckDistance;
    
    [SerializeField] private LayerMask whatIsGround;

    #endregion
    
    public bool CheckIfGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, 
            whatIsGround);
    }
    public bool CheckIfTouchingWall()
    {
        return Physics2D.Raycast(wallCheck.position, Vector2.right * 
                 core.Movement.FacingDirection, wallCheckDistance, whatIsGround);
    }
    public bool CheckIfTouchingWallBack()
    {
        return Physics2D.Raycast(wallCheck.position, Vector2.right * 
                 -core.Movement.FacingDirection, wallCheckDistance, whatIsGround);
    }
    public bool CheckIfTouchingLedge()
    {
        return Physics2D.Raycast(ledgeCheck.position, Vector2.right * 
                 core.Movement.FacingDirection, wallCheckDistance, whatIsGround);
    }

}
