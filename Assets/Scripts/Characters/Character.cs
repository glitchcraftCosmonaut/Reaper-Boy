using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("HEALTH SYSTEM")]
    [SerializeField] protected FloatValueSO maxHealth;
    [SerializeField] protected FloatValueSO health;

    protected virtual void OnEnable()
    {
        health.Value = maxHealth.Value;
        // health.Value = 1;
    }
    public virtual void TakeDamage(int damage)
    {
        if(health.Value == 0f) return;
        // StartCoroutine(HurtEffect());
        health.Value -= damage / maxHealth.Value;
        if(health.Value <= 0)
        {
            Die();
        }
        
    }

     public virtual void Die()
    {
        health.Value = 0;
        // AudioManager.Instance.PlayRandomSFX(deathSFX);
        // PoolManager.Release(deathVFX, transform.position);
        gameObject.SetActive(false);
    }

    public virtual void RestoreHealth(int value)
    {
        if(health.Value == maxHealth.Value) return;
        // health += value;
        // health = Mathf.Clamp(health, 0f, maxHealth);
        health.Value = Mathf.Clamp(health.Value + value, 0f, maxHealth.Value);
    }

    // protected IEnumerator HealthRegenerationCoroutine(WaitForSeconds waitTime, int percent)
    // {
    //     while(health.Value < maxHealth.Value)
    //     {
    //         yield return waitTime;

    //         RestoreHealth(maxHealth.Value * percent);
    //     }
    // }

    // protected IEnumerator DamageOverTimeCoroutine(WaitForSeconds waitTime, int percent)
    // {
    //     while(health.Value > 0f)
    //     {
    //         yield return waitTime;

    //         TakeDamage(maxHealth.Value * percent);
    //     }
    // }

}
