using System;
using Unity.Behavior;
using UnityEngine;
using UnityEngine.AI;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Stop navigation of NavMeshAgent", story: "Stop navigation of [NavMeshAgent]", category: "Action", id: "7ba7d1ee96b8c990b02c4c986d0504c7")]
public partial class StopNavigationOfNavMeshAgentAction : Action
{
    [SerializeReference] public BlackboardVariable<NavMeshAgent> NavMeshAgent;

    protected override Status OnStart()
    {
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        NavMeshAgent.Value.isStopped = true;
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

