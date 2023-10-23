using Cysharp.Threading.Tasks;
using UnityEngine;

public class DashAtTarget : MonoBehaviour {
  private Rigidbody2D rb;
  private Gravity gravity;

  [SerializeField] private Transform targetTransform;
  [SerializeField] private float dashSpeed = 10f;

  private bool isDashing;

  private void Start() {
    rb = GetComponent<Rigidbody2D>();
    gravity = GetComponent<Gravity>();
  }

  public async UniTask DashToTarget() {
    if (!isDashing && targetTransform != null) {
      await Dash();
    }
  }

  private async UniTask Dash() {
    isDashing = true;

    // Disable gravity so no drop
    gravity.SetHasGravity(false);

    // Calculate the direction
    Vector2 dashDirection = (targetTransform.position - transform.position).normalized;
    rb.velocity = dashDirection * dashSpeed;

    // Dash until collides
    await UniTask.WaitUntil(() => !isDashing);

    // Re-enable gravity
    gravity.SetHasGravity(true);

    // Stop on a dime
    rb.velocity = Vector2.zero;
  }

  private void OnCollisionEnter2D(Collision2D collision) {
    if (!collision.collider.isTrigger) {
      // Stop dashing when colliding with a non-trigger collider
      isDashing = false;
    }
  }
}
