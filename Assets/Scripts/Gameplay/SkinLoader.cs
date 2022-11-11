using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Gameplay
{
    public class SkinLoader : MonoBehaviour
    {
        [SerializeField] private GameObject platform;

        [SerializeField] private List<Sprite> skins;

        private void Start()
        {
            LoadSkin();
            //Debug.Log(PlayerPrefs.GetInt("SkinIndex", 0));
        }

        private void LoadSkin()
        {
            int index = PlayerPrefs.GetInt("SkinIndex", 0);
            platform.GetComponent<SpriteRenderer>().sprite = skins[index];
        }
    }
}
