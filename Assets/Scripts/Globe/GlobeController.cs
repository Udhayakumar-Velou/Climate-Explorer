using UnityEngine;

public class GlobeController : MonoBehaviour
{
    public float rotationSpeed = 0.3f;

    private Vector3 lastMousePosition;

    void Update()
    {
        HandleRotation();
    }

    void HandleRotation()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lastMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 delta = Input.mousePosition - lastMousePosition;

            transform.Rotate(
                Vector3.up,
                -delta.x * rotationSpeed,
                Space.World
            );

            transform.Rotate(
                Vector3.right,
                delta.y * rotationSpeed,
                Space.World
            );

            lastMousePosition = Input.mousePosition;
        }
    }
}