using Entity.Runes;
using UnityEngine;

namespace Game.Inventory.Runes.Runes_Data
{
    [CreateAssetMenu(fileName = "SharpnessRuneTest", menuName = "Runes/Custom/Sharpness RuneTest")]
    public class SharpnessRuneTest : RuneData
    {
        public GameObject SharpnessEffectPrefab;
        public Vector3 EffectOffset = new Vector3(0, 0.1f, 0);
        public float BaseDamageBonus = 0.15f; 
        public float SynergyDamageBonus = 0.30f;
        public override float GetStatMultiplier(Influence parameter, RuneContext context)
        {
            if (parameter == Influence.Damage)
            {
                if (context.CurrentSlotType == RuneSlotsType.Special)
                    return 1f + SynergyDamageBonus;
                
                return 1f + BaseDamageBonus;
            }

            return 1f;
        }
        
        public override void OnWeaponHit(RuneContext context)
        {

            if (SharpnessEffectPrefab != null && context.HitPoint != Vector3.zero)
            {
                var spawnPosition = context.HitPoint + EffectOffset;
                
                Instantiate(SharpnessEffectPrefab, spawnPosition, Quaternion.identity);
            }
        }
        
        
    }
}