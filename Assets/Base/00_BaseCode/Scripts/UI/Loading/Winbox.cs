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
    public GameObject cardPanel;
    public GameObject endChapperPanel;

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
        if (UseProfile.CurrentLevel_Chapper_I == 39  )
        {
            cardPanel.gameObject.SetActive(false);
            endChapperPanel.gameObject.SetActive(true);
        }
        else
        {
            cardPanel.gameObject.SetActive(true);
            endChapperPanel.gameObject.SetActive(false);
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
                if (UseProfile.CurrentLevel_Chapper_I < 40)
                {
                    UseProfile.CurrentLevel_Chapper_I += 1;
                }
                else
                {
                    UseProfile.CurrentLevel_Chapper_II += 1;

                    if (UseProfile.CurrentLevel_Chapper_II > 40)
                    {
                        UseProfile.CurrentLevel_Chapper_II = 40;
                    }
                }
                UseProfile.CurrentLevel += 1;
                if(UseProfile.CurrentLevel_Chapper_I == 40 && UseProfile.CurrentLevel_Chapper_II == 1)
                {
                    Initiate.Fade("GamePlay", Color.black, 2f);
                }
                else
                {
                    GamePlayController.Instance.playerContain.HandleNextLevel();
                }
                Debug.LogError("CurrentLevel_Chapper_I" + UseProfile.CurrentLevel_Chapper_I);
                Debug.LogError("CurrentLevel_Chapper_II" + UseProfile.CurrentLevel_Chapper_II);
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
