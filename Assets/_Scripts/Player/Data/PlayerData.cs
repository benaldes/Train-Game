using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerData",menuName = "Data/Player Data/Base Data")]
public class PlayerData : ScriptableObject
{
    [Header("Move State")] 
    public float MovementVelocity = 10f;
    
    [Header("Jump State")] 
    public float JumpVelocity = 15f;
    public int amountsOfJumps = 1;

    [Header("Roll State")] 
    public float rollVelocity = 20;
    public float rollCooldown = 2;
    public Vector2 rollAngle = new Vector2(2, -1);
    
    
    [Header("Wall Jump State")] 
    public float wallJumpVelocity = 20;
    public float wallJumpTime = 0.4f;
    public Vector2 wallJumpAngle = new Vector2(1, 2);
    
    [Header("In Air State")]
    public float coyoteTime = 0.2f;
    public float variableJumpHeightMultiplier = 0.5f;

    [Header("Wall Slide State")] 
    public float wallSlideVelocity = 3f;

    [Header("Wall Climb State")] 
    public float wallClimbVelocity = 3f;

    [Header("Ledge Climb State")] 
    public Vector2 startOffSet;
    public Vector2 stopOffSet;
    
}
