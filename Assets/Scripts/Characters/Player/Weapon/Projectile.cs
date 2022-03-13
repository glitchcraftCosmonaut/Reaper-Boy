using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] protected GameObject hitVFX;
    [SerializeField] AudioData hitSFX;
    [SerializeField] protected float damage;
    [SerializeField] protected float moveSpeed = 10f;

    [SerializeField] protected Vector2 moveDirection;

    protected GameObject target;

    private void Awake() 
    {
        if(moveDirection != Vector2.right)
        {
            transform.rotation = Quaternion.FromToRotation(Vector2.right, moveDirection);
        }
    }
    protected virtual void OnEnable() 
    {
        StartCoroutine(MoveDirectlyCoroutine());
    }
    
    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.TryGetComponent<Enemy_Behaviour>(out Enemy_Behaviour enemy_Behaviour))
        {
            enemy_Behaviour.Damage(damage);
            PoolManager.Release(hitVFX, other.ClosestPoint(transform.position));
            AudioSetting.Instance.PlaySFX(hitSFX);
            gameObject.SetActive(false);
        }
        if(other.gameObject.TryGetComponent<Boss_Behaviour>(out Boss_Behaviour boss))
        {
            boss.Damage(damage);
            PoolManager.Release(hitVFX, other.ClosestPoint(transform.position));
            AudioSetting.Instance.PlaySFX(hitSFX);
            gameObject.SetActive(false);
        }
    }
    IEnumerator MoveDirectlyCoroutine()
    {
        while(gameObject.activeSelf)
        {
            Move();
            yield return null;
        }
    }


    public void Move() => transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
}

