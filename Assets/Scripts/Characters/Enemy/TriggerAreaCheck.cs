using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAreaCheck : MonoBehaviour
{
    private Enemy_Behaviour enemyParent;

    private void Awake()
    {
        enemyParent = GetComponentInParent<Enemy_Behaviour>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            enemyParent.target = other.transform;
            enemyParent.enemyData.inRange = true;
            enemyParent.hotZone.SetActive(true);
        }
    }
}
