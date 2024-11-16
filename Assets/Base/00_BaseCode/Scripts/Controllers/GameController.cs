using MoreMountains.NiceVibrations;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System;
using System.Security.Cryptography;
using System.Text;
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
    public AdmobAds admobAds;

    public AnalyticsController AnalyticsController;
    public IapController iapController;
    public HeartGame heartGame;
    [HideInInspector] public SceneType currentScene;
 
    public StartLoading startLoading;
    public float idBackground;

    protected void Awake()
    {
        Instance = this;
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
        //if (Application.internetReachability != NetworkReachability.NotReachable)
        //{
        //    admobAds.Init();
        //    StartCoroutine(Helper.StartAction(delegate { HandleWaitInterAds();  }, () => AnalyticsController.firebaseInitialized == true));
        //}
        //else
        //{
        //    admobAds.Init();
        //    SetUp();
        //}
        SetUp();

    }
    private void HandleWaitInterAds()
    {
        StartCoroutine(Helper.StartAction(delegate { HandleSpamInter();   }, () => admobAds.IsLoadedInterstitial() == true));


    }    
    private void HandleSpamInter()
    {
          admobAds.ShowInterstitial(false, actionIniterClose: () => {

            

        }, actionWatchLog: "LoseBox");
    }    


    public void SetUp()
    {
        admobAds.Init();
        musicManager.Init();
        iapController.Init();
        MMVibrationManager.SetHapticsActive(useProfile.OnVibration);
  
        heartGame.Init();
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