using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
