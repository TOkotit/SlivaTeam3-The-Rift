using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "FinishAttack", story: "Finish attack with [enemyAttackController]", category: "Action", id: "2da2303fe7e66e35d6c148ba553cd988")]
public partial class FinishAttackAction : Action
{
    [SerializeReference] public BlackboardVariable<EnemyAttackController> EnemyAttackController;

    protected override Status OnStart()
    {
        EnemyAttackController.Value.AttackQueue.FinishAttack();
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

