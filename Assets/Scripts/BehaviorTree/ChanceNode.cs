using Cysharp.Threading.Tasks;
using UnityEngine;

public class ChanceNode : BaseNode {
  private float probability;

  public ChanceNode(float probability) {
    this.probability = probability;
  }

  public override UniTask<bool> Execute() {
    float randomValue = Random.value;
    Debug.Log($"Running change {randomValue}");
    return UniTask.FromResult(randomValue < probability);
  }
}