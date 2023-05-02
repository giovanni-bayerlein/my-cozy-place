using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float MovementSpeed, RotationSpeed, ZoomMultiplier;

    Camera cam;

    private void Start()
    {
        cam = transform.GetChild(0).GetComponent<Camera>();
    }

    private void Update()
    {
        Control();
    }

    void Control()
    {
        var movement = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));

        transform.Translate(movement * MovementSpeed * Time.deltaTime);

        if (Input.mouseScrollDelta.y != 0f)
        {
            cam.orthographicSize += Input.mouseScrollDelta.y * ZoomMultiplier;

            cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, 10f, 25f);
        }
    }
}
