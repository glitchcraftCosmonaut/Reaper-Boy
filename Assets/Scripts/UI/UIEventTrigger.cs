using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIEventTrigger : 
    MonoBehaviour,
    IPointerEnterHandler,
    IPointerDownHandler,
    ISelectHandler,
    ISubmitHandler
{
     public void OnPointerEnter(PointerEventData eventData)
    {
        // AudioManager.Instance.PlaySFX(selectSFX);
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        // AudioManager.Instance.PlaySFX(submitSFX);
    }

    public void OnSelect(BaseEventData eventData)
    {
        // AudioManager.Instance.PlaySFX(selectSFX);
    }

    public void OnSubmit(BaseEventData eventData)
    {
        // AudioManager.Instance.PlaySFX(submitSFX);

    }
}
