using System.Collections;
using UnityEngine;

public class Blinks : MonoBehaviour {
  [SerializeField] private SpriteRenderer sprite;
  [SerializeField] private float blinkInterval = 0.1f;

  public void StartBlinking(float duration = 2f) {
    StartCoroutine(Blink(duration));
  }

  private IEnumerator Blink(float duration = 2f) {
    float endTime = Time.time + duration;
    while (Time.time < endTime) {
      sprite.enabled = !sprite.enabled;
      yield return new WaitForSeconds(blinkInterval);
    }
    sprite.enabled = true;
  }
}