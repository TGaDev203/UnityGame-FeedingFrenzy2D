using UnityEngine;

public class PreyMovement : MonoBehaviour
{
    //! Components
    [SerializeField] private float moveSpeed;
    [SerializeField] private float leftPadding;
    [SerializeField] private float rightPadding;
    [SerializeField] private float upperPadding;
    [SerializeField] private float lowerPadding;
    [SerializeField] private float smoothTime;
    [SerializeField] private LayerMask _leftBoundaryLayerMask;
    [SerializeField] private LayerMask _rightBoundaryLayerMask;
    private Rigidbody2D preyRigidBody;

    private void Awake()
    {
        InitializeComponents();
    }

    //! Initialization
    private void InitializeComponents()
    {
        preyRigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        HandleMovement();
    }


    private void HandleMovement()
    {
        // Move prey to the left continuously
        preyRigidBody.velocity = new Vector2(-moveSpeed, preyRigidBody.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PreyAnimation preyAnimation = GetComponent<PreyAnimation>();

        if (preyRigidBody.IsTouchingLayers(_leftBoundaryLayerMask))
        {
            moveSpeed = -moveSpeed;
            preyAnimation.FlipSprite();
        }

        if (preyRigidBody.IsTouchingLayers(_rightBoundaryLayerMask))
        {
            moveSpeed = -moveSpeed;
            preyAnimation.StopFlipSprite();
        }
    }
}
