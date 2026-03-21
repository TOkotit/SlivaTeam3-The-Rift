using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "DashBack", story: "[Gameobject] dashes back for [x] units", category: "Action", id: "b0f52a8e44688abf1addae6932a0ff14")]
public partial class DashBackAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Gameobject;
    [SerializeReference] public BlackboardVariable<float> X;
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
        var direction = Gameobject.Value.transform.forward * -1;
        var targetPos = startPos + direction * X.Value;
        

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

