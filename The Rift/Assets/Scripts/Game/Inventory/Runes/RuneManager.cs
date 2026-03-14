using System.Collections.Generic;
using Entity.Runes;
using R3;
using UnityEngine;

namespace Game.Inventory.Runes
{
    public class RuneManager
    {
        private readonly RuneDatabase _database;
        private readonly List<RuneType> _unlockedRunes = new();
        
        private readonly Subject<RuneType> _onRuneUnlocked = new();
        public Observable<RuneType> OnRuneUnlocked => _onRuneUnlocked;
        
        public IReadOnlyList<RuneType> UnlockedRunes => _unlockedRunes;
        
        public RuneManager(RuneDatabase database)
        {
            _database = database;
            // Load();
        }
        
        public void UnlockRune(RuneType runeType)
        {
            if (_unlockedRunes.Contains(runeType)) return;

            _unlockedRunes.Add(runeType);
            _onRuneUnlocked.OnNext(runeType);
            // Save();
            Debug.Log($"<color=green>Rune Manager: {runeType} Unlocked & Saved</color>");
        }

        public RuneData GetRuneData(RuneType type) => _database.GetRuneByType(type);

        private const string SaveKey = "UnlockedRunesData";

        private void Save()
        {
            var data = new RuneSaveData { unlocked = _unlockedRunes.ToArray() };
            PlayerPrefs.SetString(SaveKey, JsonUtility.ToJson(data));
            PlayerPrefs.Save();
        }

        private void Load()
        {
            if (!PlayerPrefs.HasKey(SaveKey)) return;
            
            var data = JsonUtility.FromJson<RuneSaveData>(PlayerPrefs.GetString(SaveKey));
            if (data?.unlocked == null) return;

            _unlockedRunes.Clear();
            _unlockedRunes.AddRange(data.unlocked);
        }

        [System.Serializable]
        private class RuneSaveData { public RuneType[] unlocked; }
        
    }
}