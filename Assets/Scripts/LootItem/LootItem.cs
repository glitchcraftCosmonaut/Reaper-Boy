using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootItem : MonoBehaviour
{
    int pickUpStateID = Animator.StringToHash("PickUp");
    public Animator animator;
    protected CollectibleManager collectibleManager;
    protected UniqueID uniqueID;

    protected virtual void Awake() 
    {
        animator = GetComponent<Animator>();
        uniqueID = GetComponent<UniqueID>();
        collectibleManager = FindObjectOfType<CollectibleManager>();
        if (collectibleManager.CollectedItems.Contains(uniqueID.ID))
        {
            gameObject.SetActive(false);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PickUp();
    }

    protected virtual void PickUp()
    {
        animator.Play(pickUpStateID);
    }
}
