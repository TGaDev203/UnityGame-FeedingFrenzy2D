using UnityEngine;

public class Characters : MonoBehaviour
{
    //! Components
    bool isPlaying = false;
    [Header("Padding")]
    [SerializeField] float leftPadding;
    [SerializeField] float rightPadding;
    [SerializeField] float upperPadding;
    [SerializeField] float lowerPadding;
    [Header("Smooth Time")]
    [SerializeField] float smoothTime = 0.1f;

    private Vector3 targetPosition;
    private Vector3 velocity = Vector3.zero;

    void Update()
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

    void HandleMovementByMouse()
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

    void StartGame()
    {
        isPlaying = true;
        // Hide mouse cursor
        Cursor.visible = false;
        // Limit mouse cursor in game window
        Cursor.lockState = CursorLockMode.Confined;
    }
}