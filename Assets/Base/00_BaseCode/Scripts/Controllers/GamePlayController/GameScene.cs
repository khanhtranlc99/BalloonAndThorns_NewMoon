using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using System;
using MoreMountains.NiceVibrations;
using UnityEngine.Events;

public class GameScene : BaseScene
{
    public Transform iconBallon;
    public Text tvTarget;
    public Text tvLevel;
    public Button settinBtn;
 
    public Button adsBallsButton;
    public Button speedButton;
    int targetBallond;

    public Image warningPanel;
    public Button shopButton;
    public Button resetButton;
    public bool wasOpenShop;

    public GameObject toolsPanel;

    public GameObject panelX1;
    public GameObject panelX2;
    public void Init(LevelData levelData)
    {
        wasOpenShop = false;
        settinBtn.onClick.AddListener(delegate { GamePlayController.Instance.playerContain.levelData.inputThone.enabled = false; SettingBox.Setup(true).Show(); });
        //reStartBtn.onClick.AddListener(delegate { Initiate.Fade("GamePlay", Color.black, 2f); });
        tvLevel.text = "Level " + UseProfile.CurrentLevel;
        targetBallond = levelData.numbTarget;
        tvTarget.text = "" + targetBallond;

        adsBallsButton.onClick.AddListener(delegate { HandleBtnAdsBall();  });
        if (UseProfile.UnlimitSpike)
        {
            adsBallsButton.gameObject.SetActive(false);
        }
        speedButton.onClick.AddListener(HandleSpeedBtn);

        shopButton.onClick.AddListener(delegate {
            wasOpenShop = true;
            GamePlayController.Instance.playerContain.levelData.inputThone.enabled = false;
            ShopBox.Setup(ButtonShopType.Gift).Show();
        });

        if(UseProfile.CurrentLevel == 1)
        {
            shopButton.interactable = false;
        }
        resetButton.onClick.AddListener(OnReset);
    }

    private void HandleSpeedBtn()
    {
        //GamePlayController.Instance.TutGameplay.NextTut();
        if (Time.timeScale == 1)
        {
            Time.timeScale = 2;
            panelX2.SetActive(false);
            panelX1.SetActive(true);
        }    
        else
        {
            Time.timeScale = 1;
            panelX2.SetActive(true);
            panelX1.SetActive(false);
        }
    
    }

    public void HandleChangeNormal()
    {

        panelX2.SetActive(true);
        panelX1.SetActive(false);

    }

    public  void OnPanel()
    {
        GamePlayController.Instance.playerContain.levelData.inputThone.enabled = true;

    }
    public void OffPanel()
    {
        GamePlayController.Instance.playerContain.levelData.inputThone.enabled = false;

    }

    private void HandleBtnAdsBall()
    {

        GamePlayController.Instance.playerContain.levelData.inputThone.enabled = false;

        GameController.Instance.admobAds.ShowVideoReward(
                actionReward: () =>
                {

                      GamePlayController.Instance.playerContain.levelData.PlusBall();
                    List<GiftRewardShow> giftRewardShows = new List<GiftRewardShow>();
                    giftRewardShows.Add(new GiftRewardShow() { amount = 5, type = GiftType.SPIKE_IN_GAME });
                    foreach (var item in giftRewardShows)
                    {
                        GameController.Instance.dataContain.giftDatabase.Claim(item.type, item.amount);
                    }
                    PopupRewardBase.Setup(false).Show(giftRewardShows, delegate { GamePlayController.Instance.playerContain.levelData.inputThone.enabled = true;  });

                },
                actionNotLoadedVideo: () =>
                {

                    GameController.Instance.moneyEffectController.SpawnEffectText_FlyUp_UI
                     (
                        adsBallsButton.transform
                        ,
                     adsBallsButton.transform.position,
                     "No video",
                     Color.white,
                     isSpawnItemPlayer: true
                     );
                },
                actionClose: null,
                ActionWatchVideo.Skip_level,
                UseProfile.CurrentLevel.ToString());
    }

    public void HandleSubtrackBallon()
    {
        targetBallond -= 1;
        tvTarget.text = "" + targetBallond;
        iconBallon.transform.DOScale(new Vector3(1.2f,1.2f,1.2f), 0.3f).OnComplete(delegate {
            iconBallon.transform.DOScale(new Vector3(1, 1, 1), 0.3f).OnComplete(delegate {

         

            });
        });
    }    
    public override void OnEscapeWhenStackBoxEmpty()
    {
     
    }
    public void HandleWarning()
    {
        warningPanel.DOKill();
        warningPanel.DOFade(0.5f, 0.5f).OnComplete(delegate {

            warningPanel.DOFade(0, 0.5f);
        });
    }    
    private void OnReset()
    {
        GameController.Instance.admobAds.ShowInterstitial(false, actionIniterClose: () => { Next(); }, actionWatchLog: "Restart");
        void Next()
        {
            GameController.Instance.musicManager.PlayClickSound();
            Initiate.Fade("GamePlay", Color.black, 2f);
        }
    }
    public void OnPanelTools()
    {
        toolsPanel.SetActive(true);
    }
    public void OffPanelTools()
    {
        toolsPanel.SetActive(false);
    }
}
