using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHotZone : MonoBehaviour
{
    Boss bossParent;
    private bool inRange;
    private Animator anim;
    public string attackAnim;

    private void Awake()
    {
        bossParent = GetComponentInParent<Boss>();
        anim = GetComponentInParent<Animator>();
    }

    private void Update()
    {
        if(inRange && !anim.GetCurrentAnimatorStateInfo(0).IsName(attackAnim))
        {
            bossParent.Flip();
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            inRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            inRange = false;
            gameObject.SetActive(false);
            bossParent.triggerArea.SetActive(true);
            bossParent.enemyData.inRange = false;
            bossParent.SelectTarget();
        }
    }
}
