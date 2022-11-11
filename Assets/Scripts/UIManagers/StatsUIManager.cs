using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Scripts.Audio;

namespace Scripts.UI
{
    public class StatsUIManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI highscore;
        [SerializeField] private TextMeshProUGUI points;
        [SerializeField] private TextMeshProUGUI spikes;
        [SerializeField] private TextMeshProUGUI shields;
        [SerializeField] private TextMeshProUGUI deaths;
        [SerializeField] private TextMeshProUGUI combo;
        [SerializeField] private TextMeshProUGUI timePlayed;
        [SerializeField] private TextMeshProUGUI level;

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

            ShowStats();
        }

        public void BackToMainMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }

        private void ShowStats()
        {
            highscore.text = "Highscore: " + PlayerPrefs.GetInt("Highscore").ToString();
            points.text = "Points destroyed: " + PlayerPrefs.GetInt("Points").ToString();
            spikes.text = "Spikes destroyed: " + PlayerPrefs.GetInt("Spikes").ToString();
            shields.text = "Shield destroyed: " + PlayerPrefs.GetInt("Shields").ToString();
            deaths.text = "Deaths: " + PlayerPrefs.GetInt("Deaths").ToString();
            combo.text = "Highest combo: " + PlayerPrefs.GetInt("Combo");
            int timePlayedSeconds = (int)PlayerPrefs.GetFloat("PlayTime");
            int res = 0;
            if (timePlayedSeconds < 60)
            {
                if (timePlayedSeconds == 1)
                {
                    timePlayed.text = "Time played: " + timePlayedSeconds.ToString() + " second";
                }
                else
                {
                    timePlayed.text = "Time played: " + timePlayedSeconds.ToString() + " seconds";
                }
            }
            else if (timePlayedSeconds < 3600)
            {
                res = (int)PlayerPrefs.GetFloat("PlayTime") / 60;
                if (res == 1)
                {
                    timePlayed.text = "Time played: " + res.ToString() + " minute";
                }
                else
                {
                    timePlayed.text = "Time played: " + res.ToString() + " minutes";
                }
            }
            else
            {
                res = (int)PlayerPrefs.GetFloat("PlayTime") / 3600;
                if (res == 1)
                {
                    timePlayed.text = "Time played: " + res.ToString() + " hour";
                }
                else
                {
                    timePlayed.text = "Time played: " + res.ToString() + " hours";
                }
            }

            level.text = "Level: " + PlayerPrefs.GetInt("Level").ToString();
        }

        public void PlayButtonSound()
        {
            FindObjectOfType<AudioManager>().Play("ButtonClick");
        }
    }

}
