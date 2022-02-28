using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuUIController : MonoBehaviour
{
    [Header("Canvas")]
    [SerializeField] Canvas mainMenuCanvas;
    // [SerializeField] Canvas howToPlayCanvas;

    [Header("Button")]
    [SerializeField] Button buttonStart;
    [SerializeField] Button buttonLoad;
    [SerializeField] Button buttonQuit;


    private void OnEnable()
    {
        ButtonPressedBehaviour.buttonFunctionTable.Add(buttonStart.gameObject.name, OnButtonStartClick);
        ButtonPressedBehaviour.buttonFunctionTable.Add(buttonQuit.gameObject.name, OnButtonQuitClick);
        ButtonPressedBehaviour.buttonFunctionTable.Add(buttonLoad.gameObject.name, OnButtonLoadClick);
    }

    private void OnDisable()
    {
        ButtonPressedBehaviour.buttonFunctionTable.Clear();
    }

    void Start()
    {
        Time.timeScale = 1f;
        GameManager.GameState = GameState.Playing;
        UIInput.Instance.SelectUI(buttonStart);
    }
    void OnButtonStartClick()
    {
        mainMenuCanvas.enabled = false;
        SceneLoader.Instance.LoadGamePlayScene();
        SaveSystem.SeriouslyDeleteAllSaveFiles();
        // howToPlayCanvas.enabled = false;
        // mainMenuCanvas.SetActive(false);
        // howToPlayCanvas.SetActive(false);
    }

    void OnButtonLoadClick()
    {
        mainMenuCanvas.enabled = false;
        SceneLoader.Instance.LoadSavedGamePlayScene();
    }

    // void OnButtonBackClick()
    // {
    //     mainMenuCanvas.enabled = true;
    //     // howToPlayCanvas.enabled = false;
    //     buttonLoad.enabled = false;
    //     buttonStart.enabled = true;
    //     buttonQuit.enabled =true;
    //     // mainMenuCanvas.SetActive(true);
    //     // howToPlayCanvas.SetActive(false);
    //     UIInput.Instance.SelectUI(buttonStart);

    // }

    void OnButtonQuitClick()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
