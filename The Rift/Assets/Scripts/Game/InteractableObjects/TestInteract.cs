using System.Linq;
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
        [SerializeField] RuneType runeToInsert;
        [Inject] MainCharacterModel mainCharacterModel;
        
        public void Interact()
        {
            runeManager.UnlockRune(runeToInsert);
        }

        public Transform InteractionPoint => transform;
        
    }
}