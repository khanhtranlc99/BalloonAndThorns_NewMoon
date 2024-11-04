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
    Skin_Canon_1 = 11,
    Skin_Canon_2 = 12,
    Skin_Canon_3 = 13,
    Skin_Canon_4 = 14,
    Skin_Canon_5  = 15,
    Skin_Canon_6 = 16,
    Skin_Canon_7 = 17,
    Skin_Canon_8 = 18,
    Skin_Ball_1 = 19,
    Skin_Ball_2 = 20,
    Skin_Ball_3 = 21,
    Skin_Ball_4 = 22,
    Skin_Ball_5 = 23,
    Skin_Ball_6 = 24,
    Skin_Ball_7 = 25,
    Skin_Ball_8 = 26,
    Skin_Ball_9 = 27,
    Skin_Ball_10 = 28,
    Skin_Ball_11 = 29,


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
