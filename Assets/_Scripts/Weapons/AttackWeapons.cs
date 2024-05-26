using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AttackWeapons : Weapon
{
    protected SO_AttackWeaponData AttackWeaponData;
    
    private List<Idamageble> detectedDamageables = new List<Idamageble>();
    private List<IKnockbackable> detectedKnockbackables = new List<IKnockbackable>();

    private Movement movement;
    
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

    public override void EnterWeapon()
    {
        if(movement == null) movement = core.GetCoreComponent(typeof(Movement)) as Movement;
        
        base.EnterWeapon();
        
        SoundManager.Instance.PlaySound(AttackWeaponData.AttackDetail[attackCounter].AudioClip);
    }

    public override void AnimationActionTrigger()
    {
        base.AnimationActionTrigger();
        CheckMeleeAttack();
        
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        detectedDamageables.Clear();
    }

    private void CheckMeleeAttack()
    {
        WeaponAttackDetails details = AttackWeaponData.AttackDetail[attackCounter];
        
        foreach (Idamageble item in detectedDamageables.ToList())
        {
            item.Damage(details.DamageAmount);
        }
        foreach (IKnockbackable item in detectedKnockbackables.ToList())
        {
            item.Knockback(details.KnockbackAngle,details.KnockbackStrength,movement.FacingDirection);
        }
    }

    public void AddToDetected(Collider2D collision)
    {
        Idamageble damageable = collision.GetComponent<Idamageble>();

        if (damageable != null)
        {
            detectedDamageables.Add(damageable);
        }

        IKnockbackable knockbackable = collision.GetComponent<IKnockbackable>();
        if (knockbackable != null)
        {
            detectedKnockbackables.Add(knockbackable);
        }
    }
    public void RemoveFromDetected(Collider2D collision)
    {
        Idamageble damageable = collision.GetComponent<Idamageble>();
        
        if (damageable != null)
        {
            detectedDamageables.Remove(damageable);
        }
        
        IKnockbackable knockbackable = collision.GetComponent<IKnockbackable>();
        
        if (knockbackable != null)
        {
            detectedKnockbackables.Remove(knockbackable);
        }
    }
    
}
