using System.Collections.Generic;
using System.Linq;
using Entity;
using Entity.Attacks;
using UnityEngine;

namespace Systems
{
    public class WeaponManager
    {
        private List<WeaponProfile> _profiles;
        private Dictionary<string, WeaponProfile> _profilesRegistry = new Dictionary<string, WeaponProfile>();

        public WeaponManager()
        {
            _profiles = Resources.LoadAll<WeaponProfile>("Configs/Weapons").ToList();
            Debug.Log($"Loaded {_profiles.Count} weapon profiles");
            foreach (var profile in _profiles)
            {
                _profilesRegistry.Add(profile.Name, profile);
            }
        }

        public Weapon CreateWeapon(string name)
        {
            var model = new WeaponModel(_profilesRegistry[name]);
            var attacks = _profilesRegistry[name].Attacks.ToList();
            var weapon = new Weapon(model, attacks);
            return weapon;
        }

        public Weapon CreateWeapon(WeaponModel model)
        {
            var attacks = _profilesRegistry[model.Name].Attacks.ToList();
            var weapon = new Weapon(model, attacks);
            return weapon;
        }
    }
}