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
        private Dictionary<string, WeaponProfile> _profilesRegistry;

        public WeaponManager()
        {
            _profiles = Resources.LoadAll<WeaponProfile>("Configs/Weapons").ToList();
            foreach (var profile in _profiles)
            {
                _profilesRegistry.Add(profile.Name, profile);
            }
        }

        public WeaponModel CreateWeapon(string name)
        {
            var model = new WeaponModel(_profilesRegistry[name]);
            return model;
        }
    }
}