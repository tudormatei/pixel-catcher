using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;

public class ADSManager : MonoBehaviour, IUnityAdsListener
{
    [SerializeField] private GameManager gm;
    [SerializeField] private GameObject watchAdButton;

    private void Start()
    {
        Advertisement.AddListener(this);
        Advertisement.Initialize("4224627");
    }

    private void Update()
    {
        if (PlayerPrefs.GetInt("RunAD") == 0)
        {
            watchAdButton.SetActive(true);
        }
        else
        {
            watchAdButton.SetActive(false);
        }
    }

    public void PlayRewardedAD()
    {
        if (Advertisement.IsReady("Rewarded_Android"))
        {
            Advertisement.Show("Rewarded_Android");
        }
        else
        {
            //Debug.Log("Rewarded ad is not ready.");
        }
    }

    public void OnUnityAdsReady(string placementId)
    {
        //Debug.Log("Ads are ready.");
    }

    public void OnUnityAdsDidError(string message)
    {
        //Debug.Log("Error" + message);
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        //Debug.Log("Video started.");
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if(placementId == "Rewarded_Android" && showResult == ShowResult.Finished)
        {
            //Debug.Log("Succesfuly finished add. Reward!");
            PlayerPrefs.SetInt("LastScore", gm.score);
            PlayerPrefs.SetInt("LastMultiplier", gm.currentMultiplier);

            PlayerPrefs.SetInt("RunAD", 1);
            //PlayerPrefs.SetInt("XP", PlayerPrefs.GetInt("XP", 0) - gm.score);

            SceneManager.LoadScene("MainGame");
        }
    }
}
