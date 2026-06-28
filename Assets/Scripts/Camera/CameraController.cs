using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float zoomSpeed = 8f;
    public float minZoom = 1f;
    public float maxZoom = 12f;
    public float panSpeed = 0.01f;
    private Camera cam;
    private Vector3 lastMousePosition;
    public float rotationSpeed = 0.15f;
    private float currentRotationY = 0f;
    private float targetRotationY = 0f;
    private Vector3 homePosition;
    private Quaternion homeRotation;

    private void Start()
    {
        cam = Camera.main;
        homePosition = new Vector3(
            1.08f,
            18.26f,
            -1f
        );
        homeRotation = Quaternion.Euler(
            90f,
            0f,
            -0.5f
        );  
        transform.position = homePosition;
        transform.rotation = homeRotation;
        currentRotationY = 0f;
        targetRotationY = 0f;
    }

    private void Update()
    {
        HandleZoom();
        HandlePan();
        HandleRotation();
        if (Input.GetKeyDown(KeyCode.H))
        {
            GoHome();
        }
    }

    private void HandleZoom()
    {
        float scroll = Input.mouseScrollDelta.y;

        if (Mathf.Abs(scroll) < 0.01f)
            return;

        Vector3 pos = transform.position;

        pos.z -= scroll * zoomSpeed;

        pos.z = Mathf.Clamp(pos.z, -maxZoom, -minZoom);

        transform.position = pos;
    }

    private void HandlePan()
    {
        if (Input.GetMouseButtonDown(1))
        {
            lastMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(1))
        {
            Vector3 delta = Input.mousePosition - lastMousePosition;

            transform.position += new Vector3(
                -delta.x * panSpeed,
                -delta.y * panSpeed,
                0f
            );

            lastMousePosition = Input.mousePosition;
        }
    }

    private void HandleRotation()
    {
        if (Input.GetKey(KeyCode.LeftShift) &&
            Input.GetMouseButtonDown(0))
        {
            lastMousePosition = Input.mousePosition;
        }

        if (Input.GetKey(KeyCode.LeftShift) &&
            Input.GetMouseButton(0))
        {
            Vector3 delta = Input.mousePosition - lastMousePosition;

            targetRotationY += delta.x * rotationSpeed;

            targetRotationY = Mathf.Clamp(
                targetRotationY,
                -35f,
                35f);

            lastMousePosition = Input.mousePosition;
        }

        currentRotationY = Mathf.Lerp(
            currentRotationY,
            targetRotationY,
            Time.deltaTime * 8f);

        transform.rotation = Quaternion.Euler(
            90f,
            currentRotationY,
            -0.5f);
    }
    private void GoHome()
    {
        transform.position = homePosition;
        transform.rotation = homeRotation;
        currentRotationY = 0f;
        targetRotationY = 0f;
    }

}