using System;
using System.Collections;
using Entity.Enemy;
using MainCharacter;
using Unity.VisualScripting;
using UnityEngine;
using Utils.MiscClasses;
using VContainer;

namespace Systems
{
    public class ParrySystem
    { 
        private static ParrySystem _instance;
        public static ParrySystem Instance => _instance;
        
        public static void SetInstance(ParrySystem system)
        {
            _instance = system;
        }
        
        [Inject]
        private MainCharacterModel _mainCharacterModel;
        public static Action<EnemyModel> _onParry;
        private bool parryAvailable = true;
        public bool ParryAvailable => parryAvailable;
        private bool isParrying;
        public bool IsParrying => isParrying;

        public void Parry()
        {
            isParrying = true;
            if (parryAvailable)
            {
                parryAvailable = false;
                CoroutineRunner.instance.StartCoroutine(ReloadParry());
                CoroutineRunner.instance.StartCoroutine(DoTheParry());
            }
        }

        private IEnumerator ReloadParry()
        {
            yield return new WaitForSeconds(_mainCharacterModel.ParryReloadDelay);
            parryAvailable = true;
        }

        private IEnumerator DoTheParry()
        {
            yield return new WaitForSeconds(_mainCharacterModel.ParryDuration);
            isParrying = false;
        }
    }
}