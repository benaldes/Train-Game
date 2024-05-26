using System;
using UnityEngine;


public class WeaponHitBoxToWeapon : MonoBehaviour
{
    private AttackWeapons weapon;

    private void Awake()
    {
        weapon = GetComponentInParent<AttackWeapons>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        weapon.AddToDetected(other);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        weapon.RemoveFromDetected(other);
    }
}
