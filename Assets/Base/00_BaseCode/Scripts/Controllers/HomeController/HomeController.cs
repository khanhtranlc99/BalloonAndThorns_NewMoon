using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System.Runtime.InteropServices;
using Firebase.Analytics;
public class HomeController : Singleton<HomeController>
{
    public Button btnNext;
    public Button btnStartGame;
    public List<GameObject> lsImage;
    public HorizontalScrollSnap scrollSnap;
    public GameObject panelNext;
    public GameObject panelStart;
    public RawImageNativeAds ads_navitve;
    public RawImageNativeAds ads_navitve_1;
    public RawImageNativeAds ads_navitve_2;
    public RawImageNativeAdsFullAds ads_navitve_Full;
    public bool can_show_ads_navitve;
    public bool can_show_ads_navitve_1;
    public bool can_show_ads_navitve_2;
    public bool can_show_ads_navitve_Full;

    public Image bg;
    bool isOpen = false;
    float timeCountDown;
    public Text tvTime;
    //public Button btnClose;
    //public GameObject btnCountDown;
    public Image blinPanel;
    public Text tvPanel;
    public bool offPanel
    { 
        get
        {
            if (!GameController.Instance.admobAds.nativeGoogleAdsMobe_3.isLoadNativeOK)
            {
                return false;
            }
            if (!GameController.Instance.admobAds.nativeGoogleAdsMobe_4.isLoadNativeOK)
            {
                return false;
            }
            if (!GameController.Instance.admobAds.nativeGoogleAdsMobe_5.isLoadNativeOK)
            {
                return false;
            }
            if (!GameController.Instance.admobAds.nativeGoogleAdsMobe_6.isLoadNativeOK)
            {
                return false;
            }
            return true;
        }
    }

    public bool can_post_event_ads_navitve_1;
    public bool can_post_event_ads_navitve_2;
    public bool can_post_event_ads_navitve_Full;

    public void Start()
    {
        
        can_post_event_ads_navitve_1 = false;
        can_post_event_ads_navitve_2 = false;
        can_post_event_ads_navitve_Full = false;

        ads_navitve.gameObject.SetActive(false);
        ads_navitve_1.gameObject.SetActive(false);
        ads_navitve_2.gameObject.SetActive(false);
        ads_navitve_Full.gameObject.SetActive(false);

        isOpen = false;
        btnNext.onClick.AddListener(HandleNextButton);
        btnStartGame.onClick.AddListener(HandleStartButton);

        UseProfile.FirstShowOpenAds = true;

        GameController.Instance.admobAds.HandleLoadAdsInGame();
        //timeCountDown = RemoteConfigController.GetFloatConfig(FirebaseConfig.TIME_OFF_NATIVE_FULL, 3);
        //btnClose.onClick.AddListener(HandleChangeScene);

     
        can_show_ads_navitve = RemoteConfigController.GetBoolConfig(FirebaseConfig.IS_SHOW_NATIVE_ONBOARDING_1, true);
        can_show_ads_navitve_1 = RemoteConfigController.GetBoolConfig(FirebaseConfig.IS_SHOW_NATIVE_ONBOARDING_2, true);
        can_show_ads_navitve_2 = RemoteConfigController.GetBoolConfig(FirebaseConfig.IS_SHOW_NATIVE_ONBOARDING_3, true);
        can_show_ads_navitve_Full = RemoteConfigController.GetBoolConfig(FirebaseConfig.IS_SHOW_NATIVE_FULL, true);
        GameController.Instance.admobAds.nativeGoogleAdsMobe_3.Init(delegate { Native_3(); });
        GameController.Instance.admobAds.nativeGoogleAdsMobe_4.Init(delegate { Native_4(); });
        GameController.Instance.admobAds.nativeGoogleAdsMobe_5.Init(delegate { Native_5(); });
        GameController.Instance.admobAds.nativeGoogleAdsMobe_6.Init(delegate { Native_6(); });
        blinPanel.DOFade(0, 10).OnComplete(delegate { blinPanel.gameObject.SetActive(false); });
        StartCoroutine(ShowText());
        FirebaseAnalytics.LogEvent("onboarding_scr_1");
    }
    public IEnumerator ShowText()
    {
        yield return new WaitForSeconds(1);
        tvPanel.text = "Waits 10 second...";
        yield return new WaitForSeconds(1);
        tvPanel.text = "Waits 9 second..";
        yield return new WaitForSeconds(1);
        tvPanel.text = "Waits 8 second.";
        yield return new WaitForSeconds(1);
        tvPanel.text = "Waits 7 second...";
        yield return new WaitForSeconds(1);
        tvPanel.text = "Waits 6 second..";
        yield return new WaitForSeconds(1);
        tvPanel.text = "Waits 5 second.";
        yield return new WaitForSeconds(1);
        tvPanel.text = "Waits 4 second...";
        yield return new WaitForSeconds(1);
        tvPanel.text = "Waits 3 second..";
        yield return new WaitForSeconds(1);
        tvPanel.text = "Waits 2 second.";
        yield return new WaitForSeconds(1);
        tvPanel.text = "Waits 1 second...";
    }
    private void Native_3()
    {
        ads_navitve.gameObject.SetActive(true);
        ads_navitve.Init(GameController.Instance.admobAds.nativeGoogleAdsMobe_3.nativeAd);
    }
    private void Native_4()
    {
        ads_navitve_1.gameObject.SetActive(true);
        ads_navitve_1.Init(GameController.Instance.admobAds.nativeGoogleAdsMobe_4.nativeAd);
    }
    private void Native_5()
    {
        ads_navitve_2.gameObject.SetActive(true);
        ads_navitve_2.Init(GameController.Instance.admobAds.nativeGoogleAdsMobe_5.nativeAd);
    }
    private void Native_6()
    {
        ads_navitve_Full.gameObject.SetActive(true);
        ads_navitve_Full.Init(GameController.Instance.admobAds.nativeGoogleAdsMobe_6.nativeAd, delegate { HandleChangeScene(); });
    }
 
    private void Update()
    {
        for (int i = 0; i < lsImage.Count; i++)
        {
            if (i == scrollSnap.CurrentPage)
            {
                lsImage[i].SetActive(true);
            }
            else
            {
                lsImage[i].SetActive(false);
            }
        }

      
        if (scrollSnap.CurrentPage == 3)
        {
            panelNext.SetActive(false);
            panelStart.SetActive(true);
        }
        else
        {
            panelNext.SetActive(true);
            panelStart.SetActive(false);
        }
        if (scrollSnap.CurrentPage == 2)
        {
            panelNext.SetActive(false);
            
        }
        GameController.Instance.admobAds.admobSplash.HideBanner();

        if (offPanel )
        {
            if (blinPanel.gameObject.activeSelf)
            {
                blinPanel.DOKill();
                blinPanel.gameObject.SetActive(false);
            }
        }
        if(scrollSnap.CurrentPage == 1)
        {
            if(!can_post_event_ads_navitve_1)
            {
                can_post_event_ads_navitve_1 = true;
                FirebaseAnalytics.LogEvent("onboarding_scr_2");
            }    
          
        }
        if (scrollSnap.CurrentPage == 2)
        {
            if (!can_post_event_ads_navitve_Full)
            {
                can_post_event_ads_navitve_Full = true;
                FirebaseAnalytics.LogEvent("native_full_scr");
            }
         
        }
        if (scrollSnap.CurrentPage == 3)
        {
            if (!can_post_event_ads_navitve_2)
            {
                can_post_event_ads_navitve_2 = true;
                FirebaseAnalytics.LogEvent("onboarding_scr_3");
            }
       
        }

    }
    private void HandleNextButton()
    {
        scrollSnap.NextScreen();
    }
    private void HandleStartButton()
    { 
        if (GameController.Instance.admobAds.IsLoadedInterstitial())
        {
            GameController.Instance.admobAds.ShowInterstitialAd(actionIniterClose: () => {

                bg.DOFade(1, 0.5f).OnComplete(delegate { SceneManager.LoadSceneAsync(SceneName.GAME_PLAY); });

            });
        }
         else
        {
            bg.DOFade(1, 0.5f).OnComplete(delegate { SceneManager.LoadSceneAsync(SceneName.GAME_PLAY); });
        }


    
    }
 
    private void HandleChangeScene()
    {
        scrollSnap.ChangePage(3);
    }
}
