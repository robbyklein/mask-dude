using Cysharp.Threading.Tasks;
using UnityEngine;

public class ShootsForDuration : MonoBehaviour {
  [SerializeField] private Shoots shoots;
  [SerializeField] private float shootInterval = 0.15f;

  public async UniTaskVoid ShootForDuration(float duration = 0.3f) {
    float timeElapsed = 0;

    while (timeElapsed < duration) {
      shoots.Shoot();
      await UniTask.Delay((int)(shootInterval * 1000));
      timeElapsed += shootInterval;
    }
  }
}
