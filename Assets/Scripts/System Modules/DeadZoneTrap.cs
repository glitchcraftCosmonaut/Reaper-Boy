using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZoneTrap : MonoBehaviour
{
    [SerializeField] private LayerMask hittable;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((hittable & 1 << other.gameObject.layer) != 0)
        {
            
            // Player player = other.TryGetComponent<Player>(out Player player);
            if(other.TryGetComponent<Player>(out Player player))
            {
                player.TakeDamage(1000);  
            }
        }
    }
}
