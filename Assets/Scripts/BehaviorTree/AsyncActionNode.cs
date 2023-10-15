using Cysharp.Threading.Tasks;

public class AsyncActionNode : BaseNode {
  private System.Func<UniTask> asyncAction;

  public AsyncActionNode(System.Func<UniTask> asyncAction) {
    this.asyncAction = asyncAction;
  }

  public override async UniTask<bool> Execute() {
    await asyncAction();
    return true;
  }
}