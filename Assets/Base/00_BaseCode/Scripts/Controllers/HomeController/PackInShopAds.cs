using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class PackInShopAds : PackInShop
{
  
    public GiftType currentGift;
    ActionWatchVideo actionWatchVideo;
    public GameObject decorBtn;
    public int numGift = 1;
 
   
    //public  bool WasWatch
    //{
    //    get
    //    {

    //        return PlayerPrefs.GetInt("WasWatchPackInShopAds"   + currentGift.ToString(), 0) == 1;
    //    }
    //    set
    //    {
    //        PlayerPrefs.SetInt("WasWatchPackInShopAds" + currentGift.ToString(), value ? 1 : 0);
    //        PlayerPrefs.Save();
    //    }
    //}
    public override void Init()
    {
        btnBuy.onClick.AddListener(HandleOnClick);
        ShowCount();
    }
    private void ShowCount()
    {
        switch (currentGift)
        {
            case GiftType.MOVE_SIGHTING_POINT_BOOSTER:

                tvBuy.text = UseProfile.NumbWatchAdsMoveSighPoin.ToString() + "/3";
         
                break;
            case GiftType.SNIPER_BOOSTER:
                tvBuy.text = UseProfile.NumbWatchAdsSniper.ToString() + "/3";
         
                break;
            case GiftType.CLONE_BALLS_BOOSTER:
                tvBuy.text = UseProfile.NumbWatchCloneBall.ToString() + "/3";
  

                break;
            case GiftType.Coin:

                tvBuy.text = UseProfile.NumbWatchAdsCoin.ToString() + "/3";
        
                break;
        }
    }    
    private void HandleAfterWatchVideo()
    {
        switch (currentGift)
        {
            case GiftType.SNIPER_BOOSTER:
                UseProfile.NumbWatchAdsSniper -= 1;
                if (UseProfile.NumbWatchAdsSniper <= 0)
                {
                    Claim(delegate { UseProfile.NumbWatchAdsSniper = 3; ShowCount();   });
                }    


                break;
            case GiftType.MOVE_SIGHTING_POINT_BOOSTER:
                UseProfile.NumbWatchAdsMoveSighPoin -= 1;
                if (UseProfile.NumbWatchAdsMoveSighPoin <= 0)
                {
                    Claim(delegate { UseProfile.NumbWatchAdsMoveSighPoin = 3; ShowCount(); });
                }

                break;
            case GiftType.CLONE_BALLS_BOOSTER:
                UseProfile.NumbWatchCloneBall -= 1;
                if (UseProfile.NumbWatchCloneBall <= 0)
                {
                    Claim(delegate { UseProfile.NumbWatchCloneBall = 3; ShowCount(); });
                }

                break;
            case GiftType.Coin:

                UseProfile.NumbWatchAdsCoin -= 1;
                if (UseProfile.NumbWatchAdsCoin <= 0)
                {
                    Claim(delegate { UseProfile.NumbWatchAdsCoin = 3; ShowCount(); });
                }

                break;
        }
        ShowCount();
    }    

    private void HandleOnClick()
    {
        switch (currentGift)
        {
            case GiftType.MOVE_SIGHTING_POINT_BOOSTER:

                actionWatchVideo = ActionWatchVideo.MOVE_SIGHTING_POINT_BOOSTER;
                break;
            case GiftType.SNIPER_BOOSTER:

                actionWatchVideo = ActionWatchVideo.Sniper_Booster;
                break;
            case GiftType.CLONE_BALLS_BOOSTER:

                actionWatchVideo = ActionWatchVideo.CloneBall_Booster;
                break;
            case GiftType.ROCKET_BOOSTER:

                actionWatchVideo = ActionWatchVideo.Rocket_Booster;
                break;
        }
        GameController.Instance.musicManager.PlayClickSound();

        GameController.Instance.admobAds.ShowVideoReward(
                actionReward: () =>
                {


                    HandleAfterWatchVideo();

                },
                actionNotLoadedVideo: () =>
                {
                    btnBuy.transform.SetAsLastSibling();
                    GameController.Instance.moneyEffectController.SpawnEffectText_FlyUp_UI
                     (
                        btnBuy.transform
                        ,
                     btnBuy.transform.position,
                     "No video",
                     Color.white,
                     isSpawnItemPlayer: true
                     );
                },
                actionClose: null,
                actionWatchVideo,
                UseProfile.CurrentLevel.ToString());
    }
      


        void Claim(Action callBack)
        {
        
            List<GiftRewardShow> giftRewardShows = new List<GiftRewardShow>();
            giftRewardShows.Add(new GiftRewardShow() { amount = numGift, type = currentGift });
            foreach(var item in giftRewardShows)
            {
                GameController.Instance.dataContain.giftDatabase.Claim(item.type, item.amount);
            }    
            PopupRewardBase.Setup(false).Show(giftRewardShows, delegate { callBack?.Invoke(); });
      
          
        }



     
    }    

 