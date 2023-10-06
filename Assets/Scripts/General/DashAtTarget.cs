using Cysharp.Threading.Tasks;
using UnityEngine;

public class DashAtTarget : MonoBehaviour {
  private Rigidbody2D rb;
  [SerializeField] private Transform targetTransform;
  [SerializeField] private float dashSpeed = 10f;

  private bool isDashing;

  private void Start() {
    rb = GetComponent<Rigidbody2D>();
  }

  public async UniTaskVoid DashToTarget() {
    if (!isDashing && targetTransform != null) {
      await Dash();
    }
  }

  private async UniTask Dash() {
    isDashing = true;
    Vector3 targetPosition = targetTransform.position;

    while ((transform.position - targetPosition).sqrMagnitude > 0.1f) {
      transform.position = Vector3.MoveTowards(transform.position, targetPosition, dashSpeed * Time.deltaTime);
      await UniTask.Yield();
    }

    isDashing = false;
    rb.velocity = Vector2.zero;
  }
}
