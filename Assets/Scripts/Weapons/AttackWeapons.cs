using System;
using System.Collections.Generic;
using UnityEngine;

public class AttackWeapons : Weapon
{
    protected SO_AttackWeaponData AttackWeaponData;
    private List<Idamageble> detectedDamageable = new List<Idamageble>();
    protected override void Awake()
    {
        base.Awake();
        if (weaponData.GetType() == typeof(SO_AttackWeaponData))
        {
            AttackWeaponData = (SO_AttackWeaponData)weaponData;
        }
        else
        {
            Debug.LogError("wrong SO data for weapon");
        }
    }

    public override void AnimationActionTrigger()
    {
        base.AnimationActionTrigger();
        CheckMeleeAttack();
        
    }

    private void CheckMeleeAttack()
    {
        WeaponAttackDetails details = AttackWeaponData.AttackDerails[attackCounter];
        foreach (Idamageble item in detectedDamageable)
        {
            item.Damage(details.DamageAmount);
        }
    }

    public void AddToDetected(Collider2D collision)
    {
        Idamageble damageable = collision.GetComponent<Idamageble>();

        if (damageable != null)
        {
            detectedDamageable.Add(damageable);
        }
    }
    public void RemoveFromDetected(Collider2D collision)
    {
        Idamageble damageable = collision.GetComponent<Idamageble>();

        if (damageable != null)
        {
            detectedDamageable.Remove(damageable);
        }
    }
    
}
