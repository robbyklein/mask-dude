using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class SequenceNode : BTNode {
  private List<BTNode> children;

  public SequenceNode(List<BTNode> children) {
    this.children = children;
  }

  public override async UniTask<bool> Execute() {
    Debug.Log("Running sequence node");

    foreach (var child in children) {
      if (!await child.Execute()) return false;
    }
    return true;
  }
}
