using System;
using Cysharp.Threading.Tasks;

public class ActionNode : BTNode {
  private Action action;

  public ActionNode(Action action) {
    this.action = action;
  }

  public override UniTask<bool> Execute() {
    UnityEngine.Debug.Log($"Calling action node {action}");
    action();
    return UniTask.FromResult(true); // Assuming synchronous actions always succeed.
  }
}
