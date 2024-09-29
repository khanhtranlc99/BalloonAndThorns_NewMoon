using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

[CreateAssetMenu(menuName = "Datas/GiftDatabase", fileName = "GiftDatabase.asset")]
public class GiftDatabase : SerializedScriptableObject
{
    public Dictionary<GiftType, Gift> giftList;

    public bool GetGift(GiftType giftType, out Gift gift)
    {
        return giftList.TryGetValue(giftType, out gift);
    }

    public Sprite GetIconItem(GiftType giftType)
    {
        Gift gift = null;
        //if (IsCharacter(giftType))
        //{
        //    var Char = GameController.Instance.dataContain.dataSkins.GetSkinInfo(giftType);
        //    if (Char != null)
        //        return Char.iconSkin;
        //}
        bool isGetGift = GetGift(giftType, out gift);
        return isGetGift ? gift.getGiftSprite : null;
    }
    public GameObject GetAnimItem(GiftType giftType)
    {
        Gift gift = null;
        bool isGetGift = GetGift(giftType, out gift);
        return isGetGift ? gift.getGiftAnim : null;
    }

    public void Claim(GiftType giftType, int amount, Reason reason = Reason.none)
    {

        switch (giftType)
        {
            case GiftType.Coin:
                UseProfile.Coin += amount;
                EventDispatcher.EventDispatcher.Instance.PostEvent(EventID.CHANGE_COIN);
                break;
            case GiftType.SPIKE_IN_GAME:
              
                break;
            case GiftType.RemoveAds:
                GameController.Instance.useProfile.IsRemoveAds = true;
           
                break;
            case GiftType.MOVE_SIGHTING_POINT_BOOSTER:
                UseProfile.MoveSightingPointBooster += amount;
                EventDispatcher.EventDispatcher.Instance.PostEvent(EventID.MOVE_SIGHTING_POINT_BOOSTER);

                break;
            case GiftType.SNIPER_BOOSTER:
                UseProfile.SniperBooster += amount;
                EventDispatcher.EventDispatcher.Instance.PostEvent(EventID.SNIPER_BOOSTER);
                break;
            case GiftType.CLONE_BALLS_BOOSTER:
                UseProfile.CloneBallsBooster += amount;
                EventDispatcher.EventDispatcher.Instance.PostEvent(EventID.CLONE_BALLS_BOOSTER);
                break;

            case GiftType.ROCKET_BOOSTER:
                UseProfile.RocketBooster += amount;
                EventDispatcher.EventDispatcher.Instance.PostEvent(EventID.ROCKET_BOOSTER);
                break;

            case GiftType.UnlimitSniper:

                UseProfile.UnlimitScope = true;
          
                break;
            case GiftType.UnlimitSpike:
                UseProfile.UnlimitSpike = true;
                GamePlayController.Instance.playerContain.levelData.HandleUnlimitSpike();
                break;
        }
    }

    public static bool IsCharacter(GiftType giftType)
    {
        //switch (giftType)
        //{
        //    case GiftType.RandomSkin:
        //        return true;
        //}
        return false;
    }
}

public class Gift
{
    [SerializeField] private Sprite giftSprite;
    [SerializeField] private GameObject giftAnim;
    public virtual Sprite getGiftSprite => giftSprite;
    public virtual GameObject getGiftAnim => giftAnim;

}

public enum GiftType
{
    None = 0,
    RemoveAds = 1,
    Coin = 2,
 
    MOVE_SIGHTING_POINT_BOOSTER = 4,
    SNIPER_BOOSTER = 5,
    CLONE_BALLS_BOOSTER = 6,
    ROCKET_BOOSTER = 7,
 
    SPIKE_IN_GAME = 8,
    
    UnlimitSniper = 9,
    UnlimitSpike = 10,
}

public enum Reason
{
    none = 0,
    play_with_item = 1,
    watch_video_claim_item_main_home = 2,
    daily_login = 3,
    lucky_spin = 4,
    unlock_skin_in_special_gift = 5,
    reward_accumulate = 6,
}

[Serializable]
public class RewardRandom
{
    public int id;
    public GiftType typeItem;
    public int amount;
    public int weight;

    public RewardRandom()
    {
    }
    public RewardRandom(GiftType item, int amount, int weight = 0)
    {
        this.typeItem = item;
        this.amount = amount;
        this.weight = weight;
    }

    public GiftRewardShow GetReward()
    {
        GiftRewardShow rew = new GiftRewardShow();
        rew.type = typeItem;
        rew.amount = amount;

        return rew;
    }
}
