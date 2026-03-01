using System;
using Unity.Behavior;

[BlackboardEnum]
public enum EnemyAIStates
{
	Patrol,
	Chase,
	Attack,
	Idle,
	SpecialAbility
}
