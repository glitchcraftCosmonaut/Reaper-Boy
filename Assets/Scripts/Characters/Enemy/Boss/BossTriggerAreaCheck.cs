using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTriggerAreaCheck : MonoBehaviour
{
    private Boss bossParent;

    private void Awake()
    {
        bossParent = GetComponentInParent<Boss>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            bossParent.target = other.transform;
            bossParent.enemyData.inRange = true;
            bossParent.hotZone.SetActive(true);
        }
    }
}
