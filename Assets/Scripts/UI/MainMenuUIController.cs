using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuUIController : MonoBehaviour
{
    [Header("Canvas")]
    [SerializeField] Canvas mainMenuCanvas;
    [SerializeField] Canvas settingsCanvas;
    // [SerializeField] Canvas howToPlayCanvas;

    [Header("Button")]
    [SerializeField] Button buttonStart;
    [SerializeField] Button buttonSettings;
    [SerializeField] Button buttonBack;

    [SerializeField] Button buttonLoad;
    [SerializeField] Button buttonQuit;


    private void OnEnable()
    {
        ButtonPressedBehaviour.buttonFunctionTable.Add(buttonStart.gameObject.name, OnButtonStartClick);
        ButtonPressedBehaviour.buttonFunctionTable.Add(buttonSettings.gameObject.name, OnButtonSettingsClick);
        ButtonPressedBehaviour.buttonFunctionTable.Add(buttonQuit.gameObject.name, OnButtonQuitClick);
        ButtonPressedBehaviour.buttonFunctionTable.Add(buttonLoad.gameObject.name, OnButtonLoadClick);
        ButtonPressedBehaviour.buttonFunctionTable.Add(buttonBack.gameObject.name, OnButtonBackClick);

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
        if(SaveSystem.SaveExists("player"))
        {
            buttonLoad.gameObject.SetActive(true);
        }
        else
        {
            buttonLoad.gameObject.SetActive(false);
        }
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
     void OnButtonSettingsClick()
    {
        mainMenuCanvas.enabled = false;
        settingsCanvas.enabled = true;
        buttonBack.enabled = true;
        buttonSettings.enabled = false;
        buttonStart.enabled = false;
        buttonQuit.enabled =false;
        // mainMenuCanvas.SetActive(false);
        // howToPlayCanvas.SetActive(true);
        // UIInput.Instance.SelectUI(buttonHowToPlay);
        UIInput.Instance.SelectUI(buttonBack);

    }

    void OnButtonBackClick()
    {
        mainMenuCanvas.enabled = true;
        settingsCanvas.enabled = false;
        buttonBack.enabled = false;
        buttonSettings.enabled = true;
        buttonStart.enabled = true;
        buttonQuit.enabled =true;
        AudioManager.Instance.SaveSoundSettings();
        // mainMenuCanvas.SetActive(true);
        // howToPlayCanvas.SetActive(false);
        UIInput.Instance.SelectUI(buttonStart);

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
