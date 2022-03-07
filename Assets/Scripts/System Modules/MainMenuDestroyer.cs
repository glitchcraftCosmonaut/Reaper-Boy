using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuDestroyer : MonoBehaviour
{

    private void Awake()
    {
        if(CameraController.Instance != null)
        {
            Destroy(CameraController.Instance.gameObject);
        }
        if(PlayerManager.Instance != null)
        {
            Destroy(PlayerManager.Instance.gameObject);
        }
    }
}
