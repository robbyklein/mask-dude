using UnityEngine;

public class Shoots : MonoBehaviour {
    [SerializeField] private ObjectPool objectPool;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float projectileSpeed = 25f;

    public void Shoot() {
        GameObject projectile = objectPool.GetObject();
        if (projectile == null) return;

        projectile.transform.position = firePoint.position;
        projectile.transform.rotation = firePoint.rotation;

        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

        Vector3 shootingDirection = firePoint.right;

        if (transform.localScale.x < 0) {
            shootingDirection = -shootingDirection;
        }

        rb.velocity = shootingDirection * projectileSpeed;
    }
}
