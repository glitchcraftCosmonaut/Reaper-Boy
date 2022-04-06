using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EntranceScene : MonoBehaviour
{
    // public string lastExitName;
    public string entrancePassword;
    private void Start() 
    {
        if(PlayerManager.Instance.scenePassword == entrancePassword)
        {
            Player.MyInstance.transform.position = transform.position;
            Time.timeScale = 1f;
            Debug.Log("Enter!");
        }
        else
        {
            Debug.LogWarning("Wrong Password!");
        }
    }
}
