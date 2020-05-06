using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using GoogleMobileAds.Api;
using UnityEngine.UI;

public class AdManager : MonoBehaviour
{
    public static AdManager instance;

    public Button btn;
    public AudioSource boton;

    private string AppAndroidId = "ca-app-pub-1206908647281954~5290435062";
    private string AppIOSId     = "ca-app-pub-1206908647281954~1323269897";

    private BannerView bannerView;
   

    private RewardBasedVideoAd rewardedAd;
    

    public bool PanelReward;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(this);
    }

    private void Start()
    {
        
        #if UNITY_ANDROID
            string appId = "ca-app-pub-1206908647281954~5290435062";
        #elif UNITY_IPHONE
            string appId = "ca-app-pub-1206908647281954~1323269897";
        #else
            string appId = "unexpected_platform";
        #endif


        MobileAds.Initialize(appId);

        //RequestBanner();

        PanelReward = false;

        rewardedAd = RewardBasedVideoAd.Instance;

        RequestRewardedAd();

        rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;

        rewardedAd.OnAdRewarded += HandleUserEarnedReward;

        rewardedAd.OnAdClosed += HandleRewardedAdClosed;
        
    }
    private void Update()
    {
        if (PanelReward)
        {

            GameController.instance.RewardPanel.SetActive(true);
            
        }
    }
    public void RequestBanner()
    {
       

        #if UNITY_ANDROID
                     string bannerID = "ca-app-pub-1206908647281954/3293761457";
        #elif UNITY_IPHONE
                     string bannerID = "ca-app-pub-1206908647281954/4023361527";
        #else
                     string bannerID = "unexpected_platform";
        #endif

        bannerView = new BannerView(bannerID, AdSize.Banner, AdPosition.Bottom);

        AdRequest request = new AdRequest.Builder().Build();

        bannerView.LoadAd(request);

        bannerView.Show();
        
    }
    public void HideBanner()
    {
        bannerView.Hide();
    }

    public void RequestRewardedAd()
    {
        

        #if UNITY_ANDROID
             string rewardedAdId = "ca-app-pub-1206908647281954/9069187532";
        #elif UNITY_IPHONE
             string rewardedAdId = "ca-app-pub-1206908647281954/6677445028";
        #else
             string rewardedAdId = "unexpected_platform";
        #endif

        AdRequest request = new AdRequest.Builder().Build();

        rewardedAd.LoadAd(request, rewardedAdId);
    }

    public void ShowRewardedAd()
    {
        StartCoroutine(showRwAd());
    }
    IEnumerator showRwAd()
    {
        
        boton.Play();
        //btn.interactable = !btn.interactable;
        yield return new WaitWhile(() => boton.isPlaying);
        if (rewardedAd.IsLoaded())
        {
            rewardedAd.Show();
        }
        else
        {
            Debug.Log("rewarded ad not loaded");
        }
    }
    
    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdLoaded event received");
    }

    
    public void HandleUserEarnedReward(object sender, Reward args)
    {
        string type = args.Type;
        double amount = args.Amount;
        MonoBehaviour.print(
            "HandleRewardedAdRewarded event received for "
                        + amount.ToString() + " " + type);

        //GameController.instance.RewardPanel.SetActive(true);
        PanelReward = true ;
    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdClosed event received");
        RequestRewardedAd();
    }

}
