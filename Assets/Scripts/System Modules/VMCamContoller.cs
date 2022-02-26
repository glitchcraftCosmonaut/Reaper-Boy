using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class VMCamContoller : MonoBehaviour
{
    private GameObject virtualCam;
    public GameObject tPlayer;
    public Transform tFollowTarget;
    private CinemachineVirtualCamera vcam;

    private void OnEnable()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
    }
    void Start()
    {
        // if (tPlayer == null)
        // {
        //     tPlayer = GameObject.FindWithTag("Player");
        //     if (tPlayer != null)
        //     {
        //         tFollowTarget = tPlayer.transform;
        //         vcam.LookAt = tFollowTarget;
        //         vcam.Follow = tFollowTarget;
        //     }
        // }
    }
 
    // Update is called once per frame
    void Update()
    {
        if (tPlayer == null)
        {
            tPlayer = GameObject.FindWithTag("Player");
            if (tPlayer != null)
            {
                tFollowTarget = tPlayer.transform;
                vcam.LookAt = tFollowTarget;
                vcam.Follow = tFollowTarget;
            }
        }
    }
}
