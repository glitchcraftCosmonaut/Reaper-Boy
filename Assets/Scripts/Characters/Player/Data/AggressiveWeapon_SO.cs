using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="newAggressiveWeaponData", menuName ="Data/Weapon Data/Aggressive Weapon")]
public class AggressiveWeapon_SO : WeaponData_SO
{
    [SerializeField] private WeaponAttackDetails[] attackDetails;

    public WeaponAttackDetails[] AttackDetails { get => attackDetails; private set => attackDetails = value; }

    private void OnEnable()
    {
        amountOfAttacks = attackDetails.Length;

        movementSpeed = new float[amountOfAttacks];

        for (int i = 0; i < amountOfAttacks; i++)
        {
            movementSpeed[i] = attackDetails[i].movementSpeed;
        }
    }
}
