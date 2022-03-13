using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeUI : Singleton<UpgradeUI>
{
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.enabled = false;
    }

    // Update is called once per frame

    public void CallingMethod()
    {
        StartCoroutine(nameof(StartAnim));
    }
    
    IEnumerator StartAnim()
    {
        anim.enabled = true;
        yield return new WaitForSeconds(3f);
        anim.enabled = false;
    }
}
