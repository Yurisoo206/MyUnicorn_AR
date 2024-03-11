using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BtnManager : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    ARManager arManager;
    AR_DrawLine drawLine;
    [SerializeField] GameObject colorTab;
    [SerializeField] GameObject trashBtn;

    [SerializeField] Image colirBtn_img;


    bool isPress = false;

    private void Start()
    {
        drawLine = FindAnyObjectByType<AR_DrawLine>();
        arManager = FindObjectOfType<ARManager>();
    }

    public void OnColor(string color)
    {
        if(color == "Red")
        {
            colirBtn_img.color = Color.red;
            drawLine.color = color;
        }
        else if(color == "Blue")
        {
            colirBtn_img.color = Color.blue;
            drawLine.color = color;
        }
        else if (color == "Green")
        {
            colirBtn_img.color = Color.green;
            drawLine.color = color;
        }
        else if (color == "White")
        {
            colirBtn_img.color = Color.white;
            drawLine.color = color;
        }
        else if (color == "Black")
        {
            colirBtn_img.color = Color.black;
            drawLine.color = color;
        }
    }

    public void OnColorTab()
    {
        if (!colorTab.activeSelf)
        {
            colorTab.SetActive(true);
        }
        else if (colorTab.activeSelf)
        {
            colorTab.SetActive(false);
        }
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        if (trashBtn != null && eventData.pointerPress == trashBtn)
        {
            isPress = true;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (trashBtn != null && eventData.pointerPress == trashBtn)
        {
            isPress = false;
            arManager.OnTrash();
        }
    }
}
