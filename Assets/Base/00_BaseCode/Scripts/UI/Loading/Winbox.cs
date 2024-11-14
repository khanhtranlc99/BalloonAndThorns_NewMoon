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
    public Button nextButton_2;
    public CoinHeartBar coinHeartBar;
    bool lockBtn;
    public List<CardBase> lsCardBase;
    public List<CardUI> lsCardUI;
    public GameObject cardPanel;
    public GameObject endChapperPanel;
    public bool isAllOff
    {
        get
        {
            foreach (var item in lsCardUI)
            {
                if(!item.isChoose)
                {
                    return false;
                }    
            }
            return true;
        }    

    }

    public void Init()
    {
        lockBtn = false;
        coinHeartBar.Init();
        nextButton.onClick.AddListener(delegate { HandleNext();    });
        nextButton_2.onClick.AddListener(delegate { HanleClaimGift(); });
        GameController.Instance.musicManager.PlayWinSound();
        lsCardBase = new List<CardBase>();


    }   
    public void InitState()
    {
        nextButton.gameObject.SetActive(false);
   //     GamePlayController.Instance.playerContain.inputThone.enabled = false;
        GameController.Instance.AnalyticsController.WinLevel(UseProfile.CurrentLevel);
        if(lsCardBase.Count > 0)
        {
            lsCardBase.Clear();
        }
        if(UseProfile.CurrentLevel == 1)
        {
            lsCardBase = GamePlayController.Instance.playerContain.cardController.GetCardLevel_1();
        }    
        else
        {
            lsCardBase = GamePlayController.Instance.playerContain.cardController.GetCard();
        }
    
        for(int i = 0; i < lsCardBase.Count; i ++)
        {
            lsCardUI[i].Init(this ,lsCardBase[i]  );
        }
        if (GamePlayController.Instance.playerContain.levelData.levelDetail == 20)
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
                HandleOffPopup();          
                lockBtn = false;
          
              
            }, actionWatchLog: "WinBox");
        }           
    }

    public void HandleOffPopup()
    {

        UseProfile.Coin += GamePlayController.Instance.playerContain.levelData.numbTarget;
        UseProfile.CurrentLevel += 1;
        if(UseProfile.CurrentLevel >= 80)
        {
            UseProfile.CurrentLevel = 80;
        }
            if (GamePlayController.Instance.playerContain.levelData.levelDetail == 20)
        {
            Initiate.Fade("GamePlay", Color.black, 2f);
        }
        else
        {
            GamePlayController.Instance.playerContain.HandleNextLevel();
        }
        Close();
    }
    public void HanleClaimGift()
    {
            Close(); 
        var TempList = UseProfile.lsIdSkinBalls;
     
            var tempGiftBall = GamePlayController.Instance.playerContain.inputThone.listBallController.GetRandomBall;
            if (tempGiftBall != null && tempGiftBall.giftType != GiftType.None)
            {
       
                List<GiftRewardShow> giftRewardShows = new List<GiftRewardShow>();
                giftRewardShows.Add(new GiftRewardShow() { amount = 1, type = tempGiftBall.giftType });
                foreach (var item in giftRewardShows)
                {
                    GameController.Instance.dataContain.giftDatabase.Claim(item.type, item.amount);
                }
                TempList.Add(tempGiftBall.id);
                UseProfile.lsIdSkinBalls = TempList;
                UseProfile.id_ball_skin = tempGiftBall.id;
                PopupRewardBase.Setup(false).Show(giftRewardShows, delegate { HandleNext();  });
            }
            else
            {
            var TempListCanon = UseProfile.lsIdSkinCanons;
              var tempGiftCannon = GamePlayController.Instance.playerContain.cannonSkinController.GetRandomCannon;
                if (tempGiftCannon != null && tempGiftCannon.giftType != GiftType.None)
                {
             
                   List<GiftRewardShow> giftRewardShows = new List<GiftRewardShow>();
                    giftRewardShows.Add(new GiftRewardShow() { amount = 1, type = tempGiftCannon.giftType });
                    foreach (var item in giftRewardShows)
                    {
                        GameController.Instance.dataContain.giftDatabase.Claim(item.type, item.amount);
                    }
                TempListCanon.Add(tempGiftCannon.id);
                UseProfile.lsIdSkinCanons = TempListCanon;
                UseProfile.id_cannon_skin = tempGiftCannon.id;
                PopupRewardBase.Setup(false).Show(giftRewardShows, delegate { HandleNext(); });
                }
                else
                {
                  HandleNext();
 
                }    
            }
   
     
    }

    public void OnWatchCard()
    {
    
            foreach (var item in lsCardUI)
            {
               if(!item.isChoose)
              {
                item.HandleClear();
                item.btnWatchAds.gameObject.SetActive(true);
              }
             
            }
            nextButton.gameObject.SetActive(true);
    }
 

}
