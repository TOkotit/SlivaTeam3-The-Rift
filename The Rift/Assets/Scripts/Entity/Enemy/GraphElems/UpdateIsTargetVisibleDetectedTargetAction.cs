using System;
using Entity.Enemy;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Update IsTargetVisible DetectedTarget", story: "Update [IsTargetVisible] [DetectedTarget] by [TargetDetector]", category: "Action", id: "aae411da48ea160c0798c46036febdc9")]
public partial class UpdateIsTargetVisibleDetectedTargetAction : Action
{
    [SerializeReference] public BlackboardVariable<bool> IsTargetVisible;
    [SerializeReference] public BlackboardVariable<Transform> DetectedTarget;
    [SerializeReference] public BlackboardVariable<TargetDetector> TargetDetector;
    protected override Status OnStart()
    {
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        IsTargetVisible.Value = TargetDetector.Value.IsTargetVisible;
        
        if (IsTargetVisible.Value) {
            DetectedTarget.Value = TargetDetector.Value.DetectedTarget;
        }
        
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

