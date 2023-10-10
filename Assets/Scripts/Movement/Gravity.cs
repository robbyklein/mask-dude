using UnityEngine;

public class Gravity : MonoBehaviour {
  private Rigidbody2D subject;
  [SerializeField] float gravity = 9.8f;

  bool hasGravity = true;

  private void Start() {
    subject = GetComponent<Rigidbody2D>();
  }

  private void FixedUpdate() {
    if (hasGravity) ApplyGravity();
  }

  private void ApplyGravity() {
    subject.velocity += Vector2.down * gravity * Time.fixedDeltaTime;
  }

  public void SetHasGravity(bool newHasGravity) {
    hasGravity = newHasGravity;
  }
}