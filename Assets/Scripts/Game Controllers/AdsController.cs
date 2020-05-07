using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using ChartboostSDK;

public class AdsController : MonoBehaviour {

    //[SerializeField]
    //private Text logText;

    public static AdsController instance;

    public bool canShowChartboostInterstitial, canShowChartboostVideo;

    private bool canShowAd;

    void Awake()
    {
        MakeSingleton();
    }

    void OnEnable()
    {
        ////logText.text += "started ";

        Chartboost.didCompleteRewardedVideo += VideoCompleted;
        Chartboost.didCacheInterstitial += DidCacheInterstitial;
        Chartboost.didDismissInterstitial += DidDismissInterstitial;
        Chartboost.didCloseInterstitial += DidCloseInterstitial;
        Chartboost.didCacheRewardedVideo += DidCacheVideo;
        Chartboost.didFailToLoadInterstitial += FailToLoadInterstitial;
        Chartboost.didFailToLoadRewardedVideo += FailToLoadVideo;

        ////logText.text += "done ";

        Initialize();
        
        //Chartboost.didClickInterstitial += DidClickInterstitial;
        //Chartboost.didCacheInterstitial += DidCacheInterstitial;
    }


    void OnDisable() {

        Chartboost.didCompleteRewardedVideo -= VideoCompleted;
        Chartboost.didCacheInterstitial -= DidCacheInterstitial;
        Chartboost.didDismissInterstitial -= DidDismissInterstitial;
        Chartboost.didCloseInterstitial -= DidCloseInterstitial;
        Chartboost.didCacheRewardedVideo -= DidCacheVideo;
        Chartboost.didFailToLoadInterstitial -= FailToLoadInterstitial;
        Chartboost.didFailToLoadRewardedVideo -= FailToLoadVideo;

    }

    void Start()
    {
        Initialize();
    }

    void MakeSingleton() {
        if (instance != null) {
            Destroy(gameObject);
        } else {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }


    void OnLevelWasLoaded() {
        //logText.text += "level_loaded ";
        /*
        if( SceneManager.GetActiveScene().name== "Main Menu Scene")
        {
            //logText.text += "Main_Menu_Scene ";

            if (canShowChartboostInterstitial)
            {
                //logText.text += "can_Interstitial ";

                ShowChartboostInterstitial();
            }
            else
            {
                //logText.text += "can_Not_Interstitial ";

                LoadChartboostInterstitialAds();
            }
        }
        //logText.text += "level_loaded_Done ";
        */
    }

    public void Initialize()
    {
        //print("INITIALIZE CHARTBOOST AD");
        if (!canShowChartboostInterstitial)
        {
            LoadChartboostInterstitialAds();
        }

        if (!canShowChartboostVideo)
        {
            LoadChartboostVideoAds();
        }
    }

    public void VideoCompleted(CBLocation location, int reward)
    {
        canShowChartboostVideo = false;
        //logText.text = reward.ToString();
        StoreController.instance.Buy3LivesFromStore();
        LoadChartboostVideoAds();
    }
    public void DidCacheInterstitial(CBLocation location)
    {
        canShowChartboostInterstitial = true;
    }
    void DidDismissInterstitial(CBLocation location)
    {
        canShowChartboostInterstitial = false;
        LoadChartboostInterstitialAds();
    }
    void DidCloseInterstitial(CBLocation location)
    {
        canShowChartboostInterstitial = false;
        LoadChartboostInterstitialAds();
    }

    public void DidCacheVideo(CBLocation location)
    {
        canShowChartboostVideo = true;
    }

    void FailToLoadInterstitial(CBLocation location, CBImpressionError error)
    {
        canShowChartboostInterstitial = false;
        LoadChartboostInterstitialAds();
    }

    void FailToLoadVideo(CBLocation location, CBImpressionError error)
    {
        canShowChartboostVideo = false;
        LoadChartboostVideoAds();
    }


    public void LoadChartboostVideoAds()
    {
        Chartboost.cacheRewardedVideo(CBLocation.Default);
    }

    public void LoadChartboostInterstitialAds()
    {
        Chartboost.cacheInterstitial(CBLocation.Default);
    }
    public void ShowChartboostInterstitial()
    {
        print("INTERESTIAL");
        if (canShowChartboostInterstitial)
        {
            Chartboost.showInterstitial(CBLocation.Default);

        }
        else
        {
            LoadChartboostInterstitialAds();
        }
    }

    public void ShowChartboostVideo()
    {
        if (canShowChartboostVideo)
        {
            Chartboost.showRewardedVideo(CBLocation.Default);
        }
        else
        {
            LoadChartboostVideoAds();
        }
    }


   

} // ads controller
