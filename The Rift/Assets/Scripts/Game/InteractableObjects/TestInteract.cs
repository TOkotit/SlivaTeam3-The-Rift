using System.Runtime.InteropServices;
using Entity.Runes;
using Game.Gameplay.View.UI;
using Game.Inventory.Runes;
using MainCharacter;
using UnityEngine;
using VContainer;

namespace Game
{
    public class TestInteract: MonoBehaviour, IInteractable
    {
        [Inject] private RuneManager  _runesManager;
        [SerializeField] RuneData Persistence;
        [SerializeField] RuneData Catalyst;
        public void Interact()
        {
            Debug.Log("Interact");
            _runesManager.UnlockRune(RuneType.Persistence);
            _runesManager.UnlockRune(RuneType.Catalyst);
            foreach (var rune in _runesManager.UnlockedRunes)
            {
                Debug.Log(rune);
            }
        }

        public Transform InteractionPoint => transform;
        
    }
}