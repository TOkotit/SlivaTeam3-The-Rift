using System;
using Unity.Behavior;

[BlackboardEnum]
public enum EnemyAIStates
{
	Idle,
	Patrol,
	Chase,
	WaitingForAttack,
	Attack,
	SpecialAbility1,
	SpecialAbility2
}
