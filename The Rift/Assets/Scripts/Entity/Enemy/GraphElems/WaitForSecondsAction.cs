using System;
using System.Collections;
using Unity.Behavior;
using UnityEngine;
using Utils;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Wait for seconds", story: "Wait for [value] seconds with [coroutines]", category: "Action", id: "6a703f8558f4332b57293db8753934da")]
public partial class WaitForSecondsAction : Action
{
    [SerializeReference] public BlackboardVariable<float> Value;
    [SerializeReference] public BlackboardVariable<Coroutines> Coroutines;
    private readonly WaitForFixedUpdate _waitForFixedUpdate = new WaitForFixedUpdate();
    private bool isTimerGoing;

    
    protected override Status OnStart()
    {
        isTimerGoing = true;

        Coroutines.Value.StartCoroutine(StartTimer());
        return Status.Running;
    }
    
    IEnumerator StartTimer() {
        var elapsed = 0f;
        while (elapsed < Value.Value) {
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

