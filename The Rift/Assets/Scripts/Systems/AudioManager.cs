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

        static public AudioManager Instance { get => _instance;  set =>  _instance = value;  }
        
        private AudioSource _musicSourceA;
        private AudioSource _musicSourceB;
        private AudioSaveData _audioSaveData;
        private Dictionary<string, MusicSet> _musicSetDict = new Dictionary<string, MusicSet>();
        
        private string _currentID;
        
        public void Initialize(AudioSource sourceA, AudioSource sourceB, List<MusicSet> musicSets = null)
        {
            _instance = this;
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
            coroutineRunner.StartRoutine(FadeDown(_musicSourceA, 0));
            coroutineRunner.StartRoutine(FadeDown(_musicSourceB, 0)); 
        }

        private void SetMusicSets(List<MusicSet> musicSets)
        {
            foreach (var musicSet in musicSets)
            {
                _musicSetDict[musicSet.id] = musicSet;
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
            yield return FadeUp(_musicSourceA, duration);
        }

        public IEnumerator StopTrack(float duration)
        {
            yield return FadeDown(_musicSourceA, duration);
            yield return FadeDown(_musicSourceB, duration);
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
                _audioSaveData = new AudioSaveData
                {
                    masterVolume = 1f,
                    musicVolume = 1f,
                    sfxVolume = 1f
                };
            }
        }

        public IEnumerator FadeUp(AudioSource source, float duration)
        {
            source.Play();
            var startVolume = source.volume;
            var targetVolume = _audioSaveData.masterVolume * _audioSaveData.musicVolume;
            for (float t = 0; t < duration; t += Time.deltaTime)
            {
                source.volume = Mathf.Lerp(startVolume, targetVolume, t / duration);
                yield return null;
            }

            source.volume = targetVolume;
        }

        public IEnumerator FadeDown(AudioSource source, float duration)
        {
            var startVolume = source.volume;

            for (float t = 0; t < duration; t += Time.deltaTime)
            {
                source.volume = Mathf.Lerp(startVolume, 0f, t / duration);
                yield return null;
            }
            source.volume = 0f;
            source.Stop();
        }

        public void SwitchToBattle(float duration)
        {
            coroutineRunner.StartRoutine(FadeDown(_musicSourceA, duration));
            coroutineRunner.StartRoutine(FadeUp(_musicSourceB, duration));
        }

        public void SwitchToCalm(float duration)
        {
            coroutineRunner.StartRoutine(FadeDown(_musicSourceB, duration));
            coroutineRunner.StartRoutine(FadeUp(_musicSourceA, duration));
        }
        
        public void PlaySound(AudioSource source, AudioClip clip)
        {
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