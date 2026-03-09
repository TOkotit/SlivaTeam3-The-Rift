using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Join attackQueue ", story: "[Enemy] joins attackQueue with [EnemyAttackController]", category: "Action", id: "91bf8a405eba0a84678367869c99250a")]
public partial class JoinAttackQueueAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Enemy;
    [SerializeReference] public BlackboardVariable<EnemyAttackController> EnemyAttackController;

    protected override Status OnStart()
    {
        EnemyAttackController.Value.AttackQueue.RequestAttack(EnemyAttackController.Value);
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

