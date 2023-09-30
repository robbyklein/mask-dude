using System.Collections;
using UnityEngine;

public class ShootsAtPace : MonoBehaviour {
    [SerializeField] private Shoots shoots;
    [SerializeField] private float shootInterval = 0.2f;
    [SerializeField] private float pauseDuration = 2f;

    private void Start() {
        StartCoroutine(ShootRoutine());
    }

    private IEnumerator ShootRoutine() {
        while (true) {
            // Shoot 3 times
            for (int i = 0; i < 3; i++) {
                shoots.Shoot();
                yield return new WaitForSeconds(shootInterval);
            }
            // Pause
            yield return new WaitForSeconds(pauseDuration);
        }
    }
}