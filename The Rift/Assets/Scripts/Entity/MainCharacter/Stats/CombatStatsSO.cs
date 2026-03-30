using UnityEngine;

namespace MainCharacter
{
    [CreateAssetMenu(fileName = "CombatStats", menuName = "MainCharacter/CombatStatsSO")]
    public class CombatStatsSO : ScriptableObject
    {
        [SerializeField] private float parryReloadDelay;
        public float ParryReloadDelay => parryReloadDelay;
    }
}