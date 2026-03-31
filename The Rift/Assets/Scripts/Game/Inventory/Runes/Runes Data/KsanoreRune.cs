using Entity.Runes;
using UnityEngine;

namespace Game.Inventory.Runes.Runes_Data
{
    [CreateAssetMenu(fileName = "KsanoreRune", menuName = "Runes/Custom/KsanoreRune")]

    public class KsanoreRune : RuneData
    {
        public GameObject kernelPrefab;
        public float NonSpecialSlotBobus = 0.30f;
        
        [Header("Hit Stats")]
        public int kernelsInEnemy = 8;
        public float durationInEnemy = 3f;
        
        [Header("Miss Stats")]
        public int kernelsOnFloor = 10;
        public float durationOnFloor = 1f;
        
        public float cooldown = 2f;
        public float damage = 10f;

        [Header("Runtime State")]
        private bool _isActive = false;
        private float _nextCanUseTime = 0f;
        
        public override float GetStatMultiplier(Influence parameter, RuneContext context)
        {
            if (parameter == Influence.Durability && context.CurrentSlotType != RuneSlotsType.Special)
                return NonSpecialSlotBobus;

            return 0;
        }
        
        public override void OnWeaponHit(RuneContext context)
        {
            if (context.CurrentSlotType == RuneSlotsType.Special)
            {
                if (_isActive) return;

                if (Time.time < _nextCanUseTime)
                {
                    Debug.Log($"Ксанор на кулдауне: {_nextCanUseTime - Time.time:F1} сек.");
                    return;
                }

                _isActive = true;

                var workerObj = new GameObject("Ksanore_Steel_Effect");
                var worker = workerObj.AddComponent<KsanoreKernelsEffect>();

                var currentDuration = context.IsHitEnemy ? durationInEnemy : durationOnFloor;
                var count = context.IsHitEnemy ? kernelsInEnemy : kernelsOnFloor;
                
                Transform parent = null;
                if (context.Target != null) 
                {
                    parent = context.Target.transform;
                }

                worker.Initialize(
                    context.HitPoint, 
                    kernelPrefab, 
                    count, 
                    currentDuration, 
                    context.IsHitEnemy, 
                    damage,
                    LayerMask.GetMask("Enemy"), 
                    parent, 
                    OnEffectFinished
                );
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