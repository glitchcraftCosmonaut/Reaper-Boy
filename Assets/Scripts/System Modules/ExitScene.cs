using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitScene : Singleton<ExitScene>
{
    // public string sceneToLoad;
    // public string exitName;
    public string sceneName;
    [SerializeField] private string newScenePassword;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            PlayerManager.Instance.scenePassword = newScenePassword;
            // PlayerPrefs.SetString("LastExitName",exitName);
            // SceneLoader.Instance.LoadNextScene(sceneName);
            SceneManager.LoadScene(sceneName);
        }
    }
}
