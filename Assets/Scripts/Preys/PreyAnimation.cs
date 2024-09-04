using System.Runtime.CompilerServices;
using UnityEngine;

public class PreyAnimation : MonoBehaviour
{
    //! Components
    [SerializeField] private float destroyDelay;
    private Rigidbody2D preyRigidBody;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        InitializeComponents();
    }

    //! Initialize Components
    private void InitializeComponents()
    {
        preyRigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void DestroyPrey()
    {
        Destroy(gameObject, destroyDelay);
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
