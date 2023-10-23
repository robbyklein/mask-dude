using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

public class TestAI : BaseAI {
  #region Dependencies
  private DashAtTarget dash;
  private ShootsForDuration shootsForDuration;
  private ChaseTarget chaseTarget;
  private Jumps jumps;
  #endregion

  #region State
  private BaseNode root;
  private bool active = false;
  private UniTaskCompletionSource activationSignal = new UniTaskCompletionSource();
  #endregion

  private void Start() {
    dash = GetComponent<DashAtTarget>();
    shootsForDuration = GetComponent<ShootsForDuration>();
    chaseTarget = GetComponent<ChaseTarget>();
    jumps = GetComponent<Jumps>();

    ConstructBehaviorTree();
    _ = BehaviorTreeLoop();
  }

  public override bool Active {
    get => active;
    set {
      active = value;
      if (active) {
        activationSignal.TrySetResult();
        activationSignal = new UniTaskCompletionSource();
      }
    }
  }

  private async UniTaskVoid BehaviorTreeLoop() {
    while (true) {
      if (Active) {
        await root.Execute();
      } else {
        await activationSignal.Task;
      }
    }
  }

  private void ConstructBehaviorTree() {
    root = new SequenceNode(new List<BaseNode>
    {
        new SequenceNode(new List<BaseNode>
        {
            new ChanceNode(0.5f),
            new ActionNode(() => chaseTarget.SetChase(false)),
            new WaitNode(0.5f),
            new AsyncActionNode(() => dash.DashToTarget()),
            new WaitNode(0.5f),
            new ActionNode(() => chaseTarget.SetChase(true))
        }),
        new SequenceNode(new List<BaseNode>
        {
            new WaitNode(2f),
            new SelectorNode(new List<BaseNode>
            {
                new SequenceNode(new List<BaseNode>
                {
                    new ChanceNode(0.5f),
                    new ActionNode(() => shootsForDuration.ShootForDuration(0.5f))
                }),
                new SequenceNode(new List<BaseNode>
                {
                    new ActionNode(() => jumps.Jump()),
                    new WaitNode(0.5f),
                    new ActionNode(() => shootsForDuration.ShootForDuration(0.5f))
                })
            })
        }),
    });
  }
}
