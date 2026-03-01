using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Enemy Attack", story: "Enemy attacks with [EnemyAttackController]", category: "Action", id: "629c9a2ab6cab8c4d9b7a60484765308")]
public partial class EnemyAttackAction : Action
{
    [SerializeReference] public BlackboardVariable<EnemyAttackController> EnemyAttackController;

    protected override Status OnStart()
    {
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        EnemyAttackController.Value.Attack();
        
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

