using System.Linq;
using Entity.Runes;
using UnityEngine;

namespace Game.Inventory.Runes.Runes_Data
{
    [CreateAssetMenu(fileName = "ShaetRune", menuName = "Runes/Custom/ShaetRune")]

    public class ShaetRune : RuneData
    {
        public float NonSpecialSlotBobus = 0.30f;
        public float pullForce = 1f;
        public float duration = 1f;
        public float cooldown = 2f;
        
        
        [Header("Runtime State")]
        private bool _isActive = false;
        private float _nextCanUseTime = 0f;
        
        public override float GetStatMultiplier(Influence parameter, RuneContext context)
        {
            if (parameter == Influence.Cooldown && context.CurrentSlotType != RuneSlotsType.Special)
                return NonSpecialSlotBobus;

            return 0;
        }
        
        public override void OnWeaponHit(RuneContext context)
        {
            if (context.CurrentSlotType == RuneSlotsType.Special)
            {
                if (_isActive)
                {
                    Debug.Log("Способность еще активна!");
                    return;
                }
                
                if (Time.time < _nextCanUseTime)
                {
                    Debug.Log($"Нужно подождать еще {_nextCanUseTime - Time.time:F1} сек.");
                    return;
                }
                
                _isActive = true;
                                
                var radius = context.IsHitEnemy ? 10f : 4f;

                var workerObj = new GameObject("DynamicPull_Effect");
                var worker = workerObj.AddComponent<KinematicPuller>();
                
                worker.Initialize(context.HitPoint, pullForce, duration, radius, LayerMask.GetMask("Enemy"), OnEffectFinished);
                
                Debug.Log("<color=red> Притягивание </color>");
            }
        }
        
        private void OnEffectFinished()
        {
            _isActive = false;
            _nextCanUseTime = Time.time + cooldown;
            Debug.Log("Эффект окончен, начался откат.");
        }

        private void OnEnable()
        {
            _isActive = false;
            _nextCanUseTime = 0f;
        }
        
        public virtual void OnTick(RuneContext context) { }

    }
}