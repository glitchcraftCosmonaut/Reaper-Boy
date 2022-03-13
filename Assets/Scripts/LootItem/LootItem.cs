using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootItem : Singleton<LootItem>
{
    int pickUpStateID = Animator.StringToHash("PickUp");
    Animator animator;



    protected override void Awake() 
    {
        base.Awake();
        animator = GetComponent<Animator>();   
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            PickUp();
        }
    }

    protected virtual void PickUp()
    {
        animator.Play(pickUpStateID);
    }
}
