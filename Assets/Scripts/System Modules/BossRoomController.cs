using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoomController : MonoBehaviour
{
    public GameObject roomConfiner;
    public Boss_Behaviour boss;

    private void Awake() 
    {
        if(boss.isDeath == true)
        {
            roomConfiner.SetActive(false);
            gameObject.SetActive(false);
        } 
    }
    private void Update()
    {
        if(boss.isDeath == true)
        {
            roomConfiner.SetActive(false);
            gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            roomConfiner.SetActive(true);
            boss.enableAct = true;
            boss.healthBarCanvas.enabled = true;
        }
    }
    // private void OnTriggerExit2D(Collider2D other)
    // {
    //     if(other.CompareTag("Player") && !other.isTrigger)
    //     {
    //         roomConfiner.SetActive(false);
    //     }
    // }
}