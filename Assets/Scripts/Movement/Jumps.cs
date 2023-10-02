using UnityEngine;

public class Jumps : MonoBehaviour {
  private Rigidbody2D rb;
  private MovementHelpers movementHelpers;
  [SerializeField] private float jumpForce = 10f;

  private void Start() {
    rb = GetComponent<Rigidbody2D>();
    movementHelpers = GetComponent<MovementHelpers>();
  }

  public void Jump() {
    if (movementHelpers.IsGrounded()) {
      rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }
  }
}