using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SuggetBox : BaseBox
{
    public static SuggetBox _instance;
    public static SuggetBox Setup(GiftType giftType, bool isBoosterTut = false)
    {
        if (_instance == null)
        {
            _instance = Instantiate(Resources.Load<SuggetBox>(PathPrefabs.SUGGET_BOX));
            _instance.Init();
        }
        _instance.InitState( giftType, isBoosterTut);
        return _instance;
    }

    public Button btnClose;
    public Button payByCoinBtn;
    public Button payByAdsBtn;
    public Text tvTitler;
    public Text tvContent;
    public Text tvPrive;
    public int price;
    GiftType currentGift;
    ActionWatchVideo actionWatchVideo;
    public Image iconDecor;
    public Text tvNum;
    public Text tvCountNumbAds;
    public GameObject iconAds;
    public CoinHeartBar coinHeartBar;
    public void Init()
    {
     
        btnClose.onClick.AddListener(delegate { GamePlayController.Instance.playerContain.levelData.inputThone.enabled = true; GameController.Instance.musicManager.PlayClickSound(); Close(); });
   
        payByCoinBtn.onClick.AddListener(delegate { HandlePayByCoin(); });
    }

    public void InitState(GiftType giftType, bool isTut)
    {
        coinHeartBar.Init();
        Debug.LogError(giftType);
        GamePlayController.Instance.playerContain.levelData.inputThone.enabled = false;
        currentGift = giftType;
        switch (giftType)
        {
            case GiftType.MOVE_SIGHTING_POINT_BOOSTER:
                tvTitler.text = "MOVE SIGHTING POINT BOOSTER";
                tvContent.text = "Move the aiming point left and right ";
                price = 200;
                tvPrive.text = price.ToString();
                actionWatchVideo = ActionWatchVideo.MOVE_SIGHTING_POINT_BOOSTER;
                payByAdsBtn.onClick.RemoveAllListeners();
                payByAdsBtn.onClick.AddListener(delegate { HandlePayByAds(); });
                iconAds.SetActive(true);
                tvCountNumbAds.text = UseProfile.NumbWatchAdsMoveSighPoin.ToString() + "/3";
                break;
            case GiftType.SNIPER_BOOSTER:
                tvTitler.text = "Sniper";
                tvContent.text = "Increases visibility by 4 times";
                price = 150;
                tvPrive.text = price.ToString();
                actionWatchVideo = ActionWatchVideo.Sniper_Booster;
                payByAdsBtn.onClick.RemoveAllListeners();
                payByAdsBtn.onClick.AddListener(delegate { HandlePayByAds(); });
                iconAds.SetActive(true);
                tvCountNumbAds.text = UseProfile.NumbWatchAdsSniper.ToString() + "/3";
                break;
            case GiftType.CLONE_BALLS_BOOSTER:
                tvTitler.text = "Clone balls";
                tvContent.text = "Duplicate all spikes";
                price = 300;
                tvPrive.text = price.ToString();
                actionWatchVideo = ActionWatchVideo.CloneBall_Booster;
                payByAdsBtn.onClick.RemoveAllListeners();
                payByAdsBtn.onClick.AddListener(delegate { HandlePayByAds(); });
                iconAds.SetActive(true);
                tvCountNumbAds.text = UseProfile.NumbWatchCloneBall.ToString() + "/3";
                break;
            case GiftType.ROCKET_BOOSTER:
                tvTitler.text = "Rocket";
                tvContent.text = "Create a Big explosion";
                price = 700;
                tvPrive.text = price.ToString();
                actionWatchVideo = ActionWatchVideo.Rocket_Booster;
                payByAdsBtn.onClick.RemoveAllListeners();
                payByAdsBtn.onClick.AddListener(delegate { ShopBox.Setup(ButtonShopType.Gold).Show(); });
                iconAds.SetActive(false);
                tvCountNumbAds.text = "Shop";
                break;
        }
        iconDecor.sprite = GameController.Instance.dataContain.giftDatabase.GetIconItem(giftType);
        iconDecor.SetNativeSize();
        if (isTut)
        {
            payByAdsBtn.gameObject.SetActive(false);
            payByCoinBtn.gameObject.SetActive(false);
            tvNum.gameObject.SetActive(false);
        }    
        else
        {
            payByAdsBtn.gameObject.SetActive(true);
            payByCoinBtn.gameObject.SetActive(true);
            tvNum.gameObject.SetActive(true);

        }
     }


    public void HandlePayByAds()
    {

        GameController.Instance.musicManager.PlayClickSound();
        GameController.Instance.admobAds.ShowRewardedAd(
                     actionReward: () =>
                     {
                         switch (currentGift)
                         {
                             case GiftType.MOVE_SIGHTING_POINT_BOOSTER:
                          

                                 UseProfile.NumbWatchAdsMoveSighPoin -= 1;
                                 if (UseProfile.NumbWatchAdsMoveSighPoin <= 0)
                                 {
                                     UseProfile.NumbWatchAdsMoveSighPoin = 3;
                                     HandleClaimGiftX1();

                                 }
                                 tvCountNumbAds.text = UseProfile.NumbWatchAdsMoveSighPoin.ToString() + "/3";
                                 break;
                             case GiftType.SNIPER_BOOSTER:
                                 UseProfile.NumbWatchAdsSniper -= 1;
                                 if (UseProfile.NumbWatchAdsSniper <= 0)
                                 {
                                     UseProfile.NumbWatchAdsSniper = 3;
                                     HandleClaimGiftX1();

                                 }
                                 tvCountNumbAds.text = UseProfile.NumbWatchAdsSniper.ToString() + "/3";

                                 break;
                             case GiftType.CLONE_BALLS_BOOSTER:

                                 UseProfile.NumbWatchCloneBall -= 1;
                                 if (UseProfile.NumbWatchCloneBall <= 0)
                                 {
                                     UseProfile.NumbWatchCloneBall = 3;
                                     HandleClaimGiftX1();

                                 }
                                 tvCountNumbAds.text = UseProfile.NumbWatchCloneBall.ToString() + "/3";
                                 break;
                         }
                            


                     },
                     actionNotLoadedVideo: () =>
                     {
                         GameController.Instance.moneyEffectController.SpawnEffectText_FlyUp_UI
                          (
                             payByAdsBtn.transform
                             ,
                          payByAdsBtn.transform.position,
                          "No video at the moment!",
                          Color.white,
                          isSpawnItemPlayer: true
                          );
                     },
             
                     actionWatchVideo );
    }   
    
    public void HandlePayByCoin()
    {
        GameController.Instance.musicManager.PlayClickSound();
        if (UseProfile.Coin >= price)
        {
            UseProfile.Coin -= price;      
            HandleClaimGift();
        }
        else
        {
            ShopBox.Setup(ButtonShopType.Gold).Show();

            
            //GameController.Instance.moneyEffectController.SpawnEffectText_FlyUp_UI
            //              (
            //                 payByCoinBtn.transform
            //                 ,
            //              payByCoinBtn.transform.position,
            //              "Not enough coin",
            //              Color.white,
            //              isSpawnItemPlayer: true
            //              );
        }    


    }  
    

    public void HandleClaimGift()
    {
   
         Close();
        GameController.Instance.dataContain.giftDatabase.Claim(currentGift, 1);
        List<GiftRewardShow> giftRewardShows = new List<GiftRewardShow>();
        giftRewardShows.Add(new GiftRewardShow() { amount = 1, type = currentGift });
        PopupRewardBase.Setup(false).Show(giftRewardShows, delegate {

            GamePlayController.Instance.playerContain.levelData.inputThone.enabled = true  ;
        });

    }
    public void HandleClaimGiftX1()
    {

    
        Close();
        GameController.Instance.dataContain.giftDatabase.Claim(currentGift, 1);
        List<GiftRewardShow> giftRewardShows = new List<GiftRewardShow>();
        giftRewardShows.Add(new GiftRewardShow() { amount = 1, type = currentGift });
        PopupRewardBase.Setup(false).Show(giftRewardShows, delegate {


            GamePlayController.Instance.playerContain.levelData.inputThone.enabled = true;
        });

    }
}
