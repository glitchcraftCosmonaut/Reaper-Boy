using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] PlayerInput playerInput;

    [SerializeField] Canvas HUDCanvas;
    
    [Header("button")]
    [SerializeField] Button restartButton;
    [SerializeField] Button mainMenuButton;

    int buttonPressedParameterID = Animator.StringToHash("Pressed");
    Canvas canvas;

    private void Awake()
    {
        canvas = GetComponent<Canvas>();

        canvas.enabled = false;  
    }
    private void OnEnable()
    {
        GameManager.onGameOver += OnGameOver;


        ButtonPressedBehaviour.buttonFunctionTable.Add(restartButton.gameObject.name, OnRestartButtonClick);
        // ButtonPressedBehavior.buttonFunctionTable.Add(optionButton.gameObject.name, OnOptionButtonClick);
        ButtonPressedBehaviour.buttonFunctionTable.Add(mainMenuButton.gameObject.name, OnMainMenuButtonClick);
    }


    private void OnDisable()
    {
        GameManager.onGameOver -= OnGameOver;
        ButtonPressedBehaviour.buttonFunctionTable.Clear();
    }
    private void OnGameOver()
    {
        HUDCanvas.enabled = false;
        canvas.enabled = true;
        GameManager.GameState = GameState.GameOver;
        playerInput.SwitchToDynamicUpdateMode();
        UIInput.Instance.SelectUI(restartButton);
        playerInput.DisableAllInput();
    }
    private void OnRestartButtonClick()
    {
        canvas.enabled = false;
        SceneLoader.Instance.LoadSavedGamePlayScene();
    }
    private void OnMainMenuButtonClick()
    {
        canvas.enabled = false;
        SceneLoader.Instance.LoadMainMenuScene();
    }

}
