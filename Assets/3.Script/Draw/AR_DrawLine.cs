using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AR_DrawLine : MonoBehaviour
{
    public Transform pivotPoint;
    public GameObject linePrefabs;
    LineRenderer lineRenderer;

    public List<LineRenderer> lineList = new List<LineRenderer>();

    public Transform linePool;

    public bool isUse;
    public bool isStartLine;

    public string color= "";

    void Update()
    {
        if (isUse)
        {
            if (isStartLine)
            {
                
                DrawLineContinue();
            }
        }
    }

    public void MakeLineRenderer()
    {
        GameObject t_Line = Instantiate(linePrefabs);
        t_Line.transform.SetParent(linePool);
        t_Line.transform.position = Vector3.zero;
        t_Line.transform.localScale = new Vector3(1, 1, 1);

        lineRenderer = t_Line.GetComponent<LineRenderer>();
        ChangeColor();
        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, pivotPoint.position);

        isStartLine = true;
        lineList.Add(lineRenderer);
    }

    public void DrawLineContinue()
    {
        lineRenderer.positionCount = lineRenderer.positionCount + 1;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, pivotPoint.position);
    }

    public void ChangeColor()
    {
        switch (color)
        {
            case "Red":
                lineRenderer.startColor = Color.red;
                lineRenderer.endColor = Color.red;
                break;

            case "Blue":
                lineRenderer.startColor = Color.blue;
                lineRenderer.endColor = Color.blue;
                break;

            case "Green":
                lineRenderer.startColor = Color.green;
                lineRenderer.endColor = Color.green;
                break;

            case "White":
                lineRenderer.startColor = Color.white;
                lineRenderer.endColor = Color.white;
                break;

            case "Black":
                lineRenderer.startColor = Color.black;
                lineRenderer.endColor = Color.black;
                break;
        }
    }

    public void OnTrash()
    {
        if (lineList.Count >= 1)
        {
            Destroy(lineList[lineList.Count - 1]);
            lineList.RemoveAt(lineList.Count - 1);
        }
        
    }

    public void StartDrawline()
    {
        isUse = true;
        if (!isStartLine)
        {
            MakeLineRenderer();
        }
    }

    public void StopDrawline()
    {
        isUse = false;
        isStartLine = false;
        lineRenderer = null;
    }
}
