using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Scripts.Audio;

namespace Scripts.UI
{
    public class CollectionsUIManager : MonoBehaviour
    {
        [SerializeField] private List<Image> skins;

        private GameObject skin;

        public bool[] selections;

        [SerializeField] private List<Image> buttonSkins;

        public Color unlockedColor = new Color(255f, 255f, 255f, 255f);
        public Color lockedColor = new Color(140f, 140f, 140f, 255f);

        public Color selected = new Color(255, 255, 255, 255f);
        public Color unSelected = new Color(154f, 154f, 154f, 0f);

        [SerializeField] private GameObject postProcessing;

        public void BackToMainMenu()
        {
            for (int i = 0; i < selections.Length; i++)
            {
                if (selections[i])
                {
                    skin = skins[i].gameObject;
                    PlayerPrefs.SetInt("SkinIndex", i);
                }
            }

            SceneManager.LoadScene("MainMenu");
        }

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

            selections = new bool[buttonSkins.Count];

            for (int i = 0; i < selections.Length; i++)
            {
                selections[i] = false;
                buttonSkins[i].color = unSelected;
            }

            SelectPreviousSkin();
        }

        private void Update()
        {
            CheckUnlockedSkins();
        }

        private void SelectPreviousSkin()
        {
            for (int i = 0; i < skins.Count; i++)
            {
                if (i == PlayerPrefs.GetInt("SkinIndex", 0))
                {
                    selections[i] = true;
                    buttonSkins[i].color = selected;
                }
            }
        }

        public void SelectSkin(int index)
        {
            for (int i = 0; i < selections.Length; i++)
            {
                if (skins[index].color == unlockedColor)
                {
                    if (i == index)
                    {
                        selections[i] = true;
                        buttonSkins[i].color = selected;
                    }
                    else
                    {
                        selections[i] = false;
                        buttonSkins[i].color = unSelected;
                    }
                }
            }
        }

        public void CheckUnlockedSkins()
        {
            CheckSkin1();
            CheckSkin2();
            CheckSkin3();
            CheckSkin4();
            CheckSkin5();
            CheckSkin6();
            CheckSkin7();
            CheckSkin8();
            CheckSkin9();
            CheckSkin10();
            CheckSkin11();
            CheckSkin12();
            CheckSkin13();
            CheckSkin14();
            CheckSkin15();
            CheckSkin16();
            CheckSkin17();
            CheckSkin18();
            CheckSkin19();
            CheckSkin20();
            CheckSkin21();
        }

        private void CheckSkin1()
        {
            skins[0].color = unlockedColor;
        }

        private bool CheckSkin2()
        {
            if (PlayerPrefs.GetInt("Level", 0) >= 2)
            {
                skins[1].color = unlockedColor;
                return true;
            }
            else
            {
                skins[1].color = lockedColor;
                return false;
            }
        }

        private bool CheckSkin3()
        {
            if (PlayerPrefs.GetInt("Level", 0) >= 2)
            {
                skins[2].color = unlockedColor;
                return true;
            }
            else
            {
                skins[2].color = lockedColor;
                return false;
            }
        }

        private bool CheckSkin4()
        {
            if (PlayerPrefs.GetInt("Level", 0) >= 2)
            {
                skins[3].color = unlockedColor;
                return true;
            }
            else
            {
                skins[3].color = lockedColor;
                return false;
            }
        }

        private bool CheckSkin5()
        {
            if (PlayerPrefs.GetInt("Level", 0) >= 2)
            {
                skins[4].color = unlockedColor;
                return true;
            }
            else
            {
                skins[4].color = lockedColor;
                return false;
            }
        }

        private bool CheckSkin6()
        {
            if (PlayerPrefs.GetInt("Level", 0) >= 2)
            {
                skins[5].color = unlockedColor;
                return true;
            }
            else
            {
                skins[5].color = lockedColor;
                return false;
            }
        }

