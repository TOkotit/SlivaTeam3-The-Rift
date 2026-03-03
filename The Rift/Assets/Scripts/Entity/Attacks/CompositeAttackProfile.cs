using System;
using System.Collections.Generic;
using AYellowpaper;
using UnityEngine;


namespace Entity.Attacks
{
    [CreateAssetMenu(fileName = "CompositeAttack", menuName = "Attacks/Composite attack")]
    public class CompositeAttackProfile : ScriptableObject, IAttackProfile
    {
        [SerializeField] private string name;
        [SerializeField] private float cooldown;
        [SerializeField] private List<AttackTiming> attackTimings;
        [SerializeField] private List<InterfaceReference<IAttackEvent>> events;
        public List<AttackTiming> AttackTimings => attackTimings;

        [Serializable]
        public class AttackTiming
        {
            public float Timing;
            public InterfaceReference<IAttackProfile> Attack;
        }

        public string Name => name;
        public float Cooldown  => cooldown;
        public List<InterfaceReference<IAttackEvent>> Events => events;
    }
}