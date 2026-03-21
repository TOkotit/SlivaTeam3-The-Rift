using Entity.Enemy;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "SetDamageImmunity", story: "Set damage immunity of [Enemy] to [value]", category: "Action", id: "b3ace301370b65a9ecbd68ec61ee6b04")]
public partial class SetDamageImmunityAction : Action
{
    [SerializeReference] public BlackboardVariable<Enemy> Enemy;
    [SerializeReference] public BlackboardVariable<bool> Value;
    protected override Status OnStart()
    {
        Enemy.Value.Damagable.Health.DamageImmunity = Value.Value;
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

