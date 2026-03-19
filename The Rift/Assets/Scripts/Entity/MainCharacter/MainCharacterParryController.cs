using System;
using Entity;
using Systems;
using UnityEngine;
using VContainer;

namespace MainCharacter
{
    public class MainCharacterParryController : MonoBehaviour
    {
        private int _parryAreas;
        [Inject] private Health _mainCharHealth;
        [Inject] private AttackSystem _attackSystem;

        public event Action OnParry;
        
        public int ParryAreas
        {
            get => _parryAreas;
            set => _parryAreas = value;
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("ParryArea"))
            {
                _parryAreas++;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("ParryArea"))
            {
                _parryAreas--;
            }
        }

        public void TryParry()
        {
            if (_parryAreas > 0)
            {
                Parry();
            }
        }


        public void Parry()
        {
            OnParry?.Invoke();

            // _mainCharHealth.DamageImmunity = true;
                
            
        }
    }
}