using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using Scripts.Audio;

namespace Scripts.UI
{
    public class SettingsMenu : MonoBehaviour
    {
        [SerializeField] private Button graphics;
        [SerializeField] private Button performance;
        [SerializeField] private Toggle music;
        [SerializeField] private Toggle volume;

        [SerializeField] private GameObject postProcessing;

        public AudioMixer master;

        private void Start()
        {
            int musicVolume = PlayerPrefs.GetInt("musicVolume", 1);

            if (musicVolume == 1)
            {
                music.isOn = false;
            }
            else
            {
                music.isOn = true;
            }

            int masterVolume = PlayerPrefs.GetInt("masterVolume", 1);

            if (masterVolume == 1)
            {
                volume.isOn = false;
            }
            else
            {
                volume.isOn = true;
            }
        }

        private void Update()
        {
            if (postProcessing.activeSelf)
            {
                graphics.Select();
            }
            else
            {
                performance.Select();
            }
        }

        public void Graphics()
        {
            postProcessing.SetActive(true);
            PlayerPrefs.SetInt("Graphics", 0);
        }

        public void Performance()
        {
            postProcessing.SetActive(false);
            PlayerPrefs.SetInt("Graphics", 1);
        }

        public void Volume()
        {
            if (!volume.isOn)
            {
                PlayerPrefs.SetInt("masterVolume", 1);
                master.SetFloat("master", 10);
            }
            else
            {
                PlayerPrefs.SetInt("masterVolume", 0);
                master.SetFloat("master", -100);
            }
        }

        public void Music()
        {
            if (!music.isOn)
            {
                PlayerPrefs.SetInt("musicVolume", 1);
                FindObjectOfType<AudioManager>().GetSource("Music").volume = .4f;
            }
            else
            {
                PlayerPrefs.SetInt("musicVolume", 0);
                FindObjectOfType<AudioManager>().GetSource("Music").volume = 0f;
            }
        }

    }

}
