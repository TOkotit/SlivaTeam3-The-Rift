using Enums;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using UnityEngine.AI;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Dash", story: "[Gameobject] dashes [direction] for [value]", category: "Action", id: "e6932b791ffd3ded7964685a356e3fb7")]
public partial class DashAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Gameobject;
    [SerializeReference] public BlackboardVariable<Direction> Direction;
    [SerializeReference] public BlackboardVariable<float> Value;
    private float duration = 1f;
    private float elapsed = 0f;
    private Vector3 startPos;
    private Vector3 direction;
    private Vector3 targetPos;
    private Rigidbody rigidbody;
    
    protected override Status OnStart()
    {
        rigidbody = Gameobject.Value.GetComponent<Rigidbody>();
        
        startPos = Gameobject.Value.transform.position;
        direction = Direction.Value switch
        {
            Enums.Direction.Forward => Gameobject.Value.transform.forward,
            Enums.Direction.Backward => Gameobject.Value.transform.forward * -1,
            Enums.Direction.Left => Gameobject.Value.transform.right * -1,
            Enums.Direction.Right => Gameobject.Value.transform.right * 1,
            _ => Gameobject.Value.transform.forward
        };
        
        targetPos = startPos + direction * Value.Value;
        elapsed = 0f;

        // Debug.Log($"{direction}");
        // Debug.Log($"{startPos} {targetPos}");
        
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (elapsed >= duration)
        {
            Gameobject.Value.transform.position = targetPos;
            return Status.Success;
        }
        var newPosition = Vector3.Lerp(startPos, targetPos, elapsed / duration);
        
        rigidbody.MovePosition(newPosition);
        elapsed += Time.fixedDeltaTime;
        
        return Status.Running;
    }

    protected override void OnEnd()
    {
    }
}

