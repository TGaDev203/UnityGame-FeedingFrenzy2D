using UnityEngine;

public class Characters : MonoBehaviour
{
    private bool isPlaying = false;

    [Header("Padding")]
    [SerializeField] private float leftPadding = 0.5f;
    [SerializeField] private float rightPadding = 0.5f;
    [SerializeField] private float upperPadding;
    [SerializeField] private float lowerPadding;
    [SerializeField] private float smoothTime;
    [SerializeField] private float mouseSensitivity;
    
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

        // Retain the z axis of the character to avoid changing height (z)
        worldMousePosition.z = transform.position.z;

        // Apply mouse sensitivity
        Vector3 adjustedMousePosition = (worldMousePosition - transform.position) * mouseSensitivity + transform.position;

        // Calculate screen bounds in world units using viewport space
        Camera camera = Camera.main;
        
        // Convert viewport bounds to world space
        Vector3 lowerLeft = camera.ViewportToWorldPoint(new Vector3(0, 0, camera.nearClipPlane));
        Vector3 upperRight = camera.ViewportToWorldPoint(new Vector3(1, 1, camera.nearClipPlane));

        // Apply padding
        float xMin = lowerLeft.x + leftPadding;
        float xMax = upperRight.x - rightPadding;
        float yMin = lowerLeft.y + lowerPadding;
        float yMax = upperRight.y - upperPadding;

        // Clamp the position within the bounds
        adjustedMousePosition.x = Mathf.Clamp(adjustedMousePosition.x, xMin, xMax);
        adjustedMousePosition.y = Mathf.Clamp(adjustedMousePosition.y, yMin, yMax);

        // Smoothly move the character towards the target position
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
