using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EntranceScene : MonoBehaviour
{
    public string lastExitName;

    private void Start()
    {
        if(PlayerPrefs.GetString("LastExitName") == lastExitName)
        {
            PlayerManager.Instance.transform.position = transform.position;
        }
    }
}
