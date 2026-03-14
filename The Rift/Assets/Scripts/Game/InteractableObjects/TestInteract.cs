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
        [Inject] RuneManager runeManager;
        [SerializeField] RuneType rune1;
        [SerializeField] RuneType rune2;
        [SerializeField] RuneType rune3;
        [SerializeField] RuneType rune4;
        
        public void Interact()
        {
            Debug.Log("Interact");
            runeManager.UnlockRune(rune1);
            runeManager.UnlockRune(rune2);
            runeManager.UnlockRune(rune3);
            runeManager.UnlockRune(rune4);
        }

        public Transform InteractionPoint => transform;
        
    }
}