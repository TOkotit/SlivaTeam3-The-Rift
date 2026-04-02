using Enums;
using System;
using Entity.Enemy;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using UnityEngine.AI;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Dash", story: "[Gameobject] dashes [direction] for [value] for duration [dur] with [EnemyMovementController]", category: "Action", id: "e6932b791ffd3ded7964685a356e3fb7")]
public partial class DashAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Gameobject;
    [SerializeReference] public BlackboardVariable<Direction> Direction;
    [SerializeReference] public BlackboardVariable<float> Value;
    [SerializeReference] public BlackboardVariable<float> Dur;
    [SerializeReference] public BlackboardVariable<EnemyMovementController> EnemyMovementController;
    protected override Status OnStart()
    {
        EnemyMovementController.Value.Dash(Direction.Value, Value.Value, Dur.Value);
        
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (!EnemyMovementController.Value.IsDashing)
        {
            return Status.Success;
        }
        
        return Status.Running;
    }

    protected override void OnEnd()
    {
    }
}

