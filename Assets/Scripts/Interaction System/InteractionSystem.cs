using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractionSystem : MonoBehaviour
{

    public abstract void Start();
    public abstract void Interact();

    // private void OnTriggerEnter2D(Collider2D collision) 
    // {
    //     if(collision.CompareTag("Player"))
    //     {
    //         // collision.GetComponent<Player>().OpenInteractableIcon();
    //         Player.MyInstance.OpenInteractableIcon();
    //     }
    // }
    // private void OnTriggerExit2D(Collider2D collision) 
    // {
    //     if(collision.CompareTag("Player"))
    //     {
    //         // collision.GetComponent<Player>().CloseInteractableIcon();
    //         Player.MyInstance.CloseInteractableIcon();
    //     }
    // }
}
