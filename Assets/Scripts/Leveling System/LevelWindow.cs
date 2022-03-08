using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelWindow : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI frontLevel;
    [SerializeField] private TextMeshProUGUI backLevel;
    [SerializeField] private TextMeshProUGUI xpRemaining;

    [SerializeField] private Slider experienceBar;

    private LevelSystem levelSystem;
    private LevelSystemAnimated levelSystemAnimated;

    private int level;

    private void Start()
    {
        experienceBar.interactable = false;

        int xp = PlayerPrefs.GetInt("XP", 0);
        levelSystem.AddExperience(xp);
    }

    private void Update()
    {
        xpRemaining.text = "XP Remaining: " + (levelSystem.GetExperienceToNextLevel(level) - levelSystemAnimated.GetExperience()).ToString();
    }

    private void SetExperienceBarSize(float experienceNormalized)
    {
        experienceBar.value = experienceNormalized;
    }

    private void SetLevelNumber(int level)
    {
        this.level = level;
        backLevel.text = (level + 1).ToString();
        frontLevel.text = (level + 2).ToString();

        if (PlayerPrefs.GetInt("Level", 0) < level + 1)
        {
            PlayerPrefs.SetInt("Level", level + 1);
        }
    }

    public void SetLevelSystemAnimated(LevelSystemAnimated levelSystemAnimated)
    {
        this.levelSystemAnimated = levelSystemAnimated;

        SetLevelNumber(levelSystemAnimated.GetLevel());
        SetExperienceBarSize(levelSystemAnimated.GetExperienceNormalized());

        levelSystemAnimated.OnExperienceChanged += LevelSystemAnimated_OnExperienceChanged;
        levelSystemAnimated.OnLevelChanged += LevelSystemAnimated_OnLevelChanged;
    }

    public void SetLevelSystem(LevelSystem levelSystem)
    {
        this.levelSystem = levelSystem;
    }

    private void LevelSystemAnimated_OnExperienceChanged(object sender, System.EventArgs e)
    {
        SetExperienceBarSize(levelSystemAnimated.GetExperienceNormalized());
    }

    private void LevelSystemAnimated_OnLevelChanged(object sender, System.EventArgs e)
    {
        SetLevelNumber(levelSystemAnimated.GetLevel());
    }
}
