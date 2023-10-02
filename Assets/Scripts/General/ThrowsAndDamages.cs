using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class ThrowsAndDamages : MonoBehaviour {
  [SerializeField] private LayerMask throwSubjectLayer;
  [SerializeField] private Health throwSubjectHealth;
  [SerializeField] private CharacterStatsSO stats;
  private Throws thrower;
  private Blinks blinker;
  [SerializeField] private float cooldown = 2f;
  private bool ready = true;

  private void Start() {
    thrower = GetComponent<Throws>();
    blinker = GetComponent<Blinks>();
  }

  private void OnTriggerStay2D(Collider2D other) {
    if (((1 << other.gameObject.layer) & throwSubjectLayer) != 0) {
      Debug.Log("Something entered the enemy area");

      DamageAndThrowPlayer().Forget();
    }
  }

  private async UniTaskVoid DamageAndThrowPlayer() {
    if (ready) {
      throwSubjectHealth.TakeDamage(stats.PlayerTouchDamage);
      thrower.ThrowPlayer();
      blinker.StartBlinking(cooldown);

      ready = false;
      await UniTask.Delay(TimeSpan.FromSeconds(cooldown));
      ready = true;
    }
  }
}
