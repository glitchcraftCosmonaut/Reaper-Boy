using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootTest : MonoBehaviour
{
    int pickUpStateID = Animator.StringToHash("PickUp");
    [SerializeField] int fullHealthScore = 200;

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
        collectibleManager.AddCoin(fullHealthScore);
        collectibleManager.CollectedItems.Add(uniqueID.ID);
    }
}
