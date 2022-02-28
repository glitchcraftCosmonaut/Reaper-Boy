using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EntranceScene : MonoBehaviour
{
    // public string lastExitName;
    public string entrancePassword;

    // private void Start()
    // {
    //     // if(PlayerPrefs.GetString("LastExitName") == lastExitName)
    //     // {
    //     //     Player.MyInstance.transform.position = transform.position;
    //     // }
        
    // }
    // public GameObject door;
    private void Start() 
    {
        if(PlayerManager.Instance.scenePassword == entrancePassword)
        {
            Player.MyInstance.transform.position = transform.position;
            Debug.Log("Enter!");
        }
        else
        {
            Debug.LogWarning("Wrong Password!");
        }
    }
}
