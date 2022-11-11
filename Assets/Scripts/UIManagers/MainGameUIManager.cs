using UnityEngine;
using UnityEngine.SceneManagement;
using Scripts.Audio;

namespace Scripts.UI
{
    public class MainGameUIManager : MonoBehaviour
    {
        [SerializeField] private GameObject pauseMenu;

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

            if (pauseMenu != null)
            {
                pauseMenu.SetActive(false);
            }
        }

        public void Restart()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("MainGame");
        }

        public void ResumeGame()
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
        }

        public void PauseMenu()
        {
            if (pauseMenu.activeSelf)
            {
                pauseMenu.SetActive(false);
                Time.timeScale = 1f;
            }
            else
            {
                Time.timeScale = 0f;
                pauseMenu.SetActive(true);
            }
        }

        public void LoadMainMenu()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("MainMenu");
        }

        public void PlayButtonSound()
        {
            FindObjectOfType<AudioManager>().Play("ButtonClick");
        }
    }

}
