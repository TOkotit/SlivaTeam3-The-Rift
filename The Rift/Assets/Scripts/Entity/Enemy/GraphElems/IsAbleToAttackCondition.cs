using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "IsAbleToAttack", story: "[Enemy] is able to attack", category: "Conditions", id: "68eb36a1b506ae8c25b892cd63d45f29")]
public partial class IsAbleToAttackCondition : Condition
{
    [SerializeReference] public BlackboardVariable<EnemyAttackController> Enemy;

    public override bool IsTrue()
    {
        return Enemy.Value.IsAbleToAttack;
    }

    public override void OnStart()
    {
    }

    public override void OnEnd()
    {
    }
}
