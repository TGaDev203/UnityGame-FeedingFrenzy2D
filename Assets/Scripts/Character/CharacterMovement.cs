using UnityEngine;

public class Characters : MonoBehaviour
{
    //! Components
    private bool isPlaying = false;
    [Header("Padding")]
    [SerializeField] private float leftPadding;
    [SerializeField] private float rightPadding;
    [SerializeField] private float upperPadding;
    [SerializeField] private float lowerPadding;
    [Header("Smooth Time")]
    [SerializeField] private float smoothTime = 0.1f;

    private Vector3 targetPosition;
    private Vector3 velocity = Vector3.zero;

    private void Update()
    {
        if (isPlaying)
        {
            HandleMovementByMouse();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartGame();
        }
    }

    private void HandleMovementByMouse()
    {
        // Get the mouse position in world space
        Vector3 mousePosition = Input.mousePosition;
        Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // Retain the z axis of the capsule object to avoid change height (z)
        worldMousePosition.z = transform.position.z;

        // Apply mouse sensitivity
        Vector3 adjustedMousePosition = worldMousePosition;

        // Calculate screen bounds in world units
        Camera camera = Camera.main;
        float screenWidth = camera.orthographicSize * camera.aspect;
        float screenHeight = camera.orthographicSize;

        float xMin = -screenWidth + leftPadding;
        float xMax = screenWidth - rightPadding;
        float yMin = -screenHeight + lowerPadding;
        float yMax = screenHeight - upperPadding;

        // Clamp the position within the bounds
        adjustedMousePosition.x = Mathf.Clamp(adjustedMousePosition.x, xMin, xMax);
        adjustedMousePosition.y = Mathf.Clamp(adjustedMousePosition.y, yMin, yMax);

        // Smoothly move the capsule object towards the target position
        targetPosition = adjustedMousePosition;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }

    private void StartGame()
    {
        isPlaying = true;
        // Hide mouse cursor
        Cursor.visible = false;
        // Limit mouse cursor in game window
        Cursor.lockState = CursorLockMode.Confined;
    }
}