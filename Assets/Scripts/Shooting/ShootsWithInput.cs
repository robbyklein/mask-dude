using UnityEngine;

public class ShootsWithInput : MonoBehaviour
{
    [SerializeField] private InputHandlerSO input;
    [SerializeField] private Shoots shoots;
    [SerializeField] private float fireRate = 0.2f; // Time between shots when held

    private float nextFireTime = 0f;
    bool isFiring = false;

    private void OnEnable()
    {
        input.OnGameFiringChange += Fire;
    }

    private void OnDisable()
    {
        input.OnGameFiringChange -= Fire;
    }

    private void Update()
    {
        if (Time.time >= nextFireTime && isFiring) {
            shoots.Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    private void Fire(bool firing)
    {
        isFiring = firing;

        if (firing && Time.time >= nextFireTime) {
            shoots.Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }
}