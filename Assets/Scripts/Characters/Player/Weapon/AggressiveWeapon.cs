using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AggressiveWeapon : Weapon
{
    protected AggressiveWeapon_SO aggressiveWeaponData;

    private List<IDamageable> detectedDamageables = new List<IDamageable>();

    protected override void Awake()
    {
        base.Awake();

        if(weaponData.GetType() == typeof(AggressiveWeapon_SO))
        {
            aggressiveWeaponData = (AggressiveWeapon_SO)weaponData;
        }
        else
        {
            Debug.LogError("Wrong data for the weapon");
        }
    }

    public override void AnimationActionTrigger()
    {
        base.AnimationActionTrigger();

        CheckMeleeAttack();
    }

    private void CheckMeleeAttack()
    {
        WeaponAttackDetails details = aggressiveWeaponData.AttackDetails[attackCounter];

        foreach (IDamageable item in detectedDamageables.ToList())
        {
            item.Damage(details.damageAmount);
        }

        // foreach (IKnockbackable item in detectedKnockbackables.ToList())
        // {
        //     item.Knockback(details.knockbackAngle, details.knockbackStrength, core.Movement.FacingDirection);
        // }
    }

    public void AddToDetected(Collider2D collision)
    {

        IDamageable damageable = collision.GetComponent<IDamageable>();

        if(damageable != null)
        {
            detectedDamageables.Add(damageable);
        }

        // IKnockbackable knockbackable = collision.GetComponent<IKnockbackable>();

        // if(knockbackable != null)
        // {
        //     detectedKnockbackables.Add(knockbackable);
        // }
    }

    public void RemoveFromDetected(Collider2D collision)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();

        if (damageable != null)
        {
            detectedDamageables.Remove(damageable);
        }

        // IKnockbackable knockbackable = collision.GetComponent<IKnockbackable>();

        // if (knockbackable != null)
        // {
        //     detectedKnockbackables.Remove(knockbackable);
        // }
    }
}
