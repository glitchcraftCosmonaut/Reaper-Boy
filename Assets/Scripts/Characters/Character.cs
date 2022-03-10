using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    //pool manager here
    
    [Header("HEALTH SYSTEM")]
    [SerializeField] protected float maxHealth;
    [SerializeField] protected FloatValueSO health;

    [Header("HURT EFFECT")]
    [SerializeField] protected Material hurtMat;
    protected SpriteRenderer sp;
    protected Material defaultMat2D;

    protected virtual void OnEnable()
    {
        //debug
        // health.Value = maxHealth;
        health.Value = 1;
    }
    public virtual void TakeDamage(float damage)
    {
        if(health.Value == 0f) return;
        StartCoroutine(HurtEffect());
        health.Value -= damage / maxHealth;
        if(health.Value <= 0.1)
        {
            Die();
        }
        
    }

     public virtual void Die()
    {
        health.Value = 0;
        // AudioManager.Instance.PlayRandomSFX(deathSFX);
        // PoolManager.Release(deathVFX, transform.position);
        // gameObject.SetActive(false);
    }

    public virtual void RestoreHealth(int value)
    {
        if(health.Value == maxHealth) return;
        // health += value;
        // health = Mathf.Clamp(health, 0f, maxHealth);
        health.Value = Mathf.Clamp(health.Value + value, 0f, maxHealth);
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

    IEnumerator HurtEffect()
    {
        sp.material = hurtMat;
        yield return new WaitForSeconds(0.1f);
        sp.material = defaultMat2D;
    }

}
