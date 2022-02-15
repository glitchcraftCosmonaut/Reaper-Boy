using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("HEALTH SYSTEM")]
    [SerializeField] protected float maxHealth;
    [SerializeField] protected FloatValueSO health;

    protected virtual void OnEnable()
    {
        health.Value = maxHealth;
    }
    public virtual void TakeDamage(float damage)
    {
        if(health.Value == 0f) return;
        // StartCoroutine(HurtEffect());
        health.Value -= damage / maxHealth;
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

    public virtual void RestoreHealth(float value)
    {
        if(health.Value == maxHealth) return;
        // health += value;
        // health = Mathf.Clamp(health, 0f, maxHealth);
        health.Value = Mathf.Clamp(health.Value + value, 0f, maxHealth);
    }

    protected IEnumerator HealthRegenerationCoroutine(WaitForSeconds waitTime, float percent)
    {
        while(health.Value < maxHealth)
        {
            yield return waitTime;

            RestoreHealth(maxHealth * percent);
        }
    }

    protected IEnumerator DamageOverTimeCoroutine(WaitForSeconds waitTime, float percent)
    {
        while(health.Value > 0f)
        {
            yield return waitTime;

            TakeDamage(maxHealth * percent);
        }
    }

}
