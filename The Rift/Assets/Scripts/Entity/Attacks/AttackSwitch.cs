using System.Collections.Generic;
using AYellowpaper;
using UnityEngine;

namespace Entity.Attacks
{
    [CreateAssetMenu(fileName = "AttackSwitch", menuName = "Attacks/Attack switch")]
    public class AttackSwitch : ScriptableObject, IAttackProfile
    {
        [SerializeField] private string _name;
        [SerializeField] private float cooldown;
        [SerializeField] private List<InterfaceReference<IAttackEvent>> events;
        [SerializeField] private List<InterfaceReference<IAttackProfile>> profiles;
        private int currentID;
        public string Name => _name;
        public float Cooldown =>  cooldown;
        public List<InterfaceReference<IAttackEvent>> Events => events;

        public IAttackProfile GetNextAttack()
        {
            var attackToReturn = profiles[currentID].Value;
            currentID = (currentID + 1) % profiles.Count;
            return attackToReturn;
        }
    }
}