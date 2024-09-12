using System;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    //! Components
    [SerializeField] private float flipThreshold;
    [SerializeField] private LayerMask _layerEatable;

    private Animator characterAnimation;
    private Vector3 lastMousePosition;
    private SpriteRenderer spriteRenderer;
    private bool wasFlipped;
    private Rigidbody2D characterRigidBody;

    private void Awake()
    {
        InitializeComponents();
    }

    //! Initialization
    private void InitializeComponents()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        characterAnimation = GetComponent<Animator>();
        characterRigidBody = GetComponent<Rigidbody2D>();
        wasFlipped = spriteRenderer.flipX;
        lastMousePosition = Input.mousePosition;
    }

    private void Update()
    {
        HandleFlipSprite();
        HandleFlipAnimation();
    }

    private void HandleFlipSprite()
    {
        float movement = GetMouseHorizontalMovement();

        if (Mathf.Abs(movement) > flipThreshold)
        {
            (movement < 0 ? (Action) StopFlipSprite : FlipSprite)();
        }

        characterAnimation.SetBool("isIdling", true);
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        PreyAnimation preyAnimation = other.GetComponent<PreyAnimation>();

        if (characterRigidBody.IsTouchingLayers(_layerEatable))
        {
            characterAnimation.SetTrigger("eating");
            preyAnimation.DestroyPrey();
        }
    }

    public void FlipSprite()
    {
        spriteRenderer.flipX = true;
    }

    public void StopFlipSprite()
    {
        spriteRenderer.flipX = false;
    }
}
