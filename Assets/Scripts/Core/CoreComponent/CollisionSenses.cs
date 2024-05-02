using UnityEngine;

public class CollisionSenses : CoreComponent
{
    #region Variables
    public Transform GroundCheck => groundCheck;
    public Transform WallCheck => wallCheck;
    public Transform HorizontalLedgeCheck => horizontalLedgeCheck;
    public Transform VerticalLedgeCheck => verticalLedgeCheck;
    public float GroundCheckRadius => groundCheckRadius;
    public float WallCheckDistance => wallCheckDistance;
    public LayerMask WhatIsGround => whatIsGround;
    
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private Transform horizontalLedgeCheck;
    [SerializeField] private Transform verticalLedgeCheck;

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
    public bool CheckIfTouchingHorizontalLedge()
    {
         return Physics2D.Raycast(horizontalLedgeCheck.position, Vector2.right * 
                                core.Movement.FacingDirection, wallCheckDistance, whatIsGround);
    }
    public bool CheckIfTouchingVerticalLedge()
    {
        return Physics2D.Raycast(verticalLedgeCheck.position, Vector2.down, wallCheckDistance, whatIsGround);
    }

}
