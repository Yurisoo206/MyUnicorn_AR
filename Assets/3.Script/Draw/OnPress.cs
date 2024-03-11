using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnPress : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public AR_DrawLine drawLine;

    bool isPress = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        isPress = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPress = false;
        drawLine.StopDrawline();
    }

    void Update()
    {
        if (isPress)
        {
            drawLine.StartDrawline();
        }
    }
}
