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
    public Button claimDoubleCoinButton;
    public Text tvScore;
    int valueGacha;
    [SerializeField] private GachaBar gachaBar;
    public CoinHeartBar coinHeartBar;
    bool lockBtn;
    public void Init()
    {
        lockBtn = false;
        coinHeartBar.Init();
        nextButton.onClick.AddListener(delegate { HandleNext();    });
        gachaBar.Init();
        GameController.Instance.musicManager.PlayWinSound();

        claimDoubleCoinButton.onClick.AddListener(delegate { HandleOnClickBtnReward(); });
        tvScore.text = "+" + GamePlayController.Instance.playerContain.levelData.numbTarget;
    }   
    public void InitState()
    {
        GamePlayController.Instance.playerContain.levelData.inputThone.enabled = false;
        GameController.Instance.AnalyticsController.WinLevel(UseProfile.CurrentLevel);

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
                Initiate.Fade("GamePlay", Color.black, 2f);

            }, actionWatchLog: "WinBox");
        }    
      



       
    }

    private void HandleOnClickBtnReward()
    {
        if (!lockBtn)
        {
            lockBtn = true;
            valueGacha = gachaBar.ValueReward;
            gachaBar.HandleOnClickStop();
            GameController.Instance.admobAds.ShowVideoReward(delegate { ClaimReward(); }, delegate
            {
                GameController.Instance.moneyEffectController.SpawnEffectText_FlyUp_UI
                (
                    claimDoubleCoinButton.transform,
                    claimDoubleCoinButton.transform.position,
                    "No video at the moment!",
                    Color.white,
                    isSpawnItemPlayer: true
                );
            }, delegate { }, ActionWatchVideo.RewardEndGame, UseProfile.CurrentLevel.ToString());
        }
    }
    void ClaimReward()
    {
        Debug.LogError(valueGacha);

        var temp = (GamePlayController.Instance.playerContain.levelData.numbTarget * valueGacha);
        List<GiftRewardShow> lstReward = new List<GiftRewardShow>();
        lstReward.Add(new GiftRewardShow() { amount = temp, type = GiftType.Coin });

        UseProfile.Coin += temp;
        RewardIAPBox.Setup().Show(lstReward, delegate
        {

            GameController.Instance.musicManager.PlayClickSound();
            UseProfile.Coin += GamePlayController.Instance.playerContain.levelData.numbTarget;
            UseProfile.CurrentLevel += 1;
            if (UseProfile.CurrentLevel > 70)
            {
                UseProfile.CurrentLevel = 70;
            }
            Initiate.Fade("GamePlay", Color.black, 2f);
            Debug.LogError("ClaimReward ");
        });

    }
    private void OnDestroy()
    {
        
    }
}
