using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolRotate : MonoBehaviour
{
    private Camera mainCamera;
    public float offset = 0;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        RotatePlayerTowardsMouse();
    }

    void RotatePlayerTowardsMouse()
    {
        // Get the mouse position in world coordinates
        Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - (Vector2)transform.position).normalized;

        // Calculate the angle in degrees
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + offset;

        // Rotate the pickaxe
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
