using UnityEngine;
using UnityEngine.SceneManagement;
using Scripts.Audio;

namespace Scripts.UI
{
    public class MainMenuUIManager : MonoBehaviour
    {
        [SerializeField] private GameObject settingsPanel;
        [SerializeField] private GameObject mainMenu;
        [SerializeField] private GameObject aboutPanel;

        [SerializeField] private GameObject postProcessing;

        private void Start()
        {
            if (PlayerPrefs.GetInt("Graphics", 0) == 0)
            {
                postProcessing.SetActive(true);
            }
            else
            {
                postProcessing.SetActive(false);
            }

            Application.targetFrameRate = 60;

            PlayerPrefs.SetInt("Skin", 1);

            settingsPanel.SetActive(false);
            aboutPanel.SetActive(false);
            mainMenu.SetActive(true);
        }

        public void PlayGame()
        {
            SceneManager.LoadScene("MainGame");
        }

        public void LoadTutorial()
        {
            SceneManager.LoadScene("Tutorial");
        }

        public void LoadStats()
        {
            SceneManager.LoadScene("Stats");
        }

        public void LoadAchievements()
        {
            SceneManager.LoadScene("Achievements");
        }

        public void LoadCollections()
        {
            SceneManager.LoadScene("Collection");
        }

        public void LoadSettings()
        {
            settingsPanel.SetActive(true);
            mainMenu.SetActive(false);
            aboutPanel.SetActive(false);
        }

        public void LoadAbout()
        {
            settingsPanel.SetActive(false);
            mainMenu.SetActive(false);
            aboutPanel.SetActive(true);
        }

        public void BackToMainMenu()
        {
            settingsPanel.SetActive(false);
            mainMenu.SetActive(true);
            aboutPanel.SetActive(false);
        }

        public void PlayButtonSound()
        {
            FindObjectOfType<AudioManager>().Play("ButtonClick");
        }
    }

}
