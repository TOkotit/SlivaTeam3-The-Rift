using Entity.Enemy;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Detect Targets", story: "Detects targets by [TargetDetector] and saves to [DetectedTarget] and [DistanceToTarget]", category: "Action", id: "6ff6e010643506077d39cc40f7829805")]
public partial class DetectTargetsAction : Action
{
    [SerializeReference] public BlackboardVariable<TargetDetector> TargetDetector;
    [SerializeReference] public BlackboardVariable<Transform> DetectedTarget;
    [SerializeReference] public BlackboardVariable<float> DistanceToTarget;
    protected override Status OnStart()
    {
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        TargetDetector.Value.DetectPlayer();

        DetectedTarget.Value = TargetDetector.Value.DetectedTarget;
        DistanceToTarget.Value = TargetDetector.Value.DistanceToTarget;
        
        
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

