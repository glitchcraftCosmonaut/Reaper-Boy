using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Statsbar : MonoBehaviour
{
    [SerializeField] Image fillImageBack;
    [SerializeField] Image fillImageFront;
    [SerializeField] bool delayFill = true;
    [SerializeField] float fillDelay = 0.5f;
    [SerializeField] float fillSpeed = 0.1f;
    float currentFillAmount;
    protected float targetFillAmount;
    float previousFillAmount;
    float t;

    [SerializeField] protected FloatValueSO currentHealth;
    [SerializeField] protected FloatValueSO maxHealth;


    WaitForSeconds waitForDelayFill;

    Coroutine bufferedFillCoroutine;
    Canvas canvas;

    private void Awake() 
    {
        // currentFillAmount = currentHealth.Value / maxHealth.Value;
        Initialize(currentHealth.Value);
        if(TryGetComponent<Canvas>(out Canvas canvas))
        {
            canvas.worldCamera = Camera.main;
        }

        waitForDelayFill = new WaitForSeconds(fillDelay);
    }

     private void OnEnable()
    {
        // currentHealth.OnValueChange += Initialize;
        currentHealth.OnValueChange += UpdateStates;
    }

    private void OnDisable()
    {
        // currentHealth.OnValueChange -= Initialize;
        currentHealth.OnValueChange -= UpdateStates;
    }

    public virtual void Initialize(float currentValue)
    {
        currentValue = currentHealth.Value;
        currentFillAmount = currentValue / maxHealth.Value;
        targetFillAmount = currentFillAmount;
        fillImageBack.fillAmount = currentFillAmount;
        fillImageFront.fillAmount = currentFillAmount;
    }

    public virtual void UpdateStates(float currentValue)
    {
        targetFillAmount = currentValue / maxHealth.Value;

        if(bufferedFillCoroutine != null)
        {
            StopCoroutine(bufferedFillCoroutine);
        }

        if(currentFillAmount > targetFillAmount)
        {
            fillImageFront.fillAmount = targetFillAmount;

            bufferedFillCoroutine = StartCoroutine(BufferedFillingCoroutine(fillImageBack));

            return;
        }

        if(currentFillAmount < targetFillAmount)
        {
            fillImageBack.fillAmount = targetFillAmount;

            bufferedFillCoroutine = StartCoroutine(BufferedFillingCoroutine(fillImageFront));
        }
    }

    protected virtual IEnumerator BufferedFillingCoroutine(Image image)
    {
        if(delayFill)
        {
            yield return waitForDelayFill;
        }
        previousFillAmount = currentFillAmount;
        t = 0f;
        while(t < 1f)
        {
            t += Time.deltaTime * fillSpeed;
            currentFillAmount = Mathf.Lerp(previousFillAmount, targetFillAmount, t);
            image.fillAmount = currentFillAmount;

            yield return null;
        }
    }
}
