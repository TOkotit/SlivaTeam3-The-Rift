using System;
using Unity.Behavior;

[BlackboardEnum]
public enum SprinterAiStates
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
