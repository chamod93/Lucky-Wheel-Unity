using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using UnityEngine.UI;
using System;

public class AdmobUtils : MonoBehaviour
{
    private BannerView bannerView;
    private InterstitialAd interstitial;
    public Text text;
    private NativeExpressAdView nativeExpressAdView;

    // Use this for initialization
    void Start()
    {
        RequestBanner();
        RequestInterstitial();
        RequestNativeExpressAdView();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void RequestBanner()
    {
#if UNITY_EDITOR
        string adUnitId = "unused";
#elif UNITY_ANDROID
            string adUnitId = "ca-app-pub-8920179393260755/1666026629";
#elif UNITY_IPHONE
            string adUnitId = "INSERT_IOS_BANNER_AD_UNIT_ID_HERE";
#else
            string adUnitId = "unexpected_platform";
#endif

        // Create a 320x50 banner at the top of the screen.
        bannerView = new BannerView(adUnitId, AdSize.SmartBanner, AdPosition.Bottom);
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder()
            .AddTestDevice(AdRequest.TestDeviceSimulator)
            .Build();
        // Load the banner with the request.
        bannerView.LoadAd(request);
    }

    private void RequestInterstitial()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-8920179393260755/3142759822";
#elif UNITY_IPHONE
        string adUnitId = "INSERT_IOS_INTERSTITIAL_AD_UNIT_ID_HERE";
#else
        string adUnitId = "unexpected_platform";
#endif

        // Initialize an InterstitialAd.
        interstitial = new InterstitialAd(adUnitId);
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        interstitial.LoadAd(request);
        // Called when the user returned from the app after an ad click.
        interstitial.OnAdClosed += HandleOnAdClosed;
    }

    public void ShowInterstitial()
    {
        text.text = interstitial.IsLoaded().ToString();
        if (interstitial.IsLoaded())
        {
            interstitial.Show();
        }
    }

    private void HandleOnAdClosed(object sender, EventArgs args)
    {
        text.text = interstitial.IsLoaded().ToString();
        if (!interstitial.IsLoaded())
        {
            AdRequest request = new AdRequest.Builder().Build();
            interstitial.LoadAd(request);
        }
    }

    private void RequestNativeExpressAdView()
    {
#if UNITY_EDITOR
        string adUnitId = "unused";
#elif UNITY_ANDROID
        string adUnitId = "ca-app-pub-8920179393260755/9049692625";
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-8920179393260755/9049692625";
#else
        string adUnitId = "unexpected_platform";
#endif

        // Create a 320x50 native express ad at the top of the screen.
        nativeExpressAdView = new NativeExpressAdView(adUnitId, new AdSize(320, 150), AdPosition.Top);
        // Load a banner ad.
        nativeExpressAdView.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        nativeExpressAdView.LoadAd(new AdRequest.Builder().Build());
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        print("Ad failed to load: " + args.Message);
        // Handle the ad failed to load event.
    }


}
