using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        detectedDamageable.Clear();
    }

    private void CheckMeleeAttack()
    {
        //TODO: fix this shit
        AttackDetails attackDetails =new AttackDetails();
        attackDetails.DamageAmount = 10;
        attackDetails.Position = transform.position;
        attackDetails.StunDamageAmount  = 10;
        
        WeaponAttackDetails details = AttackWeaponData.AttackDerails[attackCounter];
        foreach (Idamageble item in detectedDamageable.ToList())
        {
            
            item.Damage(attackDetails);
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
