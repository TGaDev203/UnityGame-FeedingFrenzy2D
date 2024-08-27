using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    //! Components
    Rigidbody2D rigidBody;
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
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
                lastMousePosition = Input.mousePosition;

    }

    void FlipSprite()
    {
        Vector3 currentMousePosition = Input.mousePosition;
        float horizontalMovement = currentMousePosition.x - lastMousePosition.x;

        if (horizontalMovement < 0)
        {
            // Moving left
            spriteRenderer.flipX = false;
        }
        else if (horizontalMovement > 0)
        {
            // Moving right
            spriteRenderer.flipX = true;
        }

        // Update last mouse position
        lastMousePosition = currentMousePosition;
    }
}
