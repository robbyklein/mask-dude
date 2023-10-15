using UnityEngine;

public class Jumps : MonoBehaviour {
  private Rigidbody2D rb;
  private MovementHelpers movementHelpers;
  [SerializeField] private float jumpForce = 10f;

  public bool IsGrounded = true;

  private void Start() {
    rb = GetComponent<Rigidbody2D>();
    movementHelpers = GetComponent<MovementHelpers>();
  }

  private void Update() {
    IsGrounded = movementHelpers.IsGrounded();
  }

  public void Jump() {
    if (IsGrounded) {
      rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }
  }
}