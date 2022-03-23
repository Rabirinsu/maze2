using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
public class UnityAds : MonoBehaviour, IUnityAdsListener
{
    private string gameID = "4534277";
    private string bannerID = "Banner";
    private string interstitialID = "Level_Start";
    private string rewardedVideoID = "Rewarded";
    private bool TestMode;
    public Button showInterstitial;
    public Button watchRewardAd;
  
    void Awake()
    {
        TestMode = false;
    }
    void Start()
    {
   
        Advertisement.Initialize(gameID, TestMode);
        Advertisement.AddListener(this);
        showInterstitial.interactable = Advertisement.IsReady(interstitialID);
        watchRewardAd.interactable = Advertisement.IsReady(rewardedVideoID);
        ShowBanner();
    }

    public void ShowInterstitial()
    {
        if (Advertisement.IsReady(interstitialID))
        {
            Advertisement.Show(interstitialID);
        }
    }

    public void ShowRewardedVideo()
    {
        Advertisement.Show(rewardedVideoID);
    }

  public void ShowBanner()
    {
        if (Advertisement.IsReady(bannerID))
        {
            Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
            Advertisement.Banner.Show(bannerID);
        }
        else StartCoroutine(nameof(RepeatShowBanner));
    }    

    IEnumerator RepeatShowBanner()
    {
        yield return new WaitForSeconds(1);
        ShowBanner();
    }

    public void OnUnityAdsReady(string placementID)
    {
        if (placementID == rewardedVideoID)
        {
            watchRewardAd.interactable = true;
        }

        if (placementID == interstitialID)
        {
            showInterstitial.interactable = true;
        }

      /*  if (placementID == bannerID)
        {
            Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
            Advertisement.Banner.Show(bannerID);
        }*/
    }

    public void OnUnityAdsDidFinish(string placementID, ShowResult showResult)
    {
        if (placementID == rewardedVideoID)
        {
 
             if (showResult == ShowResult.Skipped)
            {
                //Do nothing
            }
            else if (showResult == ShowResult.Failed)
            {
                //tell player ads failed
            }
        }
    }


    public void OnUnityAdsDidError(string message)
    {
        //Show or log the error here
    }

    public void OnUnityAdsDidStart(string placementID)
    {
        //Do this if ads starts
    }

}
