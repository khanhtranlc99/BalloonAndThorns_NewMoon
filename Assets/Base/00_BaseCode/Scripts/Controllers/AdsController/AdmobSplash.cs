using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;
using UnityEngine.UI;
using UnityEngine.Events;


public class AdmobSplash : MonoBehaviour
{
    private const string BanerAdUnitId = "ca-app-pub-8564251890453142/6735208049";

    private const string InterstitialAdUnitId = "ca-app-pub-8564251890453142/6543636355";

    private const string AppOpenId = "ca-app-pub-8564251890453142/5477966697";

    public bool IsLoadedInterstitial()
    {
        if (_interstitialAd == null)
        {
            return false;
        }
        return _interstitialAd.CanShowAd();
    }
    public bool IsLoadedAOA()
    {
        if (appOpenAd == null)
        {
            return false;
        }
        return appOpenAd.CanShowAd();
    }
    #region Interstitial
    Action evtInterDone;
    InterstitialAd _interstitialAd;
    public void InitInterstitial()
    {
        // Clean up the old ad before loading a new one.
        if (_interstitialAd != null)
        {
            _interstitialAd.Destroy();
            _interstitialAd = null;
        }



        // create our request used to load the ad.
        var adRequest = new AdRequest();

        // send the request to load the ad.
        InterstitialAd.Load(InterstitialAdUnitId, adRequest,
            (InterstitialAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    Debug.LogError("interstitial ad failed to load an ad " +
                                   "with error : " + error);
                    return;
                }

                Debug.Log("Interstitial ad loaded with response : "
                          + ad.GetResponseInfo());

                _interstitialAd = ad;
                ListenToInterstitialEvent(_interstitialAd);

            });

    }
    private void ListenToInterstitialEvent(InterstitialAd interstitialAd)
    {

        // Raised when the ad is estimated to have earned money.
        interstitialAd.OnAdPaid += (AdValue adValue) =>
        {

            OnAdRevenuePaidEvent(InterstitialAdUnitId, "ADMOB_INTER_SPLASH", interstitialAd.GetResponseInfo(), adValue);
        };
        // Raised when an impression is recorded for an ad.
        interstitialAd.OnAdImpressionRecorded += () =>
        {

        };
        // Raised when a click is recorded for an ad.
        interstitialAd.OnAdClicked += () =>
        {

        };
        // Raised when an ad opened full screen content.
        interstitialAd.OnAdFullScreenContentOpened += () =>
        {

        };
        // Raised when the ad closed full screen content.
        interstitialAd.OnAdFullScreenContentClosed += () =>
        {

        };

        interstitialAd.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Interstitial Ad full screen content closed.");
            evtInterDone?.Invoke();
            // Reload the ad so that we can show another as soon as possible.
            Invoke(nameof(InitInterstitial), 3);
        };
        // Raised when the ad failed to open full screen content.
        interstitialAd.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Interstitial ad failed to open full screen content " +
                           "with error : " + error);

            // Reload the ad so that we can show another as soon as possible.
            Invoke(nameof(InitInterstitial), 3);
        };
    }
    public void ShowInterstitialAd(Action actionIniterClose)
    {
        evtInterDone = actionIniterClose;
        if (GameController.Instance.useProfile.IsRemoveAds)
        {
            evtInterDone?.Invoke();
            return;
        }
        if (_interstitialAd != null && _interstitialAd.CanShowAd())
        {        
             _interstitialAd.Show();      
        }
        else
        {
            evtInterDone?.Invoke();
        }
    }



    #endregion

    #region Banner
    BannerView bannerView;
    bool noCollapsible = false;
    public void InitializeBannerAds()
    {
        DestroyBannerView();
        bannerView = new BannerView(BanerAdUnitId, AdSize.Banner, AdPosition.Bottom);
        ListenToBannerEvent(bannerView);
        var adRequest = new AdRequest();
        bannerView.LoadAd(adRequest);
        bannerView.Hide();
    }

    private void RemoveListenBannerEvent(BannerView _banner)
    {
        if (_banner == null) return;
        // Raised when an ad is loaded into the banner view.
        _banner.OnBannerAdLoaded -= () =>
        {
            Debug.Log("Banner view loaded an ad with response : "
                      + _banner.GetResponseInfo());
        };
        // Raised when an ad fails to load into the banner view.
        _banner.OnBannerAdLoadFailed -= (LoadAdError error) =>
        {
            Debug.LogError("Banner view failed to load an ad with error : "
                           + error);
        };
        // Raised when the ad is estimated to have earned money.
        _banner.OnAdPaid -= (AdValue adValue) =>
        {
            Debug.Log(String.Format("Banner view paid {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));
        };
        // Raised when an impression is recorded for an ad.
        _banner.OnAdImpressionRecorded -= () =>
        {
            Debug.Log("Banner view recorded an impression.");
        };
        // Raised when a click is recorded for an ad.
        _banner.OnAdClicked -= () =>
        {
            Debug.Log("Banner view was clicked.");
        };
        // Raised when an ad opened full screen content.
        _banner.OnAdFullScreenContentOpened -= () =>
        {
            Debug.Log("Banner view full screen content opened.");
        };
        // Raised when the ad closed full screen content.
        _banner.OnAdFullScreenContentClosed -= () =>
        {
            Debug.Log("Banner view full screen content closed.");
        };
    }
    private void ListenToBannerEvent(BannerView _banner)
    {
        if (_banner == null) return;
        // Raised when an ad is loaded into the banner view.
        _banner.OnBannerAdLoaded += () =>
        {
      
            try
            {
                ShowBanner();
            }
            catch
            {

            }

        };
        // Raised when an ad fails to load into the banner view.
        _banner.OnBannerAdLoadFailed += (LoadAdError error) =>
        {
     

            Invoke(nameof(InitializeBannerAds), 3);

        };
        // Raised when the ad is estimated to have earned money.
        _banner.OnAdPaid += (AdValue adValue) =>
        {

            OnAdRevenuePaidEvent(BanerAdUnitId, "COLLAPSIBLE_BANNER", _banner.GetResponseInfo(), adValue);
        };
        // Raised when an impression is recorded for an ad.
        _banner.OnAdImpressionRecorded += () =>
        {

        };
        // Raised when a click is recorded for an ad.
        _banner.OnAdClicked += () =>
        {

        };
        // Raised when an ad opened full screen content.
        _banner.OnAdFullScreenContentOpened += () =>
        {

        };
        // Raised when the ad closed full screen content.
        _banner.OnAdFullScreenContentClosed += () =>
        {

        };
    }

    public void ShowBanner()
    {
        if (GameController.Instance.useProfile.IsRemoveAds)
        {
            return;
        }
        if (bannerView != null)
        {
            bannerView.Show();
        }

    }
    private void DestroyBannerView()
    {
        if (bannerView != null)
        {
            Debug.Log($"Destroy Admob banner");
  
            RemoveListenBannerEvent(bannerView);
            bannerView.Hide();
            bannerView.Destroy();
            bannerView = null;
        }
    }

    public void HideBanner()
    {
        if (GameController.Instance.useProfile.IsRemoveAds)
        {
            return;
        }
        if (bannerView != null)
        {
            bannerView.Hide();
        }

    }

    #endregion
    #region AppOpenAd

    private AppOpenAd appOpenAd = null;
    Action evtAppOpenAdDone;
    public void InitializeOpenAppAds()
    {
        if (appOpenAd != null)
        {
            appOpenAd.Destroy();
            appOpenAd = null;
        }
        var adRequest = new AdRequest();

        // send the request to load the ad.
        AppOpenAd.Load(AppOpenId, adRequest, (AppOpenAd ad, LoadAdError error) =>
        {
            if (error != null || ad == null)
            {
                return;
            }
            else
            {
                appOpenAd = ad;
                RegisterAOAEventHandlers(appOpenAd);

            }
        });

    }
    private void RegisterAOAEventHandlers(AppOpenAd ad)
    {
        // Raised when the ad is estimated to have earned money.
        ad.OnAdPaid += (AdValue adValue) =>
        {

            OnAdRevenuePaidEvent(AppOpenId, "ADMOB_AOA", ad.GetResponseInfo(), adValue);
        };
        // Raised when an impression is recorded for an ad.
        ad.OnAdImpressionRecorded += () =>
        {

        };
        // Raised when a click is recorded for an ad.
        ad.OnAdClicked += () =>
        {

        };
        // Raised when an ad opened full screen content.
        ad.OnAdFullScreenContentOpened += () =>
        {


        };
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += () =>
        {

            evtAppOpenAdDone?.Invoke();
        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Invoke(nameof(InitializeOpenAppAds), 3);

        };
    }
    public void ShowOpenAppAdsReady(Action actionIniterClose)
    {
        evtAppOpenAdDone = actionIniterClose;
        if (GameController.Instance.useProfile.IsRemoveAds)
        {
            return;
        }

        //if (!UseProfile.FirstShowOpenAds)
        //{

        //    UseProfile.FirstShowOpenAds = true;
        //}
        //else
        //{
        //if (RemoteConfigController.GetBoolConfig(FirebaseConfig.SHOW_OPEN_ADS, true))
        //      {
        if (appOpenAd != null && appOpenAd.CanShowAd())
        {
            appOpenAd.Show();
            Debug.LogError("SHOW_OPEN_ADS");
        }

        //       }
        //    Debug.LogError("FirstShowOpenAds_2");
        //}



    }

    #endregion
    private void OnAdRevenuePaidEvent(string adUnitId, string type, ResponseInfo info, AdValue value)
    {


        // AppsflyerManager.Instance.SendEvent("af_ad_revenue");
        var impressionParameters = new[] {
            new Firebase.Analytics.Parameter("ad_platform", "Admob"),
            new Firebase.Analytics.Parameter("ad_source", "Google Admob"),
            new Firebase.Analytics.Parameter("ad_unit_name", adUnitId),
            new Firebase.Analytics.Parameter("ad_format", type),
            new Firebase.Analytics.Parameter("value", value.Value),
            new Firebase.Analytics.Parameter("currency", "USD"), // All AppLovin revenue is sent in USD
        };

        Firebase.Analytics.FirebaseAnalytics.LogEvent("ad_max", impressionParameters);
        Firebase.Analytics.FirebaseAnalytics.LogEvent("ad_impression", impressionParameters);
   
    }
}
