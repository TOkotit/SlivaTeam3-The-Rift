using System.Collections.Generic;
using Entity;
using Entity.Attacks;
using NUnit.Framework;

namespace MainCharacter
{
    public class MainCharacterModel : DamagableModel
    {
        private List<WeaponModel>  _weapons;
        public List<WeaponModel> Weapons {get => _weapons; set => _weapons = value; }

        MainCharacterModel()
        {
            Weapons = new List<WeaponModel>();
        }
    }
}