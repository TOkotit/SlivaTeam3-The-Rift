using System.Linq;
using Entity.Runes;
using Game.Inventory.Runes;
using MainCharacter;
using UnityEngine;
using VContainer;

namespace Game
{
    public class TestInteract2 : MonoBehaviour, IInteractable
    {
        [Inject] RuneManager runeManager;
        [SerializeField] RuneType runeToInsert;
        [Inject] MainCharacterModel mainCharacterModel;
        
        public void Interact()
        {
            
            var data = runeManager.GetRuneData(runeToInsert);
            
            if (data == null)
            {
                Debug.LogError("Rune Data not found in Database!");
                return;
            }
            
            var weapon = mainCharacterModel.Weapons.FirstOrDefault();
            if (weapon == null)
            {
                Debug.LogWarning("Character has no weapon to insert rune!");
                return;
            }
            
            var success = weapon.TryInsertRune(data, 1);
            if (success)
            {
                Debug.Log($"<color=green>Success!</color> Rune {data.runeName} inserted into weapon slot 0.");
            }
            else
            {
                Debug.LogWarning("Failed to insert rune. Slot might be occupied or type mismatch.");
            }
        }

        public Transform InteractionPoint => transform;
        
    }
}