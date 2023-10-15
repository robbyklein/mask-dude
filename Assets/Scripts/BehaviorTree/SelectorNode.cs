using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

public class SelectorNode : BaseNode {
  private List<BaseNode> children;

  public SelectorNode(List<BaseNode> children) {
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
