using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class walkForwardController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        fighterController.mvFWD = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        fighterController.mvFWD = false;
    }
}
