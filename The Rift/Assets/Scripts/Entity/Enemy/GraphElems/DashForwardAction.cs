using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "DashForward", story: "[Gameobject] dashes forward for [value] units", category: "Action", id: "252d72cb7e693dfcf8403600a2cdd047")]
public partial class DashForwardAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Gameobject;
    [SerializeReference] public BlackboardVariable<float> Value;
    private float duration = 0.5f;
    private float elapsed = 0f;
    protected override Status OnStart()
    {
        elapsed = 0f;
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        var startPos = Gameobject.Value.transform.position;
        var direction = Gameobject.Value.transform.forward;
        var targetPos = startPos + direction * Value.Value;
        

        if (elapsed >= duration)
        {
            Gameobject.Value.transform.position = targetPos;
            return Status.Success;
        }

        Gameobject.Value.transform.position = Vector3.Lerp(startPos, targetPos, elapsed / duration);
        elapsed += Time.deltaTime;
        
        return Status.Running;
    }

    protected override void OnEnd()
    {
    }
}

