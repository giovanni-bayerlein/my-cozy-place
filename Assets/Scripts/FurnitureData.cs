using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "SO/Furniture")]
public class FurnitureData : ScriptableObject
{
    public string Name;
    public string Description;
    public LayerMask Where;
    public Sprite Icon;

    public int Cost;
    public bool ShouldPay;

    private void Awake()
    {
        ShouldPay = false;
    }

    private void OnDisable()
    {
        ShouldPay = false;
    }


    public Vector3 GetPosition(Vector3 mousePosition, float gridSize, Vector3 orientation)
    {
        if (Where == 256) // ground
            return new Vector3(Mathf.RoundToInt(mousePosition.x / gridSize) * gridSize, mousePosition.y, Mathf.RoundToInt(mousePosition.z / gridSize) * gridSize);
        else // objects
            return new Vector3(Mathf.RoundToInt(mousePosition.x / gridSize) * gridSize, Mathf.RoundToInt(mousePosition.y / gridSize) * gridSize, Mathf.RoundToInt(mousePosition.z / gridSize) * gridSize);
    }
}
