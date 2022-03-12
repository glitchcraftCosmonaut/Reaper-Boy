using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameplayUIController : MonoBehaviour
{
    [Header("Player Input")]
    [SerializeField] PlayerInput playerInput;

    // [Header("Audio Data")]
    // [SerializeField] AudioData pauseSFX;
    // [SerializeField] AudioData unpauseSFX;

    [Header("Canvas")]
    [SerializeField] Canvas hUDCanvas;
    [SerializeField] Canvas menusCanvas;
    [SerializeField] Canvas gameOverCanvas;

    [Header("button")]
    [SerializeField] Button resumeButton;
    [SerializeField] Button mainMenuButton;

    [SerializeField] Button restartButton;
    [SerializeField] Button mainMenuGameOverButton;
    
    const string PLAYER_KEY = "/player";
    int buttonPressedParameterID = Animator.StringToHash("Pressed");
    
    private void OnEnable()
    {
        playerInput.onPause += Pause;
        playerInput.onUnPause += Unpause;
        GameManager.onGameOver += OnGameOver;

        ButtonPressedBehaviour.buttonFunctionTable.Add(resumeButton.gameObject.name, OnResumeButtonClick);
        ButtonPressedBehaviour.buttonFunctionTable.Add(mainMenuButton.gameObject.name, OnMainMenuButtonClick);
        ButtonPressedBehaviour.buttonFunctionTable.Add(restartButton.gameObject.name, OnRestartButtonClick);
        ButtonPressedBehaviour.buttonFunctionTable.Add(mainMenuGameOverButton.gameObject.name, OnMainMenuButtonClick);
    }

    private void OnDisable()
    {
        playerInput.onPause -= Pause;
        playerInput.onUnPause -= Unpause;
        GameManager.onGameOver -= OnGameOver;
        ButtonPressedBehaviour.buttonFunctionTable.Clear();
    }
    private void Start()
    {
        restartButton.enabled = false;
        mainMenuGameOverButton.enabled = false;
    }

    void Pause()
    {
        hUDCanvas.enabled = false;
        menusCanvas.enabled = true;
        GameManager.GameState = GameState.Paused;
        TimeController.Instance.Pause();
        playerInput.EnablePauseInput();
        playerInput.SwitchToDynamicUpdateMode();
        UIInput.Instance.SelectUI(resumeButton);
        // AudioManager.Instance.PlaySFX(pauseSFX);
    }

    void Unpause()
    {
        // OnResumeButtonClick();
        resumeButton.Select();
        resumeButton.animator.SetTrigger(buttonPressedParameterID);
        // AudioManager.Instance.PlaySFX(unpauseSFX);
    }

    void OnResumeButtonClick()
    {
        hUDCanvas.enabled = true;
        menusCanvas.enabled = false;
        GameManager.GameState = GameState.Playing;
        TimeController.Instance.Unpause();
        playerInput.EnableGameplayInput();
        playerInput.SwitchToFixedUpdateMode();
    }

    private void OnGameOver()
    {
        hUDCanvas.enabled = false;
        gameOverCanvas.enabled = true;
        GameManager.GameState = GameState.GameOver;
        resumeButton.enabled = false;
        mainMenuButton.enabled = false;
        restartButton.enabled = true;
        mainMenuGameOverButton.enabled = true;
        playerInput.SwitchToDynamicUpdateMode();
        playerInput.DisableAllInput();
        UIInput.Instance.SelectUI(restartButton);
    }

     private void OnRestartButtonClick()
    {
        gameOverCanvas.enabled = false;
        playerInput.SwitchToFixedUpdateMode();
        Destroy(Player.MyInstance.gameObject);
        GameManager.GameState = GameState.Playing;
        if(SaveSystem.SaveExists(PLAYER_KEY))
        {
            SceneLoader.Instance.LoadSavedGamePlayScene();
        }
        else
        {
            SceneLoader.Instance.LoadGamePlayScene();
        }
    }

    void OnMainMenuButtonClick()
    {
        menusCanvas.enabled = false;
        SceneLoader.Instance.LoadMainMenuScene();
    }
}
