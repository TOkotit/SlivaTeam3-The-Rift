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
        [Inject] MainCharacter.MainCharacter _mainCharacter;
        [SerializeField] RuneData runeData;
        public void Interact()
        {
            Debug.Log("Interact");
            _mainCharacter.MainCharacterModel.Weapons[0].AddRune(runeData);
        }

        public Transform InteractionPoint => transform;
        
    }
}