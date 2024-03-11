using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARObject : MonoBehaviour
{
    [SerializeField] bool isSelected;

    MeshRenderer meshRenderer;
    Color originColor;


    public bool Selected
    {
        get 
        { 
            return this.isSelected;
        }
        set 
        {
            isSelected = value;
            UpdateMaterialColor();
        }
    }

    private void Awake()
    {
        Debug.Log("111");
        meshRenderer = GetComponent<MeshRenderer>();
        if (!meshRenderer)
        {
            meshRenderer = this.gameObject.AddComponent<MeshRenderer>();
        }

        originColor = meshRenderer.material.color;

    }

    private void UpdateMaterialColor()
    {
        if (isSelected) 
        {
            meshRenderer.material.color = Color.gray;
        }
        else 
        {
            meshRenderer.material.color = originColor;
        }

    }


}
