using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Enemy gets ready", story: "Enemy starts charging an attack with [EnemyAttackController]", category: "Action", id: "80c122425480169fff375a88f4dad404")]
public partial class EnemyGetsReadyAction : Action
{
    [SerializeReference] public BlackboardVariable<EnemyAttackController> EnemyAttackController;

    protected override Status OnStart()
    {
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

