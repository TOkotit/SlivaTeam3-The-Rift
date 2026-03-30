using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "DequeueEnemyAttack", story: "[enemy] quit queue with [enemyAttackController]", category: "Action", id: "59ad88fab777817be77b80548cc6808b")]
public partial class DequeueEnemyAttackAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Enemy;
    [SerializeReference] public BlackboardVariable<EnemyAttackController> EnemyAttackController;

    protected override Status OnStart()
    {
        EnemyAttackController.Value.AttackQueue.CancelAttack(EnemyAttackController.Value);
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

