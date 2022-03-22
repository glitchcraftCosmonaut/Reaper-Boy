using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class MainMenuUIController : MonoBehaviour
{
    [Header("Canvas")]
    [SerializeField] Canvas titleCanvas;
    [SerializeField] Canvas mainMenuCanvas;
    [SerializeField] Canvas settingsCanvas;
    [SerializeField] Canvas startMenuCanvas;
    [SerializeField] Canvas collectibleCanvas;
    [SerializeField] Canvas confirmNewGameCanvas;
    // [SerializeField] Canvas howToPlayCanvas;

    [Header("Button")]
    [SerializeField] Button buttonStart;
    [SerializeField] Button buttonNewGame;
    [SerializeField] Button buttonYes;
    [SerializeField] Button buttonNo;
    [SerializeField] Button buttonLoad;
    [SerializeField] Button buttonCollectible;
    [SerializeField] Button buttonSettings;
    [SerializeField] Button buttonBackSettings;
    [SerializeField] Button buttonBackCollectible;
    [SerializeField] Button buttonBackStartMenu;

    [SerializeField] Button buttonQuit;

    [SerializeField] Slider bgVolumeSlider;
    [SerializeField] Slider effectVolumeSlider;

    private void OnEnable()
    {
        ButtonPressedBehaviour.buttonFunctionTable.Add(buttonStart.gameObject.name, OnButtonStartClick);
        ButtonPressedBehaviour.buttonFunctionTable.Add(buttonNewGame.gameObject.name, OnButtonNewGameClick);
        ButtonPressedBehaviour.buttonFunctionTable.Add(buttonYes.gameObject.name, OnButtonYesClick);
        ButtonPressedBehaviour.buttonFunctionTable.Add(buttonNo.gameObject.name, OnButtonNoClick);
        ButtonPressedBehaviour.buttonFunctionTable.Add(buttonBackStartMenu.gameObject.name, OnButtonBackStartMenuClick);
        ButtonPressedBehaviour.buttonFunctionTable.Add(buttonLoad.gameObject.name, OnButtonLoadClick);

        ButtonPressedBehaviour.buttonFunctionTable.Add(buttonCollectible.gameObject.name, OnButtonCollectibleClick);
        ButtonPressedBehaviour.buttonFunctionTable.Add(buttonBackCollectible.gameObject.name, OnButtonBackCollectibleClick);


        ButtonPressedBehaviour.buttonFunctionTable.Add(buttonSettings.gameObject.name, OnButtonSettingsClick);
        ButtonPressedBehaviour.buttonFunctionTable.Add(buttonBackSettings.gameObject.name, OnButtonBackClick);
        
        
        ButtonPressedBehaviour.buttonFunctionTable.Add(buttonQuit.gameObject.name, OnButtonQuitClick);

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
        buttonBackSettings.enabled = false;
        buttonBackCollectible.enabled = false;
        buttonBackStartMenu.enabled = false;
        buttonNewGame.enabled = false;
        buttonYes.enabled = false;
        buttonNo.enabled = false;
        buttonLoad.enabled = false;
        bgVolumeSlider.enabled = false;
        effectVolumeSlider.enabled = false;
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
        startMenuCanvas.enabled = true;

        #region Main Menu Button
        buttonStart.enabled = false;
        buttonCollectible.enabled = false;
        buttonSettings.enabled = false;
        buttonQuit.enabled = false;
        #endregion

        #region StartMenu Button
        buttonNewGame.enabled = true;
        buttonLoad.enabled =true;
        buttonBackStartMenu.enabled = true;
        UIInput.Instance.SelectUI(buttonNewGame);
        #endregion
        // SceneLoader.Instance.LoadGamePlayScene();
        // SaveSystem.SeriouslyDeleteAllSaveFiles();
        // howToPlayCanvas.enabled = false;
        // mainMenuCanvas.SetActive(false);
        // howToPlayCanvas.SetActive(false);
    }
    private void OnButtonNewGameClick()
    {
        startMenuCanvas.enabled = false;
        titleCanvas.enabled = false;
        confirmNewGameCanvas.enabled = true;
        buttonYes.enabled = true;
        buttonNo.enabled = true;
        buttonNewGame.enabled = false;
        buttonLoad.enabled = false;
        buttonBackStartMenu.enabled = false;
        UIInput.Instance.SelectUI(buttonNo);
    }
    void OnButtonLoadClick()
    {
        startMenuCanvas.enabled = false;
        titleCanvas.enabled = false;
        SceneLoader.Instance.LoadSavedGamePlayScene();
    }
    private void OnButtonYesClick()
    {
        SaveSystem.SeriouslyDeleteAllSaveFiles();
        confirmNewGameCanvas.enabled = false;
        buttonYes.enabled = false;
        buttonNo.enabled = false;
        SceneLoader.Instance.LoadGamePlayScene();
    }
    private void OnButtonNoClick()
    {
        startMenuCanvas.enabled = true;
        titleCanvas.enabled = true;
        confirmNewGameCanvas.enabled = false;
        buttonYes.enabled = false;
        buttonNo.enabled = false;
        buttonNewGame.enabled = true;
        buttonLoad.enabled = true;
        buttonBackStartMenu.enabled = true;
        UIInput.Instance.SelectUI(buttonNewGame);
    }
    private void OnButtonBackStartMenuClick()
    {
        startMenuCanvas.enabled = false;
        mainMenuCanvas.enabled = true;
        buttonStart.enabled = true;
        buttonCollectible.enabled = true;
        buttonSettings.enabled = true;
        buttonQuit.enabled = true;
        buttonNewGame.enabled = false;
        buttonLoad.enabled =false;
        buttonBackStartMenu.enabled = false;
        UIInput.Instance.SelectUI(buttonStart);
    }
    private void OnButtonCollectibleClick()
    {
        mainMenuCanvas.enabled = false;
        titleCanvas.enabled = false;
        collectibleCanvas.enabled = true;
        buttonBackCollectible.enabled = true;
        buttonStart.enabled = false;
        buttonCollectible.enabled = false;
        buttonSettings.enabled = false;
        buttonQuit.enabled =false;
        UIInput.Instance.SelectUI(buttonBackCollectible);

    }
    private void OnButtonBackCollectibleClick()
    {
        mainMenuCanvas.enabled = true;
        titleCanvas.enabled = true;
        collectibleCanvas.enabled = false;
        buttonBackCollectible.enabled = false;
        buttonStart.enabled = true;
        buttonCollectible.enabled = true;
        buttonSettings.enabled = true;
        buttonQuit.enabled =true;
        UIInput.Instance.SelectUI(buttonStart);
    }


     void OnButtonSettingsClick()
    {
        mainMenuCanvas.enabled = false;
        titleCanvas.enabled = false;
        settingsCanvas.enabled = true;
        buttonBackSettings.enabled = true;
        bgVolumeSlider.enabled = true;
        effectVolumeSlider.enabled = true;
        buttonStart.enabled = false;
        buttonCollectible.enabled = false;
        buttonSettings.enabled = false;
        buttonQuit.enabled =false;
        UIInput.Instance.SelectUI(bgVolumeSlider);

    }

    void OnButtonBackClick()
    {
        mainMenuCanvas.enabled = true;
        titleCanvas.enabled = true;
        settingsCanvas.enabled = false;
        buttonBackSettings.enabled = false;
        bgVolumeSlider.enabled = false;
        effectVolumeSlider.enabled = false;
        buttonStart.enabled = true;
        buttonCollectible.enabled = true;
        buttonSettings.enabled = true;
        buttonQuit.enabled =true;
        AudioManager.Instance.SaveSoundSettings();
        // mainMenuCanvas.SetActive(true);
        // howToPlayCanvas.SetActive(false);
        UIInput.Instance.SelectUI(buttonStart);

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
