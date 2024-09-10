using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LoseBox : BaseBox
{
    public static LoseBox _instance;
    public static LoseBox Setup()
    {
        if (_instance == null)
        {
            _instance = Instantiate(Resources.Load<LoseBox>(PathPrefabs.LOSE_BOX));
            _instance.Init();
        }
        _instance.InitState();
        return _instance;
    }
 
    public Button btnRetry;
    public Button btnSkip;


    public void Init()
    {
        GameController.Instance.musicManager.PlayLoseSound();
        btnRetry.onClick.AddListener(delegate { HandleRetry(); });
        btnSkip.onClick.AddListener(delegate {

            HandleSkip();


        });
    }   
    public void InitState()
    {
        GamePlayController.Instance.playerContain.levelData.inputThone.enabled = false;
        GameController.Instance.AnalyticsController.LoseLevel(UseProfile.CurrentLevel);
    }
    private void HandleRetry()
    {
        GameController.Instance.admobAds.ShowInterstitial(false, actionIniterClose: () => {

            Initiate.Fade("GamePlay", Color.black, 2f);

        }, actionWatchLog: "LoseBox");
    }    
   
    private void HandleSkip()
    {
        GameController.Instance.admobAds.ShowVideoReward(
                actionReward: () =>
                {

                    UseProfile.CurrentLevel += 1;
                    Initiate.Fade("GamePlay", Color.black, 2f);
                },
                actionNotLoadedVideo: () =>
                {
          
                    GameController.Instance.moneyEffectController.SpawnEffectText_FlyUp_UI
                     (
                        btnSkip.transform
                        ,
                     btnSkip.transform.position,
                     "No video",
                     Color.white,
                     isSpawnItemPlayer: true
                     );
                },
                actionClose: null,
                ActionWatchVideo.Skip_level,
                UseProfile.CurrentLevel.ToString());
    }
}
