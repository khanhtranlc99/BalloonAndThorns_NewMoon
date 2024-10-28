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
        if (Time.timeScale == 2)
        {
            Time.timeScale = 1;
          GamePlayController.Instance.gameScene.HandleChangeNormal();
        }
      
        GameController.Instance.AnalyticsController.LoseLevel(UseProfile.CurrentLevel);
     
    }
    private void HandleRetry()
    {
        GameController.Instance.admobAds.ShowInterstitial(false, actionIniterClose: () => {

            UseProfile.CurrentLevel = 1;
            Initiate.Fade("GamePlay", Color.black, 2f);

        }, actionWatchLog: "LoseBox");
    }    
   
    private void HandleSkip()
    {
        GameController.Instance.admobAds.ShowVideoReward(
                actionReward: () =>
                {
                    GamePlayController.Instance.playerContain.inputThone.NumShoot += 3;
                    GamePlayController.Instance.gameScene.tvTarget.text = "" + GamePlayController.Instance.playerContain.inputThone.NumShoot;
                    GamePlayController.Instance.playerContain.inputThone.HandleSetUp();
                    Close();
            
                    //Initiate.Fade("GamePlay", Color.black, 2f);
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
