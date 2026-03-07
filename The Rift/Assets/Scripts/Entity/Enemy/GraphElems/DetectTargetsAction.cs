using Entity.Enemy;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Detect Targets", story: "Detects targets by [TargetDetector]", category: "Action", id: "6ff6e010643506077d39cc40f7829805")]
public partial class DetectTargetsAction : Action
{
    [SerializeReference] public BlackboardVariable<TargetDetector> TargetDetector;
    protected override Status OnStart()
    {

        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        
        
        TargetDetector.Value.DetectPlayer();

        
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

