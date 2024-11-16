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
    public RawImage mesh;
    public bool isLoadNativeOK;
    AdLoader adLoader;
    void Start()
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
        Invoke(nameof(LoadNative),3);
    }

    private void HandleNativeAdLoaded(object sender, NativeAdEventArgs args)
    {
        isLoadNativeOK = true;
        Debug.LogError("Native ad loaded.");
        this.nativeAd = args.nativeAd;

        Texture2D iconTexture = this.nativeAd.GetIconTexture();
        mesh.texture = iconTexture;
        // Register GameObject that will display icon asset of native ad.
        if (!this.nativeAd.RegisterIconImageGameObject(mesh.gameObject))
        {
            // Handle failure to register ad asset.
        }

    }
    private void LoadNative()
    {
       if(!isLoadNativeOK)
        {
            adLoader.LoadAd(new AdRequest());
        }

    }    

}
