using System.Collections.Generic;
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
        [SerializeField] RuneType runeToInsert2;
        [Inject] MainCharacterModel mainCharacterModel;
        
        public void Interact()
        {
            runeManager.UnlockRune(runeToInsert);
            runeManager.UnlockRune(runeToInsert2);
        }

        public Transform InteractionPoint => transform;
        
    }
}