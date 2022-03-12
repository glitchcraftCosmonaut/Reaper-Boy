using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : PersistentSingleton<SceneLoader>
{
    [SerializeField] UnityEngine.UI.Image transitionImage;
    [SerializeField] float fadeTime = 3.5f;
    public PlayerSaveData data { get; set; } = new PlayerSaveData();
    [SerializeField] PlayerInput playerInput;

    Color color;
    private string sceneToContinue;
    const string PLAYER_KEY = "/player";
    const string GAMEPLAY = "Stage 1";
    const string MAIN_MENU = "Main Menu";
    const string SCORING = "Scoring";

    IEnumerator LoadingCoroutine(string sceneName)
    {
        //load new scene in background
        var loadingOperation = SceneManager.LoadSceneAsync(sceneName);
        //set this scene inactive
        loadingOperation.allowSceneActivation = false;

        transitionImage.gameObject.SetActive(true);

        //fade out
        while(color.a < 1f)
        {
            color.a = Mathf.Clamp01(color.a + Time.unscaledDeltaTime / fadeTime);
            transitionImage.color = color;

            yield return null;
        }

        yield return new WaitUntil(() => loadingOperation.progress >= 0.9f);

        //active the new scene
        loadingOperation.allowSceneActivation = true;
        if(Player.MyInstance != null)
        {
            // Instantiate(Player.MyInstance.gameObject);
            Player.MyInstance.gameObject.SetActive(true);
            // Player.MyInstance.Anim.enabled = true;
        }
        playerInput.EnableGameplayInput();
        // SceneManager.LoadScene(sceneName);
        // Player.MyInstance.health.Value = 1;

        //fade in
        while(color.a > 0f)
        {
            color.a = Mathf.Clamp01(color.a - Time.unscaledDeltaTime / fadeTime);
            transitionImage.color = color;

            yield return null;
        }

        transitionImage.gameObject.SetActive(false);
    }


    public void LoadGamePlayScene()
    {
        StopAllCoroutines();
        StartCoroutine(LoadingCoroutine(GAMEPLAY));
    }
    public void LoadNextScene(string sceneName)
    {
        StopAllCoroutines();
        StartCoroutine(LoadingCoroutine(sceneName));
    }
    public void LoadSavedGamePlayScene()
    {
        StopAllCoroutines();
        data = SaveSystem.Load<PlayerSaveData>(PLAYER_KEY);

        StartCoroutine(LoadingCoroutine(data.MyPlayerData.MySceneName));
        
    }

    public void LoadMainMenuScene()
    {
        StopAllCoroutines();
        StartCoroutine(LoadingCoroutine(MAIN_MENU));
    }
    public void LoadScoringScene()
    {
        StopAllCoroutines();
        StartCoroutine(LoadingCoroutine(SCORING));
    }
}
