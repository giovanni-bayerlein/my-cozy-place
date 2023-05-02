using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Furniture : MonoBehaviour
{
    public float DragDelay = .1f;
    float dragDelayCounter;

    public FurnitureData Data;
    public bool CanBePlaced;

    BuildingController BuildingController;

    private void Start()
    {
        dragDelayCounter = DragDelay;

        BuildingController = FindObjectOfType<BuildingController>();

        CanBePlaced = true;
    }

    private void OnTriggerExit(Collider other)
    {
        CanBePlaced = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.root.TryGetComponent(out Furniture furniture))
        {
            Debug.Log(name + ": " + furniture.gameObject.name + ": " + furniture.gameObject.layer);

            CanBePlaced = false;
        }
        else
        {
            CanBePlaced = true;
        }
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;

        Debug.Log("click");
    }

    private void OnMouseUp()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;

        dragDelayCounter = DragDelay;

        BuildingController.StopDrag();

        Debug.Log("OnMouseUp");
    }

    private void OnMouseDrag()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;

        dragDelayCounter -= Time.deltaTime;

        if (dragDelayCounter > 0f) return;

        BuildingController.Drag(gameObject);

        Debug.Log("OnMouseDrag");

    }
}
