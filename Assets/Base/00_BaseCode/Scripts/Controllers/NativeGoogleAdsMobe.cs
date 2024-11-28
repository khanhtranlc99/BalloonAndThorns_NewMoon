using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;
using UnityEngine.UI;
using Firebase.Analytics;

public enum NativeName
{
    language_scr,
    onboarding_scr_1,
    onboarding_scr_2,
    native_full_scr,
    onboarding_scr_3,
    main_activity,

}
public class NativeGoogleAdsMobe : MonoBehaviour
{
    public NativeName nativeName;
    public string NativeID;
    public NativeAd nativeAd;
    public bool isLoadNativeOK;  
    public AdLoader adLoader;
     
    


 
    public void Init()
    {
        isLoadNativeOK = false;
        adLoader = new AdLoader.Builder(NativeID).ForNativeAd().Build();
        adLoader.OnNativeAdLoaded += this.HandleNativeAdLoaded;
        adLoader.OnAdFailedToLoad += this.HandleAdFailedToLoad;
        adLoader.OnNativeAdClicked += this.HandleAdNativeAdClicked;
        adLoader.OnNativeAdImpression += this.HandleOnNativeAdImpression;
      
        adLoader.LoadAd(new AdRequest());
     
    }

    private void HandleAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        isLoadNativeOK = false;


    }

    private void HandleNativeAdLoaded(object sender, NativeAdEventArgs args)
    {
    
        Debug.LogError("Native ad loaded.");
        this.nativeAd = args.nativeAd;
        isLoadNativeOK = true;


    }
    private void HandleOnNativeAdImpression(object sender, EventArgs args)
    {
        switch(nativeName)
        {
            case NativeName.language_scr:
                FirebaseAnalytics.LogEvent("language_scr");
                break;
            case NativeName.onboarding_scr_1:
                FirebaseAnalytics.LogEvent("onboarding_scr_1");
                break;
            case NativeName.onboarding_scr_2:
                FirebaseAnalytics.LogEvent("onboarding_scr_2");
                break;
            case NativeName.native_full_scr:
                FirebaseAnalytics.LogEvent("native_full_scr");
                break;
            case NativeName.onboarding_scr_3:
                FirebaseAnalytics.LogEvent("onboarding_scr_3");
                break;
            case NativeName.main_activity:
                FirebaseAnalytics.LogEvent("main_activity");
                break;
        }    

    }
    private void HandleAdNativeAdClicked(object sender, EventArgs args)
    {


    }
    public void LoadNative()
    {
        if(nativeAd != null)
        {
            nativeAd.Destroy();
            nativeAd = null;
        }
        Init();    
    }
   
}
