using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using UnityEngine.AI;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Rotate to target", story: "Rotates [gameobgect] to [target]", category: "Action", id: "18f8adc0a9a4364b69884159dce7d027")]
public partial class RotateToTargetAction : Action
{
    [SerializeReference] public BlackboardVariable<Transform> Gameobgect;
    [SerializeReference] public BlackboardVariable<Transform> Target;
    private float rotationSpeed = 15f;
    private NavMeshAgent agent;
    
    protected override Status OnStart()
    {
        agent = Gameobgect.Value.GetComponent<NavMeshAgent>();

        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        var direction = (Target.Value.position - Gameobgect.Value.transform.position).normalized;
        
        direction.y = 0;

        
        
        var lookRotation = Quaternion.LookRotation(direction);
        
        if (Quaternion.Angle(lookRotation, Gameobgect.Value.transform.rotation) 
            < 5f) return Status.Success;
        
        Gameobgect.Value.transform.rotation = Quaternion.Slerp(Gameobgect.Value.transform.rotation, 
            lookRotation, 
            Time.fixedDeltaTime * rotationSpeed);
        
        
        return Status.Running;
    }

    protected override void OnEnd()
    {
    }
}

