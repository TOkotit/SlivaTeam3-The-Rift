using System;
using System.Collections;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

using Utils;
using Random = System.Random;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "WaitRandomTime", story: "Wait for random time [value1] to [value2] seconds with [coroutines]", category: "Action", id: "b5cda3a5ee130e0bf1c6b9c9d9405565")]
public partial class WaitRandomTimeAction : Action
{
    [SerializeReference] public BlackboardVariable<float> Value1;
    [SerializeReference] public BlackboardVariable<float> Value2;
    [SerializeReference] public BlackboardVariable<Coroutines> Coroutines;
    private readonly WaitForFixedUpdate _waitForFixedUpdate = new WaitForFixedUpdate();
    private bool isTimerGoing;
    private float duration;
    
    protected override Status OnStart()
    {
        var rnd = new Random();
        var range = (double)Value2.Value - (double)Value1.Value;
        var sample = rnd.NextDouble();
        var scaled = (sample * range) + Value1.Value;
        
        duration = (float)scaled;
        isTimerGoing = true;

        Coroutines.Value.StartCoroutine(StartTimer());
        return Status.Running;
    }
    
    IEnumerator StartTimer() {
        var elapsed = 0f;
        while (elapsed < duration) {
            elapsed += Time.fixedDeltaTime;
            
            yield return _waitForFixedUpdate;
        }
        isTimerGoing = false;
        yield return null;
    }
    
    protected override Status OnUpdate()
    {
        if (isTimerGoing)
        {
            return Status.Running;
        }
        return Status.Success;
    }
    
    
    protected override void OnEnd()
    {
    }
}

