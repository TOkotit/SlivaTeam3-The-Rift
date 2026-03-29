using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Utils;
using VContainer;

namespace Systems
{
    public class AudioManager
    {
        [Serializable]
        public class MusicSet
        {
            public string id;
            public AudioClip calm;
            public AudioClip battle;
        }

        [Serializable]
        public class AudioSaveData
        {
            public float masterVolume;
            public float musicVolume;
            public float sfxVolume;
        }
        
        [Inject]
        private ICoroutineRunner  coroutineRunner;
        
        static private AudioManager _instance;

        static public AudioManager Instance
        {
            get
            {
                return _instance ?? (_instance = new AudioManager());
            }
            set
            {
                _instance = value;
            }
        }
        
        private AudioSource _musicSourceA;
        private AudioSource _musicSourceB;
        private AudioSaveData _audioSaveData;
        private Dictionary<string, MusicSet> _musicSetDict = new Dictionary<string, MusicSet>();
        
        private string _currentID;
        
        public void Initialize(AudioSource sourceA, AudioSource sourceB, List<MusicSet> musicSets = null)
        {
            _musicSourceA = sourceA;
            _musicSourceB = sourceB;

            _musicSourceA.loop = true;
            _musicSourceB.loop = true;
            _musicSourceA.playOnAwake = false;
            _musicSourceB.playOnAwake = false;
            
            if (musicSets != null)
                SetMusicSets(musicSets);

            LoadSettings();
            ApplyVolumes();

            _instance = this;
        }

        private void SetMusicSets(List<MusicSet> musicSets)
        {
            foreach (var musicSet in musicSets)
            {
                _musicSetDict.Add(musicSet.id, musicSet);
            }
        }
        
        private void ApplyVolumes()
        {
            _musicSourceA.volume = _audioSaveData.masterVolume *  _audioSaveData.musicVolume;
            _musicSourceB.volume = _audioSaveData.masterVolume * _audioSaveData.musicVolume;
        }

        public IEnumerator SwitchTrack(string id, float duration)
        {
            if (!_musicSetDict.ContainsKey(id))
            {
                Debug.Log("Incorrect id");
                yield break;
            }
            yield return StopTrack(duration);
            _currentID = id;
            _musicSourceA.clip = _musicSetDict[id].calm;
            _musicSourceB.clip = _musicSetDict[id].battle;
            FadeUp(_musicSourceA, duration);
            yield return new WaitForSeconds(duration);
        }

        public IEnumerator StopTrack(float duration)
        {
            FadeDown(_musicSourceA, duration);
            FadeDown(_musicSourceB, duration);
            yield return new WaitForSeconds(duration);
        }

        public void SaveSettings()
        {
            string jsonString = JsonUtility.ToJson(_audioSaveData, true);
            string filePath = Path.Combine(Application.persistentDataPath, "AudioSettingsData.json");
            File.WriteAllText(filePath, jsonString);
        }
        
        public void LoadSettings()
        {
            string filePath = Path.Combine(Application.persistentDataPath, "AudioSettingsData.json");
            if (File.Exists(filePath))
            {
                string jsonString = File.ReadAllText(filePath);
                _audioSaveData = JsonUtility.FromJson<AudioSaveData>(jsonString);
            }
            else
            {
                _audioSaveData = new AudioSaveData();
            }
        }

        public void FadeUp(AudioSource source, float duration)
        {
            source.Play();
            while (source.volume < 1)
            {
                source.volume += Time.deltaTime / duration;
            }
        }

        public void FadeDown(AudioSource source, float duration)
        {
            while (source.volume > 0)
            {
                source.volume -= Time.deltaTime / duration;
            }
            source.Stop();
        }

        public void SwitchToBattle(float duration)
        {
            FadeDown(_musicSourceA, duration);
            FadeUp(_musicSourceB, duration);
        }

        public void SwitchToCalm(float duration)
        {
            FadeDown(_musicSourceB, duration);
            FadeUp(_musicSourceA, duration);
        }
        
        public void PlaySound(AudioSource source, AudioClip clip)
        {
            source.clip = clip;
            source.volume = _audioSaveData.masterVolume * _audioSaveData.sfxVolume;
            source.PlayOneShot(clip);
        }
        public float MasterVolume
        {
            get => _audioSaveData.masterVolume;
            set { _audioSaveData.masterVolume = Mathf.Clamp01(value); ApplyVolumes(); SaveSettings(); }
        }
        public float MusicVolume
        {
            get => _audioSaveData.musicVolume;
            set { _audioSaveData.musicVolume = Mathf.Clamp01(value); ApplyVolumes(); SaveSettings(); }
        }
        public float SFXVolume
        {
            get => _audioSaveData.sfxVolume;
            set { _audioSaveData.sfxVolume = Mathf.Clamp01(value); SaveSettings(); }
        }
    }
}