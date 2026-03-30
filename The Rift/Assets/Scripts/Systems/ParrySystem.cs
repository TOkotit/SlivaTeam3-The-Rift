using System;
using System.Collections;
using Entity.Enemy;
using Unity.VisualScripting;
using UnityEngine;
using Utils.MiscClasses;
using VContainer;

namespace Systems
{
    public class ParrySystem
    {
        private static ParrySystem instance;
        [Inject]
        private MainCharacter.MainCharacter mainCharacter;
        public static ParrySystem Instance => instance ?? (instance = new ParrySystem());
        public static Action<EnemyModel> onParry;
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
            yield return new WaitForSeconds(mainCharacter.MainCharacterModel.ParryReloadDelay);
            parryAvailable = true;
        }

        private IEnumerator DoTheParry()
        {
            yield return new WaitForSeconds(0.9f);
            isParrying = false;
        }
    }
}