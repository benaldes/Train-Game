using System;
using UnityEngine;
[CreateAssetMenu(fileName = "NewAttackWeaponData",menuName = "Data/Weapon Data/Attack Weapon")]
public class SO_AttackWeaponData : SO_WeaponData
{
    [SerializeField] private WeaponAttackDetails[] AttackDetails;

    public WeaponAttackDetails[] AttackDetail
    {
        get => AttackDetails;
        private set => AttackDetails = value;
    }
    private void OnEnable()
    {
        amountOfAttacks = AttackDetails.Length;
        movementSpeed = new float[amountOfAttacks];

        for (int i = 0; i < amountOfAttacks; i++)
        {
            movementSpeed[i] = AttackDetails[i].MovementSpeed;
        }
    }
}
