using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class TestAI : MonoBehaviour {
  private BaseNode root;
  private DashAtTarget dash;
  private ShootsForDuration shootsForDuration;
  private ChaseTarget chaseTarget;
  private Jumps jumps;

  private void Start() {
    dash = GetComponent<DashAtTarget>();
    shootsForDuration = GetComponent<ShootsForDuration>();
    chaseTarget = GetComponent<ChaseTarget>();
    jumps = GetComponent<Jumps>();

    ConstructBehaviorTree();
    _ = BehaviorTreeLoop();
  }

  private async UniTaskVoid BehaviorTreeLoop() {
    while (true) {
      await root.Execute();
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
