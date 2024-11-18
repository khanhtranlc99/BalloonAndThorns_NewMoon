using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;
using UnityEngine.UI;

public class GoogleAdsMobe : MonoBehaviour
{
    public string AppID,  NativeID;
    NativeAd nativeAd;
    NativeAd nativeAd_1;
    NativeAd nativeAd_2;
    NativeAd nativeAd_3;
    NativeAd nativeAd_4;
   

    public Texture2D iconTexture;
    public Texture2D iconTexture_1;
    public Texture2D iconTexture_2;
    public Texture2D iconTexture_3;
    public Texture2D iconTexture_4;

 
    public bool isLoadNativeOK;
    AdLoader adLoader;



    public void Init()
    {
        isLoadNativeOK = false;
        MobileAds.RaiseAdEventsOnUnityMainThread = true;
        MobileAds.Initialize(initStatus =>
        {
            RequestNativeAd();
            Debug.LogError("Ads Initialised");
        });
    }
    public void RequestNativeAd()
    {
        adLoader = new AdLoader.Builder(NativeID).ForNativeAd().Build();
        adLoader.OnNativeAdLoaded += this.HandleNativeAdLoaded;
        adLoader.OnAdFailedToLoad += this.HandleAdFailedToLoad;
        adLoader.LoadAd(new AdRequest());
    }

    private void HandleAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        Debug.LogError("Native ad failed to load: " + args.ToString());
    
    }

    private void HandleNativeAdLoaded(object sender, NativeAdEventArgs args)
    {
        isLoadNativeOK = true;
        Debug.LogError("Native ad loaded.");
        this.nativeAd = args.nativeAd;
        iconTexture = this.nativeAd.GetIconTexture();
    

    }
    private void LoadNative()
    {
       if(!isLoadNativeOK)
        {
            adLoader.LoadAd(new AdRequest());
        }

    }    

}
