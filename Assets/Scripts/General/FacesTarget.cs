using UnityEngine;

public class FacesTarget : MonoBehaviour {
  [SerializeField] private Transform targetTransform;

  private void Update() {
    FaceTarget();
  }

  private void FaceTarget() {
    if (targetTransform == null) return;

    Vector3 localScale = transform.localScale;

    if (targetTransform.position.x > transform.position.x) {
      localScale.x = Mathf.Abs(localScale.x);
    } else if (targetTransform.position.x < transform.position.x) {
      localScale.x = -Mathf.Abs(localScale.x);
    }

    transform.localScale = localScale;
  }
}
