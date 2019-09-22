using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class walkBackController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        fighterController.mvBack = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        fighterController.mvBack = false;
    }
}
