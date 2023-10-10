using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class WaitNode : BTNode {
  private float duration;

  public WaitNode(float duration) {
    this.duration = duration;
  }

  public override async UniTask<bool> Execute() {
    Debug.Log("Running wait node");
    await UniTask.Delay(TimeSpan.FromSeconds(duration));
    return true;
  }
}