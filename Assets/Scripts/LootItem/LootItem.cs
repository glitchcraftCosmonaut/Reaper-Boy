using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootItem : MonoBehaviour
{
    int pickUpStateID = Animator.StringToHash("PickUp");
    Animator animator;



    protected virtual void Awake() 
    {
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
