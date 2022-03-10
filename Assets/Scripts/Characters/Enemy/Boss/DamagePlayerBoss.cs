using UnityEngine;

public class DamagePlayerBoss : MonoBehaviour
{
   [SerializeField] private LayerMask hittable;
    private Boss_Behaviour enemyParent;

    private EnemyBehaviourData data;

    private void Awake()
    {
        data = GetComponentInParent<Boss_Behaviour>().enemyData;
        enemyParent = GetComponentInParent<Boss_Behaviour>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((hittable & 1 << other.gameObject.layer) != 0)
        {    
            if(other.TryGetComponent<Player>(out Player player))
            {
                player.TakeDamage(data.attackDamage);  
            }
        }
    }
}
