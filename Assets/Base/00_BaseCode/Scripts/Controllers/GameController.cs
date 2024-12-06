using MoreMountains.NiceVibrations;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System;
using System.Security.Cryptography;
using System.Text;
 
using GoogleMobileAds.Ump.Api;
#if UNITY_IOS
using Unity.Advertisement.IosSupport;
#endif


public class GameController : MonoBehaviour
{
    public static GameController Instance;

    public MoneyEffectController moneyEffectController;
    public UseProfile useProfile;
    public DataContain dataContain;
    public MusicManagerGameBase musicManager;
    public AdmobAdsGoogle admobAds;

    public AnalyticsController AnalyticsController;
    public IapController iapController;
  
    [HideInInspector] public SceneType currentScene;
    public AdsStarLoadingAds adsStarLoadingAds;
    public StartLoading startLoading;
    public float idBackground;
    public GameObject startLoadingPanel;
    public GameObject adsStarLoadingAdsPanel;
    public bool initFirebaseOk;
 

    protected void Awake()
    {
        Instance = this;
        initFirebaseOk = false;
        Init();

        DontDestroyOnLoad(this);

        //GameController.Instance.useProfile.IsRemoveAds = true;


#if UNITY_IOS
    if(ATTrackingStatusBinding.GetAuthorizationTrackingStatus() == 
    ATTrackingStatusBinding.AuthorizationTrackingStatus.NOT_DETERMINED)
    {
        ATTrackingStatusBinding.RequestAuthorizationTracking();
    }
#endif

    }

    private void Start()
    {
        //   musicManager.PlayBGMusic();

    }

    public void Init()
    {
        Application.targetFrameRate = 60;
        StartCoroutine(Main());
    }
    IEnumerator Main()
    {
        admobAds.Init();
        yield return new WaitForSeconds(3.5f);  
        if (UseProfile.FirstShowOpenAds == false)
        {
            HandleWaitInterAds();
        }
        else
        {
            startLoadingPanel.gameObject.SetActive(true);
            adsStarLoadingAdsPanel.gameObject.SetActive(false);
            SetUp();
            startLoading.Init();

        }
    }
    private void HandleWaitInterAds()
    {
        startLoadingPanel.gameObject.SetActive(false);
        adsStarLoadingAdsPanel.gameObject.SetActive(true);
        SetUp();
        adsStarLoadingAds.Init();
    }    
  


    public void SetUp()
    {
      
        musicManager.Init();
        iapController.Init();
        MMVibrationManager.SetHapticsActive(useProfile.OnVibration);
  
    
        //idBackground = RemoteConfigController.GetFloatConfig(FirebaseConfig.ID_BACK_GROUND, 1);
    }

    public void LoadScene(string sceneName)
    {
        Initiate.Fade(sceneName.ToString(), Color.black, 2f);
    }


}
public enum SceneType
{
    StartLoading = 0,
    MainHome = 1,
    GamePlay = 2
}