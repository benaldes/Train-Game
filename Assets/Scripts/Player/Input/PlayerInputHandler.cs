using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
   public Vector2 RawMovementInput { get; private set; }
   public int NormInputX { get; private set; }
   public int NormInputY { get; private set; }
   public bool JumpInput { get; private set; }
   public bool JumpInputStop { get; private set; }
   public bool RollInput { get; private set; }
   public bool GrabInput { get; private set; }
   public bool[] AttackInputs { get; private set; }

   [SerializeField] 
   private float inputHoldTime = 0.2f;
   private float jumpInputStartTime;
   private float rollInputStartTime;

   private void Start()
   {
      int count = Enum.GetValues(typeof(CombatInputs)).Length;
      AttackInputs = new bool[count];
   }

   private void Update()
   {
      CheckJumpInputHoldTime();
   }

   public void onPrimaryAttackInput(InputAction.CallbackContext context)
   {
      if (context.started)
      {
         AttackInputs[(int)CombatInputs.primary] = true;
      }

      if (context.canceled)
      {
         AttackInputs[(int)CombatInputs.primary] = false;
      }
   }
   public void onSecondaryAttackInput(InputAction.CallbackContext context)
   {
      if (context.started)
      {
         AttackInputs[(int)CombatInputs.secondary] = true;
      }

      if (context.canceled)
      {
         AttackInputs[(int)CombatInputs.secondary] = false;
      }
   }
   public void OnMoveInput(InputAction.CallbackContext context)
   {
      RawMovementInput = context.ReadValue<Vector2>();
      NormInputX = Mathf.RoundToInt(RawMovementInput.x);
      NormInputY = Mathf.RoundToInt(RawMovementInput.y);
      
   }
   public void OnJumpInput(InputAction.CallbackContext context)
   {
      if (context.started)
      {
         JumpInput = true;
         JumpInputStop = false;
         jumpInputStartTime = Time.time;
      }

      if (context.canceled)
      {
         JumpInputStop = true;
      }
   }
   
   public void OnRollInput(InputAction.CallbackContext context)
   {
      if (context.started)
      {
         RollInput = true;
      }

      if (context.canceled)
      {
         RollInput = false;
      }
   }
   public void OnGrabInput(InputAction.CallbackContext context)
   {
      if (context.started)
      {
         GrabInput = true;
      }

      if (context.canceled)
      {
         GrabInput = false;
      }
   }
   public void UseJumpInput() => JumpInput = false;
   public void UseRollInput() => RollInput = false;
  

   private void CheckJumpInputHoldTime()
   {
      if (Time.time >= jumpInputStartTime + inputHoldTime)
      {
         JumpInput = false;
      }
      
   }
  
   
}
public enum CombatInputs
{
   primary,
   secondary
}
