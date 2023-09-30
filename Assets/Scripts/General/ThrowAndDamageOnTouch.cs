using System;
using System.Collections;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class ThrowAndDamageOnTouch : MonoBehaviour {
  [Header("Layers")]
  [SerializeField] private LayerMask playerLayer;

  [Header("Damage")]
  [SerializeField] private HasHealth playerHealth;
  [SerializeField] private CharacterStatsSO playerStats;
  [SerializeField] private float damageCooldown = 1f;
  private bool canDamage = true;

  [Header("Throw")]
  [SerializeField] private Rigidbody2D playerRigidbody;
  [SerializeField] private float throwForceUpward = 10f;
  [SerializeField] private float throwForceSideways = 5f;

  [Header("Blinking")]
  [SerializeField] private SpriteRenderer spriteRenderer;
  [SerializeField] private float blinkDuration = 1f;
  [SerializeField] private float blinkInterval = 0.1f;

  private void OnTriggerStay2D(Collider2D other) {
    if (((1 << other.gameObject.layer) & playerLayer) != 0) {
      TryDamageAndThrowPlayer().Forget();
    }
  }

  private async UniTaskVoid TryDamageAndThrowPlayer() {
    if (canDamage) {
      // Damage Player
      playerHealth.TakeDamage(playerStats.PlayerTouchDamage);

      // Throw Player
      Vector2 throwDirection = new Vector2(-Mathf.Sign(playerRigidbody.velocity.x) * throwForceSideways, throwForceUpward);
      playerRigidbody.velocity = throwDirection;

      // Start Blinking
      StartCoroutine(Blink());

      // Start Cooldown
      canDamage = false;
      await UniTask.Delay(TimeSpan.FromSeconds(damageCooldown));
      canDamage = true;
    }
  }

  private IEnumerator Blink() {
    float endTime = Time.time + blinkDuration;
    while (Time.time < endTime) {
      spriteRenderer.enabled = !spriteRenderer.enabled;
      yield return new WaitForSeconds(blinkInterval);
    }
    spriteRenderer.enabled = true;
  }
}
