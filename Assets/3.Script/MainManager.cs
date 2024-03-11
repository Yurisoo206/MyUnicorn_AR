using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    UnicornController unicornController;

    public GameObject[] menu = new GameObject[3];
    public GameObject unicorn;
    public Transform pivotPoint;

    public bool isActive = false;

    private void Start()
    {
        unicornController = FindObjectOfType<UnicornController>();
    }

    public void Unicorn_Deployment()
    {
        if (!isActive && !unicornController.isActive)
        {
            isActive = true;
            unicorn.transform.position = new Vector3(pivotPoint.transform.position.x, pivotPoint.transform.position.y, pivotPoint.transform.position.z);
            unicorn.transform.rotation *= Quaternion.Euler(0f, 80, 0f);
        }
        else if (isActive && unicornController.isActive)
        {
            isActive = false;
        }
    }
    
    public void OnMenu()
    {
        if (!menu[0].activeSelf)
        {
            menu[0].SetActive(true);
        }

        else if (menu[0].activeSelf)
        {
            menu[0].SetActive(false);
        }
    }

    public void OnDraw()
    {
        if (!menu[1].activeSelf)
        {
            menu[1].SetActive(true);
            menu[2].SetActive(false);
        }
        else { menu[1].SetActive(false); }

    }

    public void OnDeployment()
    {
        if (!menu[2].activeSelf)
        {
            menu[2].SetActive(true);
            menu[1].SetActive(false);
        }
        else
        {
            menu[2].SetActive(false);
        }
    }

}