        private bool CheckSkin7()
        {
            if (PlayerPrefs.GetInt("Level", 0) >= 2)
            {
                skins[6].color = unlockedColor;
                return true;
            }
            else
            {
                skins[6].color = lockedColor;
                return false;
            }
        }

        private bool CheckSkin8()
        {
            if (PlayerPrefs.GetInt("Level", 0) >= 2)
            {
                skins[7].color = unlockedColor;
                return true;
            }
            else
            {
                skins[7].color = lockedColor;
                return false;
            }
        }

        private bool CheckSkin9()
        {
            if (PlayerPrefs.GetInt("Level", 0) >= 3)
            {
                skins[8].color = unlockedColor;
                return true;
            }
            else
            {
                skins[8].color = lockedColor;
                return false;
            }
        }

        private bool CheckSkin10()
        {
            if (PlayerPrefs.GetInt("Level", 0) >= 3)
            {
                skins[9].color = unlockedColor;
                return true;
            }
            else
            {
                skins[9].color = lockedColor;
                return false;
            }
        }

        private bool CheckSkin11()
        {
            if (PlayerPrefs.GetInt("Level", 0) >= 3)
            {
                skins[10].color = unlockedColor;
                return true;
            }
            else
            {
                skins[10].color = lockedColor;
                return false;
            }
        }

        private bool CheckSkin12()
        {
            if (PlayerPrefs.GetInt("Level", 0) >= 3)
            {
                skins[11].color = unlockedColor;
                return true;
            }
            else
            {
                skins[11].color = lockedColor;
                return false;
            }
        }

        private bool CheckSkin13()
        {
            if (PlayerPrefs.GetInt("Level", 0) >= 4)
            {
                skins[12].color = unlockedColor;
                return true;
            }
            else
            {
                skins[12].color = lockedColor;
                return false;
            }
        }

        private bool CheckSkin14()
        {
            if (PlayerPrefs.GetInt("Level", 0) >= 4)
            {
                skins[13].color = unlockedColor;
                return true;
            }
            else
            {
                skins[13].color = lockedColor;
                return false;
            }
        }

        private bool CheckSkin15()
        {
            if (PlayerPrefs.GetInt("Level", 0) >= 4)
            {
                skins[14].color = unlockedColor;
                return true;
            }
            else
            {
                skins[14].color = lockedColor;
                return false;
            }
        }

        private bool CheckSkin16()
        {
            if (PlayerPrefs.GetInt("Level", 0) >= 4)
            {
                skins[15].color = unlockedColor;
                return true;
            }
            else
            {
                skins[15].color = lockedColor;
                return false;
            }
        }

        private bool CheckSkin17()
        {
            if (PlayerPrefs.GetInt("Level", 0) >= 5)
            {
                skins[16].color = unlockedColor;
                return true;
            }
            else
            {
                skins[16].color = lockedColor;
                return false;
            }
        }

        private bool CheckSkin18()
        {
            if (PlayerPrefs.GetInt("Level", 0) >= 5)
            {
                skins[17].color = unlockedColor;
                return true;
            }
            else
            {
                skins[17].color = lockedColor;
                return false;
            }
        }

        private bool CheckSkin19()
        {
            if (PlayerPrefs.GetInt("Level", 0) >= 6)
            {
                skins[18].color = unlockedColor;
                return true;
            }
            else
            {
                skins[18].color = lockedColor;
                return false;
            }
        }

        private bool CheckSkin20()
        {
            if (PlayerPrefs.GetInt("Level", 0) >= 6)
            {
                skins[19].color = unlockedColor;
                return true;
            }
            else
            {
                skins[19].color = lockedColor;
                return false;
            }
        }

        private bool CheckSkin21()
        {
            if (PlayerPrefs.GetInt("AllAchievements", 0) == 1)
            {
                skins[20].color = unlockedColor;
                return true;
            }
            else
            {
                skins[20].color = lockedColor;
                return false;
            }
        }

        public void PlayButtonSound()
        {
            FindObjectOfType<AudioManager>().Play("ButtonClick");
        }
    }

}
