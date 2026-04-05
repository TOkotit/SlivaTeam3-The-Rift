using Entity.Enemy;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "StopDash", story: "Stop dash with [EnemyMovementController]", category: "Action", id: "af951572542c9b011a8c2146ec512dcd")]
public partial class StopDashAction : Action
{
    [SerializeReference] public BlackboardVariable<EnemyMovementController> EnemyMovementController;

    protected override Status OnStart()
    {
        EnemyMovementController.Value.StopDashing();
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

