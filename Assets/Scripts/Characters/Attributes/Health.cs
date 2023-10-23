using UnityEngine;

public class Health : MonoBehaviour {
  [SerializeField] private CharacterStatsSO stats;
  [SerializeField] private LayerMask projectileLayer;
  [SerializeField] private HealthBarDisplay display;

  private float health = 100;

  private void Awake() {
    health = stats.Health;
  }

  private void OnTriggerEnter2D(Collider2D collision) {
    if ((projectileLayer.value & (1 << collision.gameObject.layer)) != 0) {
      TakeDamage(stats.projectileDamage);
    }
  }

  public void TakeDamage(float damage) {
    health -= damage;

    if (display != null) {
      display.UpdateFilled(health / stats.Health * 100);
    }

    if (health <= 0) Die();
  }

  private void Die() {
    Destroy(gameObject);
  }
}