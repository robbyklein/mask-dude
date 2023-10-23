using UnityEngine;

public class BattleManager : MonoBehaviour {
  [SerializeField] private InputHandlerSO input;
  [SerializeField] private BaseAI enemyAI;

  private void Start() {
    // enemyAI.Active = true;
  }
}