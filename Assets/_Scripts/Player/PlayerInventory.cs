using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInventory : MonoBehaviour
{
    public Weapon[] weapons;
    
    public PlayerAttackState attackState;

    private bool weaponOne = true;

    public Player player;

    public void SendAttackState(PlayerAttackState attackState)
    {
        this.attackState = attackState;
    }

    private void Update()
    {
        if(player.StateMachine.CurrentState == attackState) return;
        
        if(Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.JoystickButton3))
        {
            if (weaponOne)
            {
                
                attackState.SetWeapon(weapons[1]);
                weaponOne = false;
            }
            else
            {
                attackState.SetWeapon(weapons[0]);
                weaponOne = true;
            }
            
        }
    }
}
