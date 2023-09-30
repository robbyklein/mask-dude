using UnityEngine;

public class Shoots : MonoBehaviour {
    [SerializeField] private ObjectPool objectPool;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float projectileSpeed = 10f;

    public void Shoot() {
        GameObject projectile = objectPool.GetObject();
        if (projectile == null) return;

        projectile.transform.position = firePoint.position;
        projectile.transform.rotation = firePoint.rotation;

        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

        Vector3 shootingDirection = firePoint.right;
        if (transform.localScale.x < 0) // assuming the player is flipped by changing the x component of localScale
        {
            shootingDirection = -shootingDirection;
        }
        rb.velocity = shootingDirection * projectileSpeed;
    }
}
