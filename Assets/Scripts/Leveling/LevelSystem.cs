using System;

namespace Scripts.Leveling
{
    public class LevelSystem
    {
        public event EventHandler OnExperienceChanged;
        public event EventHandler OnLevelChanged;

        private static readonly int[] experiencePerLevel = new[] { 250, 500, 1000, 1750, 2750, 4000, 5500, 7250, 9000, 11250, 13750, 16500, 19500, 22750, 26250, 30000, 34000 };

        private int level;
        private int experience;

        public LevelSystem()
        {
            level = 0;
            experience = 0;
        }

        public void AddExperience(int amout)
        {
            if (!IsMaxLevel())
            {
                experience += amout;
                while (!IsMaxLevel() && experience >= GetExperienceToNextLevel(level))
                {
                    experience -= GetExperienceToNextLevel(level);
                    level++;

                    if (OnLevelChanged != null) OnLevelChanged(this, EventArgs.Empty);
                }

                if (OnExperienceChanged != null) OnExperienceChanged(this, EventArgs.Empty);
            }

        }

        public int GetLevel()
        {
            return level;
        }

        public int GetExperience()
        {
            return experience;
        }

        public int GetExperienceToNextLevel(int level)
        {
            if (level < experiencePerLevel.Length)
            {
                return experiencePerLevel[level];
            }
            else
            {
                return 100;
            }
        }

        public bool IsMaxLevel()
        {
            return IsMaxLevel(level);
        }

        public bool IsMaxLevel(int level)
        {
            return level == experiencePerLevel.Length - 1;
        }

        public float GetExperienceNormalized()
        {
            if (IsMaxLevel())
            {
                return 1f;
            }
            else
            {
                return (float)experience / GetExperienceToNextLevel(level);
            }
        }
    }
}
