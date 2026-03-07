using Entity.Enemy;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Update Distance", story: "Update [DistanceToTarget] by [TargetDetector] [IsTargetVisible]", category: "Action", id: "70e25237988a89d9ce25e2ccc025d8da")]
public partial class UpdateDistanceAction : Action
{
    [SerializeReference] public BlackboardVariable<float> DistanceToTarget;
    [SerializeReference] public BlackboardVariable<TargetDetector> TargetDetector;
    [SerializeReference] public BlackboardVariable<bool> IsTargetVisible;
    protected override Status OnStart()
    {
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (IsTargetVisible.Value) {
            DistanceToTarget.Value = TargetDetector.Value.UpdateDistanceToTarget();
        }
        
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

