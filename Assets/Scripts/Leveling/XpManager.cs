using UnityEngine;

namespace Scripts.Leveling
{
    public class XpManager : MonoBehaviour
    {
        [SerializeField] private LevelWindow levelWindow;

        private void Awake()
        {
            LevelSystem levelSystem = new LevelSystem();
            levelWindow.SetLevelSystem(levelSystem);

            LevelSystemAnimated levelSystemAnimated = new LevelSystemAnimated(levelSystem);
            levelWindow.SetLevelSystemAnimated(levelSystemAnimated);
        }
    }
}
