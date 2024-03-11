using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARManager : MonoBehaviour
{
    [SerializeField]
    ARRaycastManager raycastManager;
    [SerializeField] Camera arCamera;

    [SerializeField] GameObject selectedPrefab;
    [SerializeField] GameObject selectedScroll;
    ARObject selectedObject;


    static List<ARRaycastHit> arHits = new List<ARRaycastHit>();
    static RaycastHit physicsHit;

    public void SetSelectedPrefeb(GameObject selectedPrefab)
    {
        this.selectedPrefab = selectedPrefab;
    }

    private void Awake()
    {
        if (selectedPrefab != null)
        {
            selectedPrefab = raycastManager.raycastPrefab;
        }
    }

    private void Update()
    {
        if (Input.touchCount == 0 || !selectedScroll.activeSelf)
        {
            return;
        }

        Touch touch = Input.GetTouch(0);
        Vector2 touchPosition = touch.position;

        if (IsPointOverUI(touchPosition)) { return; }

        if (touch.phase == TouchPhase.Began)
        {
            SelectedARObject(touchPosition);
        }
        else if (touch.phase == TouchPhase.Ended)
        {
            if (selectedObject)
            {
                selectedObject.Selected = false;
            }
        }

        SelectedARPlane(touchPosition);
    }

    bool SelectedARObject(Vector2 touchPosition)
    {
        Ray ray = arCamera.ScreenPointToRay(touchPosition);

        if (Physics.Raycast(ray, out physicsHit))
        {
            selectedObject = physicsHit.transform.GetComponent<ARObject>();
            if (selectedObject)
            {
                selectedObject.Selected = true;
                return true;
            }
        }
        return false;
    }

    void SelectedARPlane(Vector2 touchPosition)
    {
        if (raycastManager.Raycast(touchPosition, arHits, TrackableType.PlaneWithinPolygon))
        {
            Pose hitPos = arHits[0].pose;

            if (!selectedObject)
            {
                var newARObj = Instantiate(selectedPrefab, hitPos.position, hitPos.rotation);
                selectedObject = newARObj.AddComponent<ARObject>();
            }

            else if (selectedObject.Selected)
            {
                selectedObject.transform.position = hitPos.position;
                selectedObject.transform.rotation = hitPos.rotation;
            }
        }
    }


    bool IsPointOverUI(Vector2 pos)
    {
        PointerEventData eventDataCurPos = new PointerEventData(EventSystem.current);
        eventDataCurPos.position = pos;
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurPos, results);
        return results.Count > 0;
    }

    public void OnScroll()
    {
        if (selectedScroll.activeSelf)
        {
            selectedScroll.SetActive(false);
        }

        else if (!selectedScroll.activeSelf)
        {
            selectedScroll.SetActive(true);
        }
    }

    public void OnTrash()
    {
        if (selectedObject.Selected && selectedObject != null)
        {
            Destroy(selectedObject);
        }
    }

}