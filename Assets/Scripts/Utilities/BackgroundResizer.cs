using UnityEngine;

public class BackgroundResizer : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    void Start()
    {
        ResizeBackground();
    }

    void ResizeBackground()
    {
        // Get the size of the sprite
        Vector2 spriteSize = spriteRenderer.sprite.bounds.size;
        
        // Get the size of the camera (viewport)
        Camera camera = Camera.main;
        float screenWidth = camera.orthographicSize * camera.aspect * 2;
        float screenHeight = camera.orthographicSize * 2;

        // Calculate the ratio to maintain the aspect ratio of the image
        float widthRatio = screenWidth / spriteSize.x;
        float heightRatio = screenHeight / spriteSize.y;
        
        // Use the smallest ratio to maintain the aspect ratio of the image
        float scale = Mathf.Max(widthRatio, heightRatio);
        
        // Update the size of the sprite
        transform.localScale = new Vector3(scale, scale, 1);
    }
}
