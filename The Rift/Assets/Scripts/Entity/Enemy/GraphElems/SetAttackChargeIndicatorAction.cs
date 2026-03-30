using Entity.Enemy;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "SetAttackChargeIndicator", story: "Set AttackChargeIndicator of [enemy] to [value]", category: "Action", id: "cc14753ee8893ef9f6d2140d5d1e1c7b")]
public partial class SetAttackChargeIndicatorAction : Action
{
    [SerializeReference] public BlackboardVariable<Enemy> Enemy;
    [SerializeReference] public BlackboardVariable<bool> Value;

    protected override Status OnStart()
    {
        
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        Enemy.Value.AttackChargeIndicator.SetActive(Value.Value);
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

