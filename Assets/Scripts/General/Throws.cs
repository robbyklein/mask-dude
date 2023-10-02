using UnityEngine;

public class Throws : MonoBehaviour {
  [SerializeField] private Rigidbody2D playerRigidbody;
  [SerializeField] private float throwForceUpward = 10f;
  [SerializeField] private float throwForceSideways = 5f;

  public void ThrowPlayer() {
    Vector2 throwDirection = new Vector2(-Mathf.Sign(playerRigidbody.velocity.x) * throwForceSideways, throwForceUpward);
    playerRigidbody.velocity = throwDirection;
  }
}