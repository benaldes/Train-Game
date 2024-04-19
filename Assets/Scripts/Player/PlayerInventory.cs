using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public Weapon[] weapons;
    
    public PlayerAttackState attackState;

    private bool weaponOne = true;

    public void SendAttackState(PlayerAttackState attackState)
    {
        this.attackState = attackState;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
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
