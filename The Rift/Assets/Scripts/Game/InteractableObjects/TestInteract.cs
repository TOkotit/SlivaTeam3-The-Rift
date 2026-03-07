using System.Runtime.InteropServices;
using Entity.Runes;
using Game.Gameplay.View.UI;
using MainCharacter;
using UnityEngine;
using VContainer;

namespace Game
{
    public class TestInteract: MonoBehaviour, IInteractable
    {
        [SerializeField] RuneData Persistence;
        [SerializeField] RuneData Catalyst;
        [Inject] private MainCharacter.MainCharacter _mainCharacter;
        public void Interact()
        {
            var characterWeapons = _mainCharacter.MainCharacterModel.Weapons;
            Debug.Log($"<color=green> weapons count = {characterWeapons.Count}</color>");
            foreach (var weapon  in characterWeapons )
            {
                Debug.Log($"<color=green>Weapon name:</color> {weapon.Name}\n" +
                          $"<color=green>Damage: {weapon.Damage}</color>\n" +
                          $"<color=green>CurrentDurability: {weapon.CurrentDurability}</color>\n" +
                          $"<color=green>AttackSpeed: {weapon.AttackSpeed}</color>\n");
                weapon.AddRune(Persistence);
                weapon.AddRune(Catalyst);
            }
            
            foreach (var weapon  in characterWeapons )
            {
                Debug.Log($"After adding rune\n" +
                          $"<color=green>Weapon name:</color> {weapon.Name}\n" +
                          $"<color=green>Damage: {weapon.Damage}</color>\n" +
                          $"<color=green>CurrentDurability: {weapon.CurrentDurability}</color>\n" +
                          $"<color=green>AttackSpeed: {weapon.AttackSpeed}</color>\n");
            }
            
        }

        public Transform InteractionPoint => transform;
    }
}