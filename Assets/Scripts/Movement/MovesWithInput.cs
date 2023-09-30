using System;
using UnityEngine;

public class MovesWithInput : MonoBehaviour {
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private InputHandlerSO input;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float groundCheckDistance = 0.1f;
    [SerializeField] private float accelerationTime = 0.1f;

    private float velocityXSmooth;
    private Vector2 movement;

    private void OnEnable() {
        input.OnGameMovement += HandleMove;
        input.OnGameJump += Jump;
    }

    private void OnDisable() {
        input.OnGameMovement -= HandleMove;
        input.OnGameJump -= Jump;
    }

    private void FixedUpdate() {
        Move();
    }

    private void Jump() {
        if (IsGrounded()) {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    private void Move() {
        float targetVelocityX = movement.x * speed;
        Vector3 localScale = transform.localScale;

        if (movement.x > 0f) {
            localScale.x = Math.Abs(localScale.x); // Ensure the localScale.x is positive
        } else if (movement.x < 0f) {
            localScale.x = -Math.Abs(localScale.x); // Ensure the localScale.x is negative
        }

        transform.localScale = localScale;

        rb.velocity = new Vector2(
            Mathf.SmoothDamp(rb.velocity.x, targetVelocityX, ref velocityXSmooth, accelerationTime),
            rb.velocity.y
        );
    }

    private bool IsGrounded() {
        Vector2 boxSize = new Vector2(Mathf.Abs(boxCollider.size.x) * Mathf.Abs(transform.localScale.x), boxCollider.size.y * Mathf.Abs(transform.localScale.y));
        Vector2 boxCenter = (Vector2)transform.position + (Vector2.down * (boxSize.y / 2));
        RaycastHit2D hit = Physics2D.BoxCast(boxCenter, boxSize, 0f, Vector2.down, groundCheckDistance, groundLayer);

        return hit.collider != null;
    }

    private void HandleMove(Vector2 newMovement) {
        movement = newMovement;
    }
}
