using Entity.Attacks;
using UnityEngine;
using VContainer;

namespace Entity
{
    [CreateAssetMenu(fileName = "DashOnAttackEvent", menuName = "AttackEvents/DashOnAttackEvent")]
    public class DashOnAttackEvent : ScriptableObject, IAttackEvent
    {
        public void Act()
        {
            MainCharacterMovement.singleton.Dash();
        }
    }
}