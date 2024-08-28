using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    //! Components
    Animator characterAnimation;
    Vector3 lastMousePosition;

    SpriteRenderer spriteRenderer;

    void Awake()
    {
        InitializeComponents();
    }

    void Update()
    {
        FlipSprite();
    }

    void InitializeComponents()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        characterAnimation = GetComponent<Animator>();

    }

    void FlipSprite()
    {
        float movement = GetMouseHorizontalMovement();

        if (movement < 0)
        {
            // Moving left
            spriteRenderer.flipX = false;
            characterAnimation.SetBool("isSwimming", true);
        }
        else if (movement > 0)
        {
            // Moving right
            spriteRenderer.flipX = true;
            characterAnimation.SetBool("isSwimming", true);
        }
    }

    float GetMouseHorizontalMovement()
    {
        Vector3 currentMousePosition = Input.mousePosition;
        float horizontalMovement = currentMousePosition.x - lastMousePosition.x;
        // Update last mouse position
        lastMousePosition = currentMousePosition;
        return horizontalMovement;
    }
}
