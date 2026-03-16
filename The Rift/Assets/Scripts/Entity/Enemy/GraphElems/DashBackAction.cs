using System;
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

    protected override Status OnStart()
    {
        
        
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        Gameobject.Value.transform.position -= Gameobject.Value.transform.rotation.normalized.eulerAngles * X.Value;
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

