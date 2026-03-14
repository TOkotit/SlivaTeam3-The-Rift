using Entity.Enemy;
using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "IsVisibleByGroup", story: "Target is visible from group by [targetdetector]", category: "Conditions", id: "41047dd4bcb39e19d8b70d90d3dd6920")]
public partial class IsVisibleByGroupCondition : Condition
{
    [SerializeReference] public BlackboardVariable<TargetDetector> Targetdetector;

    public override bool IsTrue()
    {
        return Targetdetector.Value.IsTargetVisibleByGroup;
    }

    public override void OnStart()
    {
    }

    public override void OnEnd()
    {
    }
}
