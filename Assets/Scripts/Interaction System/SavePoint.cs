using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : InteractionSystem
{
    Animator saveAnim;
    Animation saveAnimation;

    public void Awake()
    {
        saveAnim = GetComponent<Animator>();
    }
    // private void Awake()
    // {
    //     saveAnim = GetComponent<Animator>();
    // }

    public override void Interact()
    {
        saveAnim.SetTrigger("Interacted");
        GameEvents.OnSaveInitiated();
    }

}
