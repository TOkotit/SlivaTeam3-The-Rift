using Entity.Attacks;
using Systems;
using UnityEngine;

namespace Entity
{
    [CreateAssetMenu(fileName = "ParryEvent", menuName = "AttackEvents/ParryEvent")]
    public class ParryEvent : ScriptableObject, IAttackEvent
    {
        public void Act()
        {
            ParrySystem.Instance.Parry();
        }
    }
}