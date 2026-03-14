using Entity.Attacks;
using UnityEngine;
using VContainer;

namespace Entity
{
    [CreateAssetMenu(fileName = "DashOnAttackEvent", menuName = "AttackEvents/DashOnAttackEvent")]
    public class DashOnAttackEvent : ScriptableObject, IAttackEvent
    {
        [Inject] private MainCharacterMovement _controller;
        public void Act()
        {
            _controller.Dash();
        }
    }
}