using Cysharp.Threading.Tasks;
using UnityEngine;

public class TestAI : MonoBehaviour {
  private DashAtTarget dash;
  private ShootsForDuration shootsForDuration;
  private ChaseTarget chaseTarget;
  private Jumps jumps;

  private bool isDashingPeriodically = false;

  private async UniTaskVoid Start() {
    dash = GetComponent<DashAtTarget>();
    shootsForDuration = GetComponent<ShootsForDuration>();
    chaseTarget = GetComponent<ChaseTarget>();
    jumps = GetComponent<Jumps>();

    chaseTarget.SetChase(true);

    // Start main behavior loop
    _ = MainBehaviorLoop();

    // Start the periodic dash behavior
    if (!isDashingPeriodically) {
      isDashingPeriodically = true;
      _ = MaybeDashEveryFiveSeconds();
    }
  }

  private async UniTask MainBehaviorLoop() {
    while (true) {
      await UniTask.Delay(2000); // Wait for 2 seconds (or other desired interval)

      // Roll dice for decision
      int diceRoll = Random.Range(0, 2); // 0 or 1

      if (diceRoll == 0) {
        shootsForDuration.ShootForDuration(0.5f);
      } else {
        jumps.Jump();
        await UniTask.Delay(500); // Wait for half a second after jumping
        shootsForDuration.ShootForDuration(0.5f);
      }
    }
  }

  private async UniTask MaybeDashEveryFiveSeconds() {
    while (true) {
      Debug.Log("Checking dash");
      await UniTask.Delay(5000); // Wait for 5 seconds

      if (Random.Range(0f, 1f) > 0.5f) // 50% chance to dash
      {
        chaseTarget.SetChase(false); // Stop chasing
        await UniTask.Delay(500); // Wait for half a second
        dash.DashToTarget();
        await UniTask.Delay(500); // Wait for half a second post-dash
        chaseTarget.SetChase(true); // Resume chasing
      }
    }
  }
}
