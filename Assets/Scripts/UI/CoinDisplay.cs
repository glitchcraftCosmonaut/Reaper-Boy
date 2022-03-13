using UnityEngine;
using UnityEngine.UI;


public class CoinDisplay : MonoBehaviour
{
    static Text coinText;

    private void Awake()
    {
        coinText = GetComponent<Text>();

    }
    private void Start()
    {
        coinText.text = "X " + CollectibleManager.Instance.Coin.ToString();
    }

    public static void UpdateText(int coin) => coinText.text = "X " + coin.ToString();

    public static void ScaleText(Vector3 targetScale)
    {
        coinText.rectTransform.localScale = targetScale;
    }
}