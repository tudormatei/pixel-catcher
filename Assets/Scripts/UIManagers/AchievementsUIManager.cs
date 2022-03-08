using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class AchievementsUIManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> achivements;
    private List<Image> achievementsImageComp;
    private bool[] completed;
    [SerializeField] private List<Sprite> achivementUnlocked;

    [SerializeField] private GameObject postProcessing;

    [SerializeField] private TextMeshProUGUI completition;

    void Start()
    {
        if(PlayerPrefs.GetInt("Graphics", 0) == 0)
        {
            postProcessing.SetActive(true);
        }
        else
        {
            postProcessing.SetActive(false);
        }

        achievementsImageComp = new List<Image>();
        completed = new bool[achivements.Count];

        LoadAchievements();

        CheckCompletition();
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void CheckCompletition()
    {
        int index = 0;
        foreach(bool complete in completed)
        {
            if (complete)
                index++;
        }

        int total = achivements.Count;

        completition.text = index.ToString() + "/" + total.ToString() + " Completed";
    }

    private void LoadAchievements()
    {
        foreach(GameObject achievement in achivements)
        {
            Image image = achievement.GetComponent<Image>();
            achievementsImageComp.Add(image);
        }

        CheckAchivement1();
        CheckAchivement2();
        CheckAchivement3();
        CheckAchivement4();
        CheckAchivement5();
        CheckAchivement6();
        CheckAchivement7();
        CheckAchivement8();
        CheckAchivement9();
        CheckAchivement10();

        LoadOrder();

        foreach (bool complet in completed)
        {
            if (!complet)
            {
                Debug.Log("Not all achievements are completed.");
                PlayerPrefs.SetInt("AllAchievements", 0);
                return;
            }
        }

        PlayerPrefs.SetInt("AllAchievements", 1);
    }

    private void LoadOrder()
    {
        int index = 0;
        for(int i = 0;i < completed.Length; i++)
        {
            if (completed[i])
            {
                achivements[i].transform.SetSiblingIndex(index);
                index++;
            }
        }
    }

    private void CheckAchivement1()
    {
        UnlockAchievement(achievementsImageComp[0], achivementUnlocked[0]);
        completed[0] = true;
    }
    private void CheckAchivement2()
    {
        if(PlayerPrefs.GetInt("Highscore") >= 1)
        {
            UnlockAchievement(achievementsImageComp[1], achivementUnlocked[1]);
            completed[1] = true;
        }
        else
        {
            completed[1] = false;
        }
    }
    private void CheckAchivement3()
    {
        if (PlayerPrefs.GetInt("Points") >= 500)
        {
            UnlockAchievement(achievementsImageComp[2], achivementUnlocked[2]);
            completed[2] = true;
        }
        else
        {
            completed[2] = false;
        }
    }
    private void CheckAchivement4()
    {
        if (PlayerPrefs.GetInt("Points") >= 1500)
        {
            UnlockAchievement(achievementsImageComp[3], achivementUnlocked[3]);
            completed[3] = true;
        }
        else
        {
            completed[3] = false;
        }
    }
    private void CheckAchivement5()
    {
        if (PlayerPrefs.GetInt("Deaths") >= 10)
        {
            UnlockAchievement(achievementsImageComp[4], achivementUnlocked[4]);
            completed[4] = true;
        }
        else
        {
            completed[4] = false;
        }
    }
    private void CheckAchivement6()
    {
        if (PlayerPrefs.GetInt("Deaths") >= 50)
        {
            UnlockAchievement(achievementsImageComp[5], achivementUnlocked[5]);
            completed[5] = true;
        }
        else
        {
            completed[5] = false;
        }
    }
    private void CheckAchivement7()
    {
        if ((int)PlayerPrefs.GetFloat("PlayTime") / 60 >= 5)
        {
            UnlockAchievement(achievementsImageComp[6], achivementUnlocked[6]);
            completed[6] = true;
        }
        else
        {
            completed[6] = false;
        }
    }
    private void CheckAchivement8()
    {
        if ((int)PlayerPrefs.GetFloat("PlayTime") / 60 >= 20)
        {
            UnlockAchievement(achievementsImageComp[7], achivementUnlocked[7]);
            completed[7] = true;
        }
        else
        {
            completed[7] = false;
        }
    }
    private void CheckAchivement9()
    {
        if (PlayerPrefs.GetInt("Combo") >= 4)
        {
            UnlockAchievement(achievementsImageComp[8], achivementUnlocked[8]);
            completed[8] = true;
        }
        else
        {
            completed[8] = false;
        }
    }
    private void CheckAchivement10()
    {
        if (PlayerPrefs.GetInt("Shields") >= 10)
        {
            UnlockAchievement(achievementsImageComp[9], achivementUnlocked[9]);
            completed[9] = true;
        }
        else
        {
            completed[9] = false;
        }
    }

    private void UnlockAchievement(Image image, Sprite sprite)
    {
        image.sprite = sprite;
    }

    public void PlayButtonSound()
    {
        FindObjectOfType<AudioManager>().Play("ButtonClick");
    }
}
