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
    [SerializeField] AudioData selectSFX;
    [SerializeField] AudioData submitSFX;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        AudioSetting.Instance.PlaySFX(selectSFX);
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        AudioSetting.Instance.PlaySFX(submitSFX);
    }

    public void OnSelect(BaseEventData eventData)
    {
        AudioSetting.Instance.PlaySFX(selectSFX);
    }

    public void OnSubmit(BaseEventData eventData)
    {
        AudioSetting.Instance.PlaySFX(submitSFX);
    }
}
