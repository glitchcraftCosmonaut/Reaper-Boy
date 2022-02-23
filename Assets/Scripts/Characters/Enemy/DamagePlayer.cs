using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    [SerializeField] private LayerMask hittable;
    private Enemy_Behaviour enemyParent;

    private EnemyBehaviourData data;

    private void Awake()
    {
        data = GetComponentInParent<Enemy_Behaviour>().enemyData;
        enemyParent = GetComponentInParent<Enemy_Behaviour>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((hittable & 1 << other.gameObject.layer) != 0)
        {
            
            // Player player = other.TryGetComponent<Player>(out Player player);
            if(other.TryGetComponent<Player>(out Player player))
            {
                player.TakeDamage(data.attackDamage);  
            }
        }
    }
}


