using UnityEngine;

public class Shoots : MonoBehaviour
{
    [SerializeField] private ObjectPool objectPool;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float projectileSpeed = 10f;

    public void Shoot()
    {
        GameObject projectile = objectPool.GetObject();
        if (projectile == null) return;

        projectile.transform.position = firePoint.position;
        projectile.transform.rotation = firePoint.rotation;

        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.velocity = firePoint.right * projectileSpeed;
    }
}
