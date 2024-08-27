using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

        // if (Input.GetKeyDown(KeyCode.Escape))
        // {
        //     EndGame();
        // }
    }

    void HandleMovementByMouse()
    {
        // Get mouse cursor position on the screen
        Vector3 mousePosition = Input.mousePosition;
        // Convert mouse cursor position from screen coordinates to world coordinates
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        // Retain the z axis of the capsule object
        mousePosition.z = transform.position.z;

        // Calculate screen bounds in world units
        Camera camera = Camera.main;
        float screenWidth = camera.orthographicSize * camera.aspect;
        float screenHeight = camera.orthographicSize;

        float xMin = -screenWidth + leftPadding;
        float xMax = screenWidth - rightPadding;
        float yMin = -screenHeight + upperPadding;
        float yMax = screenHeight - lowerPadding;

        mousePosition.x = Mathf.Clamp(mousePosition.x, xMin, xMax);
        mousePosition.y = Mathf.Clamp(mousePosition.y, yMin, yMax);

        // Move the capsule object to the mouse cursor position
        transform.position = mousePosition;
    }

    void StartGame()
    {
        isPlaying = true;
        // Hide mouse cursor
        Cursor.visible = false;
        // Limit mouse cursor in game window
        Cursor.lockState = CursorLockMode.Confined;
    }

    // void EndGame()
    // {
    //     isPlaying = false;
    //     Cursor.visible = true;
    //     Cursor.lockState = CursorLockMode.None;
    //     UnityEditor.EditorApplication.isPlaying = false;
    // }
}
