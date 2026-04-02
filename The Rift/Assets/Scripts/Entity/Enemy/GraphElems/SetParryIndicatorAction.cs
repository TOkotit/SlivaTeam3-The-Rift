using Entity.Enemy;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "SetParryIndicator", story: "Set parryIndicator of [enemy] to [value]", category: "Action", id: "eb37d644f3f4f2300be5002f4b5faaeb")]
public partial class SetParryIndicatorAction : Action
{
    [SerializeReference] public BlackboardVariable<Enemy> Enemy;
    [SerializeReference] public BlackboardVariable<bool> Value;

    protected override Status OnStart()
    {
        Enemy.Value.ParryIndicator.SetActive(Value.Value);
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

