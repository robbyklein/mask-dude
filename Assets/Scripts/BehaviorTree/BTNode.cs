using Cysharp.Threading.Tasks;

public abstract class BTNode {
  public abstract UniTask<bool> Execute();
}