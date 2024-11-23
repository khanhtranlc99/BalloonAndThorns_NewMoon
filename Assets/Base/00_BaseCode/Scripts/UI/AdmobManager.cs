using System;
using System.Collections;
using System.Collections.Generic;
using GoogleMobileAds.Api;
// using GoogleMobileAds.Api.Mediation.AppLovin;
// using GoogleMobileAds.Api.Mediation.IronSource;
// using GoogleMobileAds.Api.Mediation.LiftoffMonetize;
// using GoogleMobileAds.Api.Mediation.UnityAds;
// using GoogleMobileAds.Mediation.DTExchange.Api;
using Newtonsoft.Json;
using UnityEngine;

public class AdmobManager : Singleton<AdmobManager>
{
    #region Inspector
    [SerializeField] private string bannerID = "ca-app-pub-4667689154549040/5142291943";
    [SerializeField] private string AOAID = "ca-app-pub-4667689154549040/8889965268";
    #endregion

    #region Variables
    BannerView bannerView;
    private bool isShowBanner = false;
    public bool bannerAvailable { get; private set; } = false;
    private float bannerRefreshTime = 10;
    public bool IsReady { get; private set; }
    public bool HasBanner = false;
    public bool IsShowingAds = false;
    #endregion

    #region Unity Methods

    void Start()
    {
        Init();
    }

    private void Update()
    {
        if (isShowBanner)
        {
            bannerRefreshTime -= Time.deltaTime;
            if (bannerRefreshTime <= 0 && GameController.Instance.useProfile.IsRemoveAds)
            {
                bannerRefreshTime = RemoteConfigController.GetFloatConfig(StringHelper.CAN_SHOW_RATE, 10);
                Debug.Log("Reload collapsible banner");
                CreateBanner();
            }

        }
    }
    #endregion

    #region Public Methods

    public void SetLoadAds()
    {
        CreateBanner();
        LoadAOA();
    }

    public void ShowBanner(bool isShow)
    {
        isShowBanner = isShow;
        if (isShow)
        {
            if (bannerView != null)
                bannerView.Show();
            else
                CreateBanner();
            bannerRefreshTime = RemoteConfigController.GetFloatConfig(StringHelper.CAN_SHOW_RATE, 10);
            Debug.Log($"Show Admob banner");
        }
        else
        {
            bannerRefreshTime = RemoteConfigController.GetFloatConfig(StringHelper.CAN_SHOW_RATE, 10);
            Debug.Log($"Hide Admob banner");
            DestroyBannerView();
        }

    }
    public void ShowAppOpenAds()
    {
        if (appOpenAd != null && appOpenAd.CanShowAd())
            appOpenAd.Show();
    }
    #endregion

    #region Private Methods
    public void Init()
    {
        Debug.Log("[Admob] Init");
        bannerRefreshTime = bannerRefreshTime = RemoteConfigController.GetFloatConfig(StringHelper.CAN_SHOW_RATE, 10);
        MobileAds.Initialize(initStatus =>
        {
        
            Dictionary<string, AdapterStatus> map = initStatus.getAdapterStatusMap();
            Debug.Log($"[Admob] Init status: {initStatus} - {map.Count}");
            foreach (KeyValuePair<string, AdapterStatus> keyValuePair in map)
            {
                string className = keyValuePair.Key;
                AdapterStatus status = keyValuePair.Value;
                switch (status.InitializationState)
                {
                    case AdapterState.NotReady:
                        // The adapter initialization did not complete.
                        Debug.Log("[Admob] Adapter: " + className + " not ready.");
                        break;
                    case AdapterState.Ready:
                        // The adapter was successfully initialized.
                        Debug.Log("[Admob] Adapter: " + className + " is initialized.");
                        break;
                }
            }

            Debug.Log($"[Admob] Init Done whit status {JsonConvert.SerializeObject(initStatus)}");
            SetLoadAds();
        });
        StartCoroutine(delaytemp());
    }
    IEnumerator delaytemp()
    {
        yield return new WaitForSeconds(7);
        IsReady = true;
    }    
    #endregion

    #region Banner Methods
    private void CreateBanner()
    {
        DestroyBannerView();
        bannerView = new BannerView(bannerID, AdSize.Banner, AdPosition.Bottom);
        ListenToBannerEvent(bannerView);
        var adRequest = new AdRequest();
       // adRequest.Extras.Add("collapsible", "bottom");
        bannerView.LoadAd(adRequest);
        
        if (isShowBanner )
            bannerView.Show();
        else
            DestroyBannerView();
    }

    private void DestroyBannerView()
    {
        if (bannerView != null)
        {
            Debug.Log($"Destroy Admob banner");
            bannerAvailable = false;
            RemoveListenBannerEvent(bannerView);
            bannerView.Hide();
            bannerView.Destroy();
            bannerView = null;
        }
    }

    private void ListenToBannerEvent(BannerView _banner)
    {
        if (_banner == null) return;
        // Raised when an ad is loaded into the banner view.
        _banner.OnBannerAdLoaded += () =>
        {
            bannerAvailable = true;
            Debug.Log("Banner view loaded an ad with response : "
                      + _banner.GetResponseInfo());
            if (isShowBanner)
            {
                _banner.Show();
          
            }
            else
            {
                _banner.Hide();
            }
        };
        // Raised when an ad fails to load into the banner view.
        _banner.OnBannerAdLoadFailed += (LoadAdError error) =>
        {
            bannerAvailable = false;
            Debug.LogError("Banner view failed to load an ad with error : "
                           + error);
            Invoke(nameof(CreateBanner), 10);
        
        };
        // Raised when the ad is estimated to have earned money.
        _banner.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(String.Format("Banner view paid {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));
            OnAdRevenuePaidEvent(bannerID, "COLLAPSIBLE_BANNER", _banner.GetResponseInfo(), adValue);
        };
        // Raised when an impression is recorded for an ad.
        _banner.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Banner view recorded an impression.");
        };
        // Raised when a click is recorded for an ad.
        _banner.OnAdClicked += () =>
        {
            Debug.Log("Banner view was clicked.");
        };
        // Raised when an ad opened full screen content.
        _banner.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Banner view full screen content opened.");
        };
        // Raised when the ad closed full screen content.
        _banner.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Banner view full screen content closed.");
        };
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
    #endregion

    #region AOA Methods
    public bool isLoadingAOA = false;
    public bool AOAAvailable => appOpenAd != null && appOpenAd.CanShowAd();
    int AOAtryTimes = 0;
    private AppOpenAd appOpenAd = null;
    private void LoadAOA()
    {
        if (RemoteConfigController.GetBoolConfig(FirebaseConfig.SHOW_OPEN_ADS, true))
        {
            appOpenAd = null;
            IsReady = true;
            return;
        }
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            appOpenAd = null;
            IsReady = true;
            return;
        }
        AOAtryTimes++;
        isLoadingAOA = true;
        if (appOpenAd != null)
        {
            appOpenAd.Destroy();
            appOpenAd = null;
        }
        Debug.Log("Loading the app open ad.");

        // Create our request used to load the ad.
        var adRequest = new AdRequest();

        // send the request to load the ad.
        AppOpenAd.Load(AOAID, adRequest, (AppOpenAd ad, LoadAdError error) =>
            {
                if (error != null || ad == null)
                {
                    Debug.LogError("[Admob] App open ad failed to load an ad with error : " + error);
                    if (AOAtryTimes < 10)
                    {
                        LoadAOA();
                    }
                    else
                    {
                        isLoadingAOA = false;
                        IsReady = true;
                    }
                }
                else
                {
                    Debug.Log("[Admob] App open ad loaded with response : " + ad.GetResponseInfo());

                    appOpenAd = ad;
                    RegisterAOAEventHandlers(appOpenAd);
                    isLoadingAOA = false;
                    IsReady = true;
                }
            });
    }
    private void RegisterAOAEventHandlers(AppOpenAd ad)
    {
        // Raised when the ad is estimated to have earned money.
        ad.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(String.Format("App open ad paid {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));
            OnAdRevenuePaidEvent(AOAID, "ADMOB_AOA", ad.GetResponseInfo(), adValue);
        };
        // Raised when an impression is recorded for an ad.
        ad.OnAdImpressionRecorded += () =>
        {
            Debug.Log("App open ad recorded an impression.");
        };
        // Raised when a click is recorded for an ad.
        ad.OnAdClicked += () =>
        {
            Debug.Log("App open ad was clicked.");
        };
        // Raised when an ad opened full screen content.
        ad.OnAdFullScreenContentOpened += () =>
        {
            IsShowingAds = true;
            Debug.Log("App open ad full screen content opened.");
        };
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += () =>
        {
            IsShowingAds = false;
            Debug.Log("App open ad full screen content closed.");
        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            IsShowingAds = false;
            Debug.LogError("App open ad failed to open full screen content " +
                           "with error : " + error);
        };
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

    #region Inter Methods
    private InterstitialAd _interstitialAd;
    public string _adInterUnitId;
    Action evtInterDone;
    public void ShowInterstitialAd(Action callback)
    {
        evtInterDone = callback;
        if (_interstitialAd != null && _interstitialAd.CanShowAd())
        {
            Debug.Log("Showing interstitial ad.");
            _interstitialAd.Show();
        }
        else
        {
            Debug.LogError("Interstitial ad is not ready yet.");
        }
    }
    public void LoadInterstitialAd()
    {
        // Clean up the old ad before loading a new one.
        if (_interstitialAd != null)
        {
            _interstitialAd.Destroy();
            _interstitialAd = null;
        }

        Debug.Log("Loading the interstitial ad.");

        // create our request used to load the ad.
        var adRequest = new AdRequest();

        // send the request to load the ad.
        InterstitialAd.Load(_adInterUnitId, adRequest,
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
                RegisterInterEventHandlers(_interstitialAd);
                RegisterInterReloadHandler(_interstitialAd);
            });
    }
    private void RegisterInterEventHandlers(InterstitialAd interstitialAd)
    {
        // Raised when the ad is estimated to have earned money.
        interstitialAd.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(String.Format("Interstitial ad paid {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));
                
            OnAdRevenuePaidEvent(AOAID, "ADMOB_INTER", interstitialAd.GetResponseInfo(), adValue);
        };
        // Raised when an impression is recorded for an ad.
        interstitialAd.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Interstitial ad recorded an impression.");
        };
        // Raised when a click is recorded for an ad.
        interstitialAd.OnAdClicked += () =>
        {
            Debug.Log("Interstitial ad was clicked.");
        };
        // Raised when an ad opened full screen content.
        interstitialAd.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Interstitial ad full screen content opened.");
        };
        // Raised when the ad closed full screen content.
        interstitialAd.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Interstitial ad full screen content closed.");
        };
        // Raised when the ad failed to open full screen content.
        interstitialAd.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Interstitial ad failed to open full screen content " +
                           "with error : " + error);
        };
    }
    private void RegisterInterReloadHandler(InterstitialAd interstitialAd)
    {
        // Raised when the ad closed full screen content.
        interstitialAd.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Interstitial Ad full screen content closed.");
            evtInterDone?.Invoke();
            // Reload the ad so that we can show another as soon as possible.
            LoadInterstitialAd();
        };
        // Raised when the ad failed to open full screen content.
        interstitialAd.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Interstitial ad failed to open full screen content " +
                           "with error : " + error);

            // Reload the ad so that we can show another as soon as possible.
            LoadInterstitialAd();
        };
    }
    #endregion Inter Methods

    #region Rewared Methods
    private RewardedAd _rewardedAd;
    public string _adRewardedUnitId;
    Action evtRewardedClose;
    public void ShowRewardedAd(Action closeCallback)
    {
        evtRewardedClose = closeCallback;
        const string rewardMsg =
            "Rewarded ad rewarded the user. Type: {0}, amount: {1}.";

        if (_rewardedAd != null && _rewardedAd.CanShowAd())
        {
            _rewardedAd.Show((Reward reward) =>
            {
                // TODO: Reward the user.
                Debug.Log(String.Format(rewardMsg, reward.Type, reward.Amount));
            });
        }
    }
    public void LoadRewardedAd()
    {
        // Clean up the old ad before loading a new one.
        if (_rewardedAd != null)
        {
            _rewardedAd.Destroy();
            _rewardedAd = null;
        }

        Debug.Log("Loading the rewarded ad.");

        // create our request used to load the ad.
        var adRequest = new AdRequest();

        // send the request to load the ad.
        RewardedAd.Load(_adRewardedUnitId, adRequest,
            (RewardedAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    Debug.LogError("Rewarded ad failed to load an ad " +
                                   "with error : " + error);
                    return;
                }

                Debug.Log("Rewarded ad loaded with response : "
                          + ad.GetResponseInfo());

                _rewardedAd = ad;
                RegisterRewardedEventHandlers(_rewardedAd);
            });
    }
    private void RegisterRewardedEventHandlers(RewardedAd ad)
    {
        // Raised when the ad is estimated to have earned money.
        ad.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(String.Format("Rewarded ad paid {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));
            OnAdRevenuePaidEvent(AOAID, "ADMOB_REWARDED", ad.GetResponseInfo(), adValue);
        };
        // Raised when an impression is recorded for an ad.
        ad.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Rewarded ad recorded an impression.");
        };
        // Raised when a click is recorded for an ad.
        ad.OnAdClicked += () =>
        {
            Debug.Log("Rewarded ad was clicked.");
        };
        // Raised when an ad opened full screen content.
        ad.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Rewarded ad full screen content opened.");
        };
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Rewarded ad full screen content closed.");
            evtRewardedClose?.Invoke();
        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Rewarded ad failed to open full screen content " +
                           "with error : " + error);
        };
    }
    #endregion
}
