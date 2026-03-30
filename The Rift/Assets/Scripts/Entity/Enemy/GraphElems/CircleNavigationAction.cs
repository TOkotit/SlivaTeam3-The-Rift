using System;
using Unity.Behavior;
using UnityEngine;
using UnityEngine.AI;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "CircleNavigation", story: "[NavAgent] navigates in orbit of [transform] with [speed] for [duration] seconds", category: "Action", id: "1e2129a95a27cc3b04cbf8c9fa50fe3c")]
public partial class CircleNavigationAction : Action
{
    [SerializeReference] public BlackboardVariable<NavMeshAgent> NavAgent;
    [SerializeReference] public BlackboardVariable<Transform> Transform;
    [SerializeReference] public BlackboardVariable<float> Speed;
    [SerializeReference] public BlackboardVariable<float> Duration;
    private float elapsed = 0f;
    
    protected override Status OnStart()
    {
        elapsed = 0f;
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (Transform.Value == null) return Status.Failure;

        elapsed += Time.fixedDeltaTime;
        
        if (elapsed >= Duration.Value)
        {
            return Status.Success;
        }
        
        var direction = (Transform.Value.position - NavAgent.Value.transform.position).normalized;
        
        direction.y = 0;
        
        var lookRotation = Quaternion.LookRotation(direction);

        var rotationToAdd = Quaternion.Euler(0, 90, 0);

        lookRotation *= rotationToAdd;
        
        NavAgent.Value.transform.rotation = Quaternion.Slerp(NavAgent.Value.transform.rotation, lookRotation, elapsed / Duration.Value);
        
        NavAgent.Value.speed = Speed.Value;
        
        NavAgent.Value.SetDestination(NavAgent.Value.transform.position + NavAgent.Value.transform.forward);
        
        return Status.Running;
    }

    protected override void OnEnd()
    {
    }
}

