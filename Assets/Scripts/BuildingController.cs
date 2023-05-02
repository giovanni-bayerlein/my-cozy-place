using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : MonoBehaviour
{
    public GameObject SelectedPrefab;

    public LayerMask GroundLayer, WallLayer, DefaultLayer;

    public GameController GameController;

    public float Size;

    private void Start()
    {
        GameController = FindObjectOfType<GameController>();
    }

    private void Update()
    {
        if (!SelectedPrefab) return;

        var data = SelectedPrefab.GetComponent<Furniture>().Data;

        Vector3 position;

        var mousePosition = GetMousePosition(data.Where);

        position = data.GetPosition(mousePosition, Size, transform.forward);

        SelectedPrefab.transform.position = position;

        if (Input.GetMouseButtonDown(0)) Place();
        if (Input.GetMouseButtonDown(1)) Cancel();
        if (Input.GetKeyDown(KeyCode.R)) Rotate();
    }

    void Cancel()
    {
        Destroy(SelectedPrefab);

        GameController.Add(SelectedPrefab);

        SelectedPrefab = null;
    }

    void Place()
    {
        var furniture = SelectedPrefab.GetComponent<Furniture>();

        if (!furniture.CanBePlaced) return;

        if (furniture.Data.ShouldPay)
        {
            GameController.SpendMoney(furniture.Data.Cost);
        }

        GameController.Use(SelectedPrefab);

        SelectedPrefab = null;
    }

    void Rotate()
    {
        SelectedPrefab.transform.Rotate(0f, 90f, 0f);
    }

    public void StopDrag()
    {
        if (!SelectedPrefab) return;

        Place();
    }

    public void Drag(GameObject obj)
    {
        if (SelectedPrefab) return;

        SelectedPrefab = obj;
    }

    public void Select(GameObject prefab)
    {
        if (SelectedPrefab != null) return;

        SelectedPrefab = Instantiate(prefab, prefab.transform.position, prefab.transform.rotation);
    }

    Vector3 GetMousePosition(LayerMask layer)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layer))
        {
            if (layer == DefaultLayer)
            {
                return new Vector3(hit.point.x, hit.point.y, hit.point.z);
            }

            if (layer == GroundLayer)
            {
                return new Vector3(hit.point.x, hit.point.y, hit.point.z);
            }
            else
            {
                return new Vector3(hit.point.x - (SelectedPrefab.transform.localScale.x / 2f), hit.point.y, hit.point.z - (SelectedPrefab.transform.localScale.z / 2f));
            }
        }

        return Vector3.zero;
    }
}
