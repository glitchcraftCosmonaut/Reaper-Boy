using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialCollectDisplay : MonoBehaviour
{
    static Text specialCollectText;

    private void Awake()
    {
        specialCollectText = GetComponent<Text>();

    }
    private void Start()
    {
        // specialCollectText.text = "X " + CollectibleManager.Instance.SpecialCollectible.ToString();
        if(SaveSystem.SaveExists("player"))
        {
            specialCollectText.text = "X " + CollectibleManager.Instance.SpecialCollectible.ToString();
        }

        else
        {
            CollectibleManager.Instance.ResetScore();
        }
    }

    public static void UpdateText(int special) => specialCollectText.text = "X " + special.ToString();

    public static void ScaleText(Vector3 targetScale)
    {
        specialCollectText.rectTransform.localScale = targetScale;
    }
}
