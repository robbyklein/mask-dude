using UnityEngine;

public class MovementHelpers : MonoBehaviour {
  private BoxCollider2D boxCollider;
  [SerializeField] private LayerMask groundLayer;
  [SerializeField] private float groundCheckDistance = 0.1f;

  private void Start() {
    boxCollider = GetComponent<BoxCollider2D>();
  }

  public bool IsGrounded() {
    Vector2 boxSize = new Vector2(
        Mathf.Abs(boxCollider.size.x) * Mathf.Abs(transform.localScale.x),
        boxCollider.size.y * Mathf.Abs(transform.localScale.y)
    );

    // Shift the boxCenter downwards by 0.2f
    Vector2 boxCenter = (Vector2)transform.position + boxCollider.offset - new Vector2(0, groundCheckDistance);

    RaycastHit2D hit = Physics2D.BoxCast(boxCenter, boxSize, 0f, Vector2.down, groundCheckDistance, groundLayer);

#if UNITY_EDITOR
    DrawDebugBox(boxCenter, boxSize);
#endif

    return hit.collider != null;
  }



  private void DrawDebugBox(Vector2 boxCenter, Vector2 boxSize) {
    float halfWidth = boxSize.x / 2;
    float halfHeight = boxSize.y / 2;
    Vector2 topLeft = boxCenter + new Vector2(-halfWidth, halfHeight);
    Vector2 topRight = boxCenter + new Vector2(halfWidth, halfHeight);
    Vector2 bottomLeft = boxCenter + new Vector2(-halfWidth, -halfHeight);
    Vector2 bottomRight = boxCenter + new Vector2(halfWidth, -halfHeight);

    float duration = 1f;
    Color color = Color.red;

    Debug.DrawLine(topLeft, topRight, color, duration);
    Debug.DrawLine(topRight, bottomRight, color, duration);
    Debug.DrawLine(bottomRight, bottomLeft, color, duration);
    Debug.DrawLine(bottomLeft, topLeft, color, duration);
  }

}