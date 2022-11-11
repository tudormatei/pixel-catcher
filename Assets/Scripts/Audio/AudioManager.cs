using UnityEngine;
using System;
using UnityEngine.Audio;

namespace Scripts.Audio
{
    public class AudioManager : MonoBehaviour
    {
        public Sound[] sounds;

        public static AudioManager instance;

        public AudioMixerGroup master;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
                return;
            }

            DontDestroyOnLoad(gameObject);

            foreach (Sound s in sounds)
            {
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.outputAudioMixerGroup = master;
                s.source.clip = s.clip;

                s.source.volume = s.volume;
                s.source.pitch = s.pitch;
                s.source.loop = s.loop;
            }
        }

        private void Start()
        {
            Play("Music");

            int musicVolume = PlayerPrefs.GetInt("musicVolume", 1);

            if (musicVolume == 1)
            {
                GetSource("Music").volume = .4f;
            }
            else
            {
                GetSource("Music").volume = 0f;
            }

            int masterVolume = PlayerPrefs.GetInt("masterVolume", 1);

            if (masterVolume == 1)
            {
                master.audioMixer.SetFloat("master", 10);
            }
            else
            {
                master.audioMixer.SetFloat("master", -100);
            }
        }

        public void Play(string name)
        {
            Sound s = Array.Find(sounds, sound => sound.name == name);
            if (s == null) return;

            s.source.Play();
        }

        public void Stop(string name)
        {
            Sound s = Array.Find(sounds, sound => sound.name == name);
            if (s == null) return;

            s.source.Stop();
        }

        public AudioSource GetSource(string name)
        {
            Sound s = Array.Find(sounds, sound => sound.name == name);
            if (s == null) return null;

            return s.source;
        }
    }
}
