using UnityEngine;
using CodeMonkey.Utils;
using System;

namespace Scripts.Leveling
{
    public class LevelSystemAnimated
    {
        public event EventHandler OnExperienceChanged;
        public event EventHandler OnLevelChanged;

        private LevelSystem levelSystem;

        private bool isAnimated;
        private float updateTimer;
        private float updateTimerMax;

        private int level;
        private int experience;

        public LevelSystemAnimated(LevelSystem levelSystem)
        {
            SetLevelSystem(levelSystem);
            updateTimerMax = .003f;

            FunctionUpdater.Create(() => Update());
        }

        public void SetLevelSystem(LevelSystem levelSystem)
        {
            this.levelSystem = levelSystem;

            level = levelSystem.GetLevel();
            experience = levelSystem.GetExperience();

            levelSystem.OnExperienceChanged += LevelSystem_OnExperienceChanged;
            levelSystem.OnLevelChanged += LevelSystem_OnLevelChanged;
        }

        private void LevelSystem_OnExperienceChanged(object sender, System.EventArgs e)
        {
            isAnimated = true;
        }

        private void LevelSystem_OnLevelChanged(object sender, System.EventArgs e)
        {
            isAnimated = true;
        }

        private void Update()
        {
            if (isAnimated)
            {
                updateTimer += Time.deltaTime;
                while (updateTimer > updateTimerMax)
                {
                    updateTimer -= updateTimerMax;

                    if (level < levelSystem.GetLevel())
                    {
                        AddExperience();
                    }
                    else
                    {
                        if (experience < levelSystem.GetExperience())
                        {
                            AddExperience();
                        }
                        else
                        {
                            isAnimated = false;
                        }
                    }
                }
            }
        }

        private void AddExperience()
        {
            experience++;
            if (experience >= levelSystem.GetExperienceToNextLevel(level))
            {
                level++;
                experience = 0;
                if (OnLevelChanged != null) OnLevelChanged(this, EventArgs.Empty);
            }

            if (OnExperienceChanged != null) OnExperienceChanged(this, EventArgs.Empty);
        }

        public int GetLevel()
        {
            return level;
        }

        public int GetExperience()
        {
            return experience;
        }

        public float GetExperienceNormalized()
        {
            if (levelSystem.IsMaxLevel(level))
            {
                return 1f;
            }
            else
            {
                return (float)experience / levelSystem.GetExperienceToNextLevel(level);
            }
        }

    }

}
