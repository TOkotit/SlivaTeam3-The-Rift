using System;
using Unity.Behavior;

[BlackboardEnum]
public enum WarriorAiStates
{
	Idle,
	Patrol,
	Chase,
	WaitingForAttack,
	Attack,
	SpecialAbility1,
	SpecialAbility2,
	Dead
}
