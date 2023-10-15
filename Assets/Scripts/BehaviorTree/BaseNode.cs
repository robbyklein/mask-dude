using Cysharp.Threading.Tasks;

public abstract class BaseNode {
  public abstract UniTask<bool> Execute();
}