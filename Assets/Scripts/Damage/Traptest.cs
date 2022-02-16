using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traptest : MonoBehaviour
{
        [SerializeField] private LayerMask hittable;
        [SerializeField] private int damage = 20;

        private void DestroyThrowable()
        {
            StopAllCoroutines();
            Destroy(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if ((hittable & 1 << other.gameObject.layer) != 0)
            {
                // Player player = other.TryGetComponent<Player>(out Player player);
                if(other.TryGetComponent<Player>(out Player player))
                {
                    player.TakeDamage(damage);
                }
                DestroyThrowable();
            }
        }

}
