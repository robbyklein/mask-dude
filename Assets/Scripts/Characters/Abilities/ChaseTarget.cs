using UnityEngine;

public class ChaseTarget : MonoBehaviour {
  [SerializeField] private Transform targetTransform;
  [SerializeField] private Moves moves;
  [SerializeField] private float directionChangeSmoothness = 0.05f;
  [SerializeField] private float chaseThreshold = 1f;

  private bool shouldChase = false;
  private float currentDirection = 0;

  private void Update() {
    if (shouldChase && targetTransform != null) {
      float targetDirection = Mathf.Sign(targetTransform.position.x - transform.position.x);

      if (Mathf.Abs(targetTransform.position.x - transform.position.x) > chaseThreshold) {
        currentDirection = Mathf.Lerp(currentDirection, targetDirection, directionChangeSmoothness);
        moves.Move(currentDirection);
      }
    }
  }

  public void SetChase(bool newShouldChase) {
    shouldChase = newShouldChase;
  }
}
