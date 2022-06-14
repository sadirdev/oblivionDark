using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;



public class EdMob : MonoBehaviour
{

    private static string _testId = "";
    // Переменная межстраничной рекламы

    private  static InterstitialAd _betAds;
    // Переменная полной рекламы
    private static RewardedAd _fullAds;
    private static AdRequest _request;
    private static BannerView _bannerView;

    const string adUnitIdBanner = "ca-app-pub-4369820251030088/3682713617";
    const string adUnitIdFull = "ca-app-pub-4369820251030088/3114069101";
    const string adUnitIdBet = "ca-app-pub-4369820251030088/8655289959";

    void Start()
    {


        _betAds = new InterstitialAd(adUnitIdBet);
        _fullAds = new RewardedAd(adUnitIdFull);
        _request = new AdRequest.Builder().Build();
        _betAds.LoadAd(_request);
        _fullAds.LoadAd(_request);


    }

    public static void ShowBanner()
    {
        if(_bannerView == null) _bannerView = new BannerView(adUnitIdBanner, AdSize.Banner, AdPosition.Bottom);
        AdRequest adRequest = new AdRequest.Builder().Build();
        _bannerView.LoadAd(adRequest);

        _bannerView.Show();
    }
    public static void HideBanner()
    {
        if(_bannerView !=null) _bannerView.Hide();
    }



    public static bool ShowAdsBet() // межстраничная
    {
        
        if (Application.internetReachability == NetworkReachability.NotReachable) return false;
        else
        {
            if (_betAds.IsLoaded())
            {
                _betAds.Show();
                Debug.Log("Межстраничную Показали");
            } 
            _request = new AdRequest.Builder().Build();
            _betAds.LoadAd(_request);
            return true;
        }
    }
    public static bool ShowAdsFull() // Полная
    {
        
        if (Application.internetReachability == NetworkReachability.NotReachable) return false;
        else
        {
            if (_fullAds.IsLoaded()) 
            {
                Debug.Log("Полная Показали");
                _fullAds.Show();
            } 
            _request = new AdRequest.Builder().Build();
            _fullAds.LoadAd(_request);
            return true;
        }
    }
   


   
}

