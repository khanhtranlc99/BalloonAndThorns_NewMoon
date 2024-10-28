using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class Winbox : BaseBox
{
    public static Winbox _instance;
    public static Winbox Setup()
    {
        if (_instance == null)
        {
            _instance = Instantiate(Resources.Load<Winbox>(PathPrefabs.WIN_BOX));
            _instance.Init();
        }
        _instance.InitState();
        return _instance;
    }
    public Button nextButton;
 
    public CoinHeartBar coinHeartBar;
    bool lockBtn;
    public List<CardBase> lsCardBase;
    public List<CardUI> lsCardUI;

    public void Init()
    {
        lockBtn = false;
        coinHeartBar.Init();
        nextButton.onClick.AddListener(delegate { HandleNext();    });   
        GameController.Instance.musicManager.PlayWinSound();
        lsCardBase = new List<CardBase>();


    }   
    public void InitState()
    {
   //     GamePlayController.Instance.playerContain.inputThone.enabled = false;
        GameController.Instance.AnalyticsController.WinLevel(UseProfile.CurrentLevel);
        if(lsCardBase.Count > 0)
        {
            lsCardBase.Clear();
        }
        lsCardBase = GamePlayController.Instance.playerContain.cardController.GetCard();
        for(int i = 0; i < lsCardBase.Count; i ++)
        {
            lsCardUI[i].Init(lsCardBase[i], delegate { HandleNext(); } );
        }
    }    

    private void HandleNext()
    {
        if(!lockBtn)
        {
            lockBtn = true;
            GameController.Instance.admobAds.ShowInterstitial(false, actionIniterClose: () => {

                GameController.Instance.musicManager.PlayClickSound();
                UseProfile.Coin += GamePlayController.Instance.playerContain.levelData.numbTarget;
                UseProfile.CurrentLevel += 1;
                if (UseProfile.CurrentLevel > 80)
                {
                    UseProfile.CurrentLevel = 80;
                }
                //Initiate.Fade("GamePlay", Color.black, 2f);
                GamePlayController.Instance.playerContain.HandleNextLevel();
               // GamePlayController.Instance.playerContain.inputThone.enabled = true;
               // GamePlayController.Instance.playerContain.inputThone.numShoot = 5;
                lockBtn = false;
                Close();
              
            }, actionWatchLog: "WinBox");
        }    
      



       
    }

   
 
    private void OnDestroy()
    {
        
    }
}
