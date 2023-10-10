using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

public class SelectorNode : BTNode {
  private List<BTNode> children;

  public SelectorNode(List<BTNode> children) {
    this.children = children;
  }

  public override async UniTask<bool> Execute() {
    Debug.Log("Running selector node");
    foreach (var child in children) {
      if (await child.Execute()) return true;
    }
    return false;
  }
}
