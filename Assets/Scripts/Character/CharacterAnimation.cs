using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    //! Components
    private Animator characterAnimation;
    private Vector3 lastMousePosition;
    private SpriteRenderer spriteRenderer;
    private bool wasFlipped;

    // Define a small threshold to prevent continuous flipping
    [SerializeField] private float flipThreshold;

    private void Awake()
    {
        InitializeComponents();
        lastMousePosition = Input.mousePosition;
        wasFlipped = spriteRenderer.flipX;
    }

    private void InitializeComponents()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        characterAnimation = GetComponent<Animator>();
    }

    private void Update()
    {
        FlipSprite();
        HandleFlipAnimation();
    }

    private void FlipSprite()
    {
        float movement = GetMouseHorizontalMovement();

        if (Mathf.Abs(movement) > flipThreshold)
        {
            if (movement < 0)
            {
                // Moving left
                spriteRenderer.flipX = false;
            }
            else if (movement > 0)
            {
                // Moving right
                spriteRenderer.flipX = true;
            }
        }

        characterAnimation.SetBool("isSwimming", true);
    }

    private float GetMouseHorizontalMovement()
    {
        Vector3 currentMousePosition = Input.mousePosition;
        float horizontalMovement = currentMousePosition.x - lastMousePosition.x;
        // Update last mouse position
        lastMousePosition = currentMousePosition;
        return horizontalMovement;
    }

    private void HandleFlipAnimation()
    {
        bool isCurrentlyFlipped = spriteRenderer.flipX;

        if (isCurrentlyFlipped != wasFlipped)
        {
            characterAnimation.SetTrigger("turning");
            wasFlipped = isCurrentlyFlipped;
        }
    }
}
