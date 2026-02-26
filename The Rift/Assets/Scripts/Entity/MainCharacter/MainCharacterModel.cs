using System.Collections.Generic;
using Entity;
using NUnit.Framework;

namespace MainCharacter
{
    public class MainCharacterModel : DamagableModel
    {
        private List<WeaponProfile>  _weapons;
        public List<WeaponProfile> Weapons {get => _weapons; set => _weapons = value; }

        public MainCharacterModel(Health health)
        {
            this.health = health;
            Weapons = new List<WeaponProfile>();
        }
    }
}