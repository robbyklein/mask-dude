using UnityEngine;

public class Moves : MonoBehaviour {
  private Rigidbody2D rb;
  [SerializeField] private float speed = 5f;
  [SerializeField] private float accelerationTime = 0.1f;

  private float velocityXSmooth;

  private void Start() {
    rb = GetComponent<Rigidbody2D>();
  }

  public void Move(float movement) {
    float targetVelocityX = movement * speed;
    Vector3 localScale = transform.localScale;

    if (movement > 0f) {
      localScale.x = Mathf.Abs(localScale.x);
    } else if (movement < 0f) {
      localScale.x = -Mathf.Abs(localScale.x);
    }

    transform.localScale = localScale;

    rb.velocity = new Vector2(
        Mathf.SmoothDamp(rb.velocity.x, targetVelocityX, ref velocityXSmooth, accelerationTime),
        rb.velocity.y
    );
  }
}