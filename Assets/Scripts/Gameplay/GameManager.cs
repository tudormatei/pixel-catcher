using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using EZCameraShake;
using Scripts.Audio;

namespace Scripts.Gameplay
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private PostProcessingManager postProcessing;

        [SerializeField] private GameObject scoreObject;
        [SerializeField] private GameObject multiplierObject;
        [SerializeField] private GameObject shieldCooldownObject;

        [SerializeField] private Animator scoreAnimator;

        [SerializeField] private GameObject shield;
        [SerializeField] private float shieldDuration = 5f;
        [SerializeField] private Image shieldCooldown;
        [SerializeField] private Image shieldCooldownBorder;

        [SerializeField] private GameObject pauseButton;

        [SerializeField] private ParticleSystem pointExplosion;
        [SerializeField] private ParticleSystem platformExplosion;
        [SerializeField] private ParticleSystem spikeExposion;
        [SerializeField] private ParticleSystem comboAdderExplosion;
        [SerializeField] private ParticleSystem comboRemoverExplosion;
        [SerializeField] private ParticleSystem shieldExplosion;

        [SerializeField] private List<GameObject> spawners;

        [SerializeField] private GameObject pointText;
        [SerializeField] private GameObject poinTextParent;

        private TextMeshProUGUI scoreUI;
        private TextMeshProUGUI multiplierUI;

        public int score = 0;
        public int currentMultiplier = 1;

        private Color[] colors = { new Color(255, 255, 255, 255), new Color(255f, 0f, 0f, 255f), new Color(0f, 255f, 0f, 255f), new Color(0f, 0f, 255f, 255f) };

        private bool alive = true;
        private float playTime = 0f;

        private AudioManager audioManager;

        [SerializeField] private GameObject mainMenu;
        [SerializeField] private TextMeshProUGUI highscore;

        private void Start()
        {
            if (PlayerPrefs.GetInt("LastScore", 0) != 0)
            {
                score = PlayerPrefs.GetInt("LastScore", 0);
                currentMultiplier = PlayerPrefs.GetInt("LastMultiplier", 1);

                PlayerPrefs.SetInt("XP", PlayerPrefs.GetInt("XP", 0) - score);

                PlayerPrefs.DeleteKey("LastMultiplier");
                PlayerPrefs.DeleteKey("LastScore");
            }
            else
            {
                PlayerPrefs.SetInt("RunAD", 0);
                score = 0;
                currentMultiplier = 1;
            }

            pauseButton.GetComponent<Button>().interactable = true;
            alive = true;
            playTime = 0f;

            Time.timeScale = 1f;

            mainMenu.SetActive(false);
            shield.SetActive(false);

            shieldCooldown.fillAmount = 1f;

            scoreUI = scoreObject.GetComponent<TextMeshProUGUI>();
            multiplierUI = multiplierObject.GetComponent<TextMeshProUGUI>();
        }

        private void Update()
        {
            UpdateMultiplierColor();

            Stats();
        }

        private void Stats()
        {
            if (currentMultiplier > PlayerPrefs.GetInt("Combo", 0))
            {
                PlayerPrefs.SetInt("Combo", currentMultiplier);
            }

            if (alive)
            {
                playTime += Time.deltaTime;
            }
        }

        private void UpdateMultiplierColor()
        {
            scoreUI.text = score.ToString();

            multiplierUI.text = "x" + currentMultiplier.ToString();

            switch (currentMultiplier)
            {
                case 1:
                    multiplierUI.color = colors[0];
                    break;
                case 2:
                    multiplierUI.color = colors[2];
                    break;
                case 3:
                    multiplierUI.color = colors[3];
                    break;
                default:
                    multiplierUI.color = colors[1];
                    break;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            string tag = collision.transform.tag;
            if (tag == "Point" || (shield.activeSelf && tag == "Point"))
            {
                audioManager = FindObjectOfType<AudioManager>();
                audioManager.Play("Point");

                pointExplosion.transform.position = new Vector3(collision.transform.position.x, transform.position.y + 1f, -3f);
                pointExplosion.Play();

                GameObject go = Instantiate(pointText, new Vector3(collision.transform.position.x, collision.transform.position.y + 1f, collision.transform.position.z), Quaternion.identity);
                if (go != null)
                {
                    go.GetComponent<TextMesh>().text = "+" + currentMultiplier.ToString();
                    go.transform.SetParent(poinTextParent.transform);
                    StartCoroutine(LerpTextColor(new Color(1, 1, 1, 0), 1, go));
                }

                score += currentMultiplier;
                scoreAnimator.SetTrigger("increaseScore");

                PlayerPrefs.SetInt("Points", PlayerPrefs.GetInt("Points", 0) + 1);

                Destroy(collision.gameObject);
            }
            else if (tag == "Spike")
            {
                PlayerPrefs.SetInt("Spikes", PlayerPrefs.GetInt("Spikes", 0) + 1);

                Die();

                if (shield.activeSelf)
                {
                    spikeExposion.transform.position = new Vector3(collision.transform.position.x, transform.position.y + 1f, -3f);
                    spikeExposion.Play();

                    audioManager = FindObjectOfType<AudioManager>();
                    audioManager.Play("Spike");
                }
                else
                {
                    audioManager = FindObjectOfType<AudioManager>();
                    audioManager.Play("Death");

                    alive = false;

                    GainXP();

                    PlayerPrefs.SetInt("Deaths", PlayerPrefs.GetInt("Deaths", 0) + 1);

                    PlayerPrefs.SetFloat("PlayTime", PlayerPrefs.GetFloat("PlayTime") + playTime);

                    pauseButton.GetComponent<Button>().interactable = false;

                    platformExplosion.transform.position = new Vector3(collision.transform.position.x, transform.position.y + 1f, -3f);
                    platformExplosion.Play();
                }

                Destroy(collision.gameObject);
            }
            else if (tag == "Combo Adder")
            {
                audioManager = FindObjectOfType<AudioManager>();
                audioManager.Play("ComboAdd");

                comboAdderExplosion.transform.position = new Vector3(collision.transform.position.x, transform.position.y + 1f, -3f);
                comboAdderExplosion.Play();

                currentMultiplier++;

                Destroy(collision.gameObject);
            }
            else if (tag == "Combo Remover")
            {
                audioManager = FindObjectOfType<AudioManager>();
                audioManager.Play("ComboRemove");

                comboRemoverExplosion.transform.position = new Vector3(collision.transform.position.x, transform.position.y + 1f, -3f);
                comboRemoverExplosion.Play();

                if (currentMultiplier > 1)
                {
                    currentMultiplier--;
                }

                Destroy(collision.gameObject);
            }
            else if (tag == "Shield")
            {
                audioManager = FindObjectOfType<AudioManager>();
                audioManager.Play("Shield");

                postProcessing.ShieldPostProcessing();

                shieldExplosion.transform.position = new Vector3(collision.transform.position.x, transform.position.y + 1f, -3f);
                shieldExplosion.Play();

                PlayerPrefs.SetInt("Shields", PlayerPrefs.GetInt("Shields", 0) + 1);

                StartCoroutine(StartShieldTimer());

                Destroy(collision.gameObject);
            }
        }

        private void Die()
        {
            scoreUI.text = "Score: " + score.ToString();

            if (!shield.activeSelf)
            {
                CameraShaker.Instance.ShakeOnce(2f, 2f, .1f, 1f);

                Destroy(poinTextParent);

                if (score > PlayerPrefs.GetInt("Highscore", 0))
                {
                    PlayerPrefs.SetInt("Highscore", score);
                }

                LoadDeathScreen();

                Destroy(gameObject);
            }
        }

        private void GainXP()
        {
            PlayerPrefs.SetInt("LastXP", PlayerPrefs.GetInt("XP", 0));
            PlayerPrefs.SetInt("XP", PlayerPrefs.GetInt("XP", 0) + score);
        }

        private void LoadDeathScreen()
        {
            if (mainMenu != null)
            {
                multiplierUI.enabled = false;
                shieldCooldownBorder.enabled = false;
                shieldCooldown.enabled = false;

                highscore.text = "Highscore: " + PlayerPrefs.GetInt("Highscore", 0).ToString();

                mainMenu.SetActive(true);

                Time.timeScale = 0.1f;

                foreach (GameObject go in spawners)
                {
                    go.SetActive(false);
                }
            }
        }

        IEnumerator StartShieldTimer()
        {
            shieldCooldown.fillAmount = 1f;

            shield.SetActive(true);

            float time = shieldDuration;
            while (time > 0)
            {
                time -= Time.deltaTime;
                shieldCooldown.fillAmount -= 1 / shieldDuration * Time.deltaTime;
                yield return null;
            }

            shield.SetActive(false);

            shieldCooldown.fillAmount = 1f;
        }

        IEnumerator LerpTextColor(Color endValue, float duration, GameObject go)
        {
            float time = 0;
            TextMesh text = go.GetComponent<TextMesh>();
            Color startValue = text.color;

            while (time < duration)
            {
                text.color = Color.Lerp(startValue, endValue, time / duration);
                time += Time.deltaTime;
                yield return null;
            }
            text.color = endValue;
        }
    }

}
