using UnityEngine;

public class Gravtity : MonoBehaviour {
  private Rigidbody2D subject;
  [SerializeField] float gravity = 9.8f;

  private void Start() {
    subject = GetComponent<Rigidbody2D>();
  }

  private void FixedUpdate() {
    ApplyGravity();
  }

  private void ApplyGravity() {
    subject.velocity += Vector2.down * gravity * Time.fixedDeltaTime;
  }
}