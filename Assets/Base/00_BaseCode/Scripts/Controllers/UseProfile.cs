using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MoreMountains.NiceVibrations;
using Newtonsoft.Json;

public class UseProfile : MonoBehaviour
{
    public static bool NeedCheckShop
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.NEED_CHECK_SHOP, 0) == 1;
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.NEED_CHECK_SHOP, value ? 1 : 0);
            PlayerPrefs.Save();
        }
    }
    public static bool WasBoughtUnlimitTime
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.WAS_BOUGHT_UNLIMIT_TIME, 0) == 1;
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.WAS_BOUGHT_UNLIMIT_TIME, value ? 1 : 0);
            PlayerPrefs.Save();
        }
    }
    public static bool FirstShowOpenAds
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.FIRST_SHOW_OPEN_ADS, 0) == 1;
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.FIRST_SHOW_OPEN_ADS, value ? 1 : 0);
            PlayerPrefs.Save();
        }
    }
    public static bool FirstLoading
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.LOADING_COMPLETE, 0) == 1;
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.LOADING_COMPLETE, value ? 1 : 0);    
            PlayerPrefs.Save();
        }
    }

    public static int CurrentLevel_Chapper_I
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.CURRENT_LEVEL_CHAPER_I, 1);
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.CURRENT_LEVEL_CHAPER_I, value);
            PlayerPrefs.Save();
        }
    }
    public static int CurrentLevel_Chapper_II
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.CURRENT_LEVEL_CHAPER_II, 1);
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.CURRENT_LEVEL_CHAPER_II, value);
            PlayerPrefs.Save();
        }
    }
    public static int CurrentLevel 
    {
        get
        {
        
            return PlayerPrefs.GetInt(StringHelper.CURRENT_LEVEL, 1);
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.CURRENT_LEVEL, value);
            PlayerPrefs.Save();
        }
    }
    public static int NumbWatchAdsSniper
    {
        get
        {
            if (PlayerPrefs.GetInt(StringHelper.NUMB_WATCH_ADS_SNIPER, 3) <= 0)
            {
                return 0;
            }
            return PlayerPrefs.GetInt(StringHelper.NUMB_WATCH_ADS_SNIPER, 3);
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.NUMB_WATCH_ADS_SNIPER, value);
            PlayerPrefs.Save();
        }
    }
    public static int NumbWatchAdsMoveSighPoin
    {
        get
        {
            if (PlayerPrefs.GetInt(StringHelper.NUMB_WATCH_ADS_MOVESIGHPOIN, 3) <= 0)
            {
                return 0;
            }
            return PlayerPrefs.GetInt(StringHelper.NUMB_WATCH_ADS_MOVESIGHPOIN, 3);
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.NUMB_WATCH_ADS_MOVESIGHPOIN, value);
            PlayerPrefs.Save();
        }
    }
    public static int NumbWatchCloneBall
    {
        get
        {
            if (PlayerPrefs.GetInt(StringHelper.NUMB_WATCH_ADS_CLONEBALL, 3) <= 0)
            {
                return 0;
            }
            return PlayerPrefs.GetInt(StringHelper.NUMB_WATCH_ADS_CLONEBALL, 3);
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.NUMB_WATCH_ADS_CLONEBALL, value);
            PlayerPrefs.Save();
        }
    }



    public static int NumbWatchAdsCoin
    {
        get
        {
            if (PlayerPrefs.GetInt(StringHelper.NUMB_WATCH_ADS_COIN, 3) <= 0)
            {
                return 0;
            }
            return PlayerPrefs.GetInt(StringHelper.NUMB_WATCH_ADS_COIN, 3);
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.NUMB_WATCH_ADS_COIN, value);
            PlayerPrefs.Save();
        }
    }
   
    public static DateTime TimeLastOverHealth
    {
        get
        {
            if (PlayerPrefs.HasKey(StringHelper.TIME_LAST_OVER_HEALTH))
            {
                var temp = Convert.ToInt64(PlayerPrefs.GetString(StringHelper.TIME_LAST_OVER_HEALTH));
                return DateTime.FromBinary(temp);
            }
            else
            {
                var newDateTime = DateTime.Now;
                PlayerPrefs.SetString(StringHelper.TIME_LAST_OVER_HEALTH, newDateTime.ToBinary().ToString());
                PlayerPrefs.Save();
                return newDateTime;
            }
        }
        set
        {
            PlayerPrefs.SetString(StringHelper.TIME_LAST_OVER_HEALTH, value.ToBinary().ToString());
            PlayerPrefs.Save();
        }
    }
 
    public static int Coin
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.COIN, 0);
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.COIN, value);
            PlayerPrefs.Save();
            EventDispatcher.EventDispatcher.Instance.PostEvent(EventID.CHANGE_COIN);
        }
    }
    public static int Heart
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.HEART, 5);
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.HEART, value);
            PlayerPrefs.Save();
        }
    }

    public static int DestroyScewBooster
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.REDO_BOOSTER, 0);
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.REDO_BOOSTER, value);
            PlayerPrefs.Save();
         
        }
    }
    public static int DrillBooster
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.SUPORT_BOOSTER, 0);
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.SUPORT_BOOSTER, value);
            PlayerPrefs.Save();

        }
    }
    public static int MoveSightingPointBooster
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.MOVE_SIGHTING_POINT_BOOSTER, 3);
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.MOVE_SIGHTING_POINT_BOOSTER, value);
            PlayerPrefs.Save();
            EventDispatcher.EventDispatcher.Instance.PostEvent(EventID.MOVE_SIGHTING_POINT_BOOSTER);
        }
    }
    public static int SniperBooster
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.SNIPER_BOOSTER, 3);
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.SNIPER_BOOSTER, value);
            PlayerPrefs.Save();
            EventDispatcher.EventDispatcher.Instance.PostEvent(EventID.SNIPER_BOOSTER);
        }
    }
    public static int CloneBallsBooster
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.CLONE_BALLS_BOOSTER, 3);
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.CLONE_BALLS_BOOSTER, value);
            PlayerPrefs.Save();
            EventDispatcher.EventDispatcher.Instance.PostEvent(EventID.CLONE_BALLS_BOOSTER);

        }
    }
    public static int RocketBooster
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.ROCKET_BOOSTER, 3);
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.ROCKET_BOOSTER, value);
            PlayerPrefs.Save();
            EventDispatcher.EventDispatcher.Instance.PostEvent(EventID.ROCKET_BOOSTER);
        }
    }
    public static int WinStreak
    {
        get
        {
            if (PlayerPrefs.GetInt(StringHelper.WINSTREAK, 0) >= 5)
            {
                return 5;
            }
            return PlayerPrefs.GetInt(StringHelper.WINSTREAK, 0);
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.WINSTREAK, value);
            PlayerPrefs.Save();
        
        }
    }
    public static int FlameUp_Item
    {
        get
        {
            if(PlayerPrefs.GetInt(StringHelper.FLAMEUP_ITEM, 0) >= 2)
            {
                return 2;
            }
            return PlayerPrefs.GetInt(StringHelper.FLAMEUP_ITEM, 0);
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.FLAMEUP_ITEM, value);
            PlayerPrefs.Save();
            EventDispatcher.EventDispatcher.Instance.PostEvent(EventID.FLAMEUP_ITEM);
        }
    }
    public static int FastBoom_Item
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.FASTBOOM_ITEM, 0);
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.FASTBOOM_ITEM, value);
            PlayerPrefs.Save();
            EventDispatcher.EventDispatcher.Instance.PostEvent(EventID.FASTBOOM_ITEM);
        }
    }
    public static int id_ball_skin
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.ID_BALL_SKIN, 0);
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.ID_BALL_SKIN, value);
            PlayerPrefs.Save();
            EventDispatcher.EventDispatcher.Instance.PostEvent(EventID.CHANGE_ID_BALL);
        }
    }
    public static int id_cannon_skin
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.ID_CANNON_SKIN, 0);
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.ID_CANNON_SKIN, value);
            PlayerPrefs.Save();
            EventDispatcher.EventDispatcher.Instance.PostEvent(EventID.CHANGE_ID_CANNON);
        }
    }

    public static List<int> lsIdSkinBalls
    {
        get
        {
            List<int> tempList = new List<int>();
            var temp = JsonConvert.DeserializeObject<List<int>>( PlayerPrefs.GetString(StringHelper.LS_ID_SKIN_BALLS ));
            if(temp == null)
            {
                string jsonValue = JsonConvert.SerializeObject(new List<int>() { 0 });
                PlayerPrefs.SetString(StringHelper.LS_ID_SKIN_BALLS, jsonValue);
            }
            else
            { 
           
                foreach (var item in temp)
                {
                    tempList.Add(item);
                }
            }
       
            return tempList;

        }
        set
        {
            string jsonValue = JsonConvert.SerializeObject(value);
            PlayerPrefs.SetString(StringHelper.LS_ID_SKIN_BALLS, jsonValue);
            PlayerPrefs.Save();
        }
    }
    public static List<int> lsIdSkinCanons
    {
        get
        {
            List<int> tempList = new List<int>();
            var temp = JsonConvert.DeserializeObject<List<int>>(PlayerPrefs.GetString(StringHelper.LS_ID_SKIN_CANNONS));
            if (temp == null)
            {
                string jsonValue = JsonConvert.SerializeObject(new List<int>() { 0 });
                PlayerPrefs.SetString(StringHelper.LS_ID_SKIN_CANNONS, jsonValue);
            }
            else
            {

                foreach (var item in temp)
                {
                    tempList.Add(item);
                }
            }
            return tempList;

        }
        set
        {
            string jsonValue = JsonConvert.SerializeObject(value);
            PlayerPrefs.SetString(StringHelper.LS_ID_SKIN_CANNONS, jsonValue);
            PlayerPrefs.Save();
        }
    }
    public static string DataCardSave
    {
        get
        {
           return PlayerPrefs.GetString(StringHelper.LS_ID_SKIN_CANNONS);
        }
        set
        {
            PlayerPrefs.SetString(StringHelper.LS_ID_SKIN_CANNONS, value);
        }
    }
    public static bool isSaveDataCard
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.IS_SAVE_DATA_CARD, 0) == 1;
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.IS_SAVE_DATA_CARD, value ? 1 : 0);
            PlayerPrefs.Save();
        }
    }
    public static bool UnlimitScope
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.UNLIMIT_SCOPE, 0) == 1;
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.UNLIMIT_SCOPE, value ? 1 : 0);
            PlayerPrefs.Save();
        }
    }
    public static bool UnlimitSpike
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.UNLIMIT_SPIKE, 0) == 1;
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.UNLIMIT_SPIKE, value ? 1 : 0);
            PlayerPrefs.Save();
        }
    }


    public static int NumberOfDisplayedInterstitialD0_D1
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.NUMBER_OF_DISPLAYED_INTERST_ITIAL_D0_D1_KEY, 0);
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.NUMBER_OF_DISPLAYED_INTERST_ITIAL_D0_D1_KEY, value);
            PlayerPrefs.Save();

        }
    }

    public static int NumberOfDisplayedInterstitialD1
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.NUMBER_OF_DISPLAYED_INTERST_ITIAL_D1_KEY, 0);
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.NUMBER_OF_DISPLAYED_INTERST_ITIAL_D1_KEY, value);
            PlayerPrefs.Save();

        }
    }

    public static int NumberRewardShowed
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.NUMBER_REWARD_SHOWED, 0);
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.NUMBER_REWARD_SHOWED, value);
            PlayerPrefs.Save();

        }
    }
    public static int NumberInterShowed
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.NUMBER_INTER_SHOWED, 0);
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.NUMBER_INTER_SHOWED, value);
            PlayerPrefs.Save();

        }
    }

    public int LevelUnlock
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.CURRENT_LEVEL_PLAY, 1);
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.CURRENT_LEVEL_PLAY, value);
            PlayerPrefs.Save();
        }
    }
    public bool IsRemoveAds
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.REMOVE_ADS, 0) == 1;
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.REMOVE_ADS, value ? 1 : 0);
            PlayerPrefs.Save();
        }
    }
    public bool OnVibration
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.ONOFF_VIBRATION, 1) == 1;
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.ONOFF_VIBRATION, value ? 1 : 0);
            MMVibrationManager.SetHapticsActive(value);
            PlayerPrefs.Save();
        }
    }
    public bool OnSound
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.ONOFF_SOUND, 1) == 1;
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.ONOFF_SOUND, value ? 1 : 0);
            GameController.Instance.musicManager.SetSoundVolume(value ? 1 : 0);
            PlayerPrefs.Save();
        }
    }
    public bool OnMusic
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.ONOFF_MUSIC, 1) == 1;
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.ONOFF_MUSIC, value ? 1 : 0);
            GameController.Instance.musicManager.SetMusicVolume(value ? 0.15f : 0);
            PlayerPrefs.Save();
        }
    }
    public static bool IsFirstTimeInstall
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.FIRST_TIME_INSTALL, 1) == 1;
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.FIRST_TIME_INSTALL, value ? 1 : 0);
            PlayerPrefs.Save();
        }
    }
    public static int RetentionD
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.RETENTION_D, 0);
        }
        set
        {
            if (value < 0)
                value = 0;

            PlayerPrefs.SetInt(StringHelper.RETENTION_D, value);
            PlayerPrefs.Save();
        }
    }
    public static int DaysPlayed
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.DAYS_PLAYED, 1);
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.DAYS_PLAYED, value);
            PlayerPrefs.Save();
        }
    }
    public static int PayingType
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.PAYING_TYPE, 0);
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.PAYING_TYPE, value);
            PlayerPrefs.Save();
        }
    }
    public static DateTime FirstTimeOpenGame
    {
        get
        {
            if (PlayerPrefs.HasKey(StringHelper.FIRST_TIME_OPEN_GAME))
            {
                var temp = Convert.ToInt64(PlayerPrefs.GetString(StringHelper.FIRST_TIME_OPEN_GAME));
                return DateTime.FromBinary(temp);
            }
            else
            {
                var newDateTime = UnbiasedTime.Instance.Now().AddDays(-1);
                PlayerPrefs.SetString(StringHelper.FIRST_TIME_OPEN_GAME, newDateTime.ToBinary().ToString());
                PlayerPrefs.Save();
                return newDateTime;
            }
        }
        set
        {
            PlayerPrefs.SetString(StringHelper.FIRST_TIME_OPEN_GAME, value.ToBinary().ToString());
            PlayerPrefs.Save();
        }
    }
    public static DateTime LastTimeOpenGame
    {
        get
        {
            if (PlayerPrefs.HasKey(StringHelper.LAST_TIME_OPEN_GAME))
            {
                var temp = Convert.ToInt64(PlayerPrefs.GetString(StringHelper.LAST_TIME_OPEN_GAME));
                return DateTime.FromBinary(temp);
            }
            else
            {
                var newDateTime = UnbiasedTime.Instance.Now().AddDays(-1);
                PlayerPrefs.SetString(StringHelper.LAST_TIME_OPEN_GAME, newDateTime.ToBinary().ToString());
                PlayerPrefs.Save();
                return newDateTime;
            }
        }
        set
        {
            PlayerPrefs.SetString(StringHelper.LAST_TIME_OPEN_GAME, value.ToBinary().ToString());
            PlayerPrefs.Save();
        }
    }
    public static DateTime LastTimeResetSalePackShop
    {
        get
        {
            if (PlayerPrefs.HasKey(StringHelper.LAST_TIME_RESET_SALE_PACK_SHOP))
            {
                var temp = Convert.ToInt64(PlayerPrefs.GetString(StringHelper.LAST_TIME_RESET_SALE_PACK_SHOP));
                return DateTime.FromBinary(temp);
            }
            else
            {
                var newDateTime = UnbiasedTime.Instance.Now().AddDays(-1);
                PlayerPrefs.SetString(StringHelper.LAST_TIME_RESET_SALE_PACK_SHOP, newDateTime.ToBinary().ToString());
                PlayerPrefs.Save();
                return newDateTime;
            }
        }
        set
        {
            PlayerPrefs.SetString(StringHelper.LAST_TIME_RESET_SALE_PACK_SHOP, value.ToBinary().ToString());
            PlayerPrefs.Save();
        }
    }
    public static bool CanShowRate
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.CAN_SHOW_RATE, 1) == 1;
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.CAN_SHOW_RATE, value ? 1 : 0);
            PlayerPrefs.Save();
        }
    }
    public static int NumberOfAdsInPlay;
    public static int NumberOfAdsInDay
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.NUMBER_OF_ADS_IN_DAY, 0);
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.NUMBER_OF_ADS_IN_DAY, value);
            PlayerPrefs.Save();
        }
    }
    public static DateTime LastTimeOnline
    {
        get
        {
            if (PlayerPrefs.HasKey(StringHelper.LAST_TIME_ONLINE))
            {
                var temp = Convert.ToInt64(PlayerPrefs.GetString(StringHelper.LAST_TIME_ONLINE));
                return DateTime.FromBinary(temp);
            }
            else
            {
                var newDateTime = DateTime.Now.AddDays(-1);
                PlayerPrefs.SetString(StringHelper.LAST_TIME_ONLINE, newDateTime.ToBinary().ToString());
                PlayerPrefs.Save();
                return newDateTime;
            }
        }
        set
        {
            PlayerPrefs.SetString(StringHelper.LAST_TIME_ONLINE, value.ToBinary().ToString());
            PlayerPrefs.Save();
        }
    }
    public static DateTime TimeUnlimitHeart
    {
        get
        {
            if (PlayerPrefs.HasKey(StringHelper.TIME_UNLIMIT_HEART))
            {
                var temp = Convert.ToInt64(PlayerPrefs.GetString(StringHelper.TIME_UNLIMIT_HEART));
                return DateTime.FromBinary(temp);
            }
            else
            {
                var newDateTime = DateTime.Now.AddDays(-1);
                PlayerPrefs.SetString(StringHelper.TIME_UNLIMIT_HEART, newDateTime.ToBinary().ToString());
                PlayerPrefs.Save();
                return newDateTime;
            }
        }
        set
        {
            PlayerPrefs.SetString(StringHelper.TIME_UNLIMIT_HEART, value.ToBinary().ToString());
            PlayerPrefs.Save();
        }
    }

    public static bool isUnlimitHeart
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.IS_UNLIMIT_HEART, 0) == 1;
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.IS_UNLIMIT_HEART, value ? 1 : 0);
            PlayerPrefs.Save();
        }
    }

    public string ListSave
    {
        get
        {
            return PlayerPrefs.GetString(GameData.LEVEL_SAVE);
        }
        set
        {
            PlayerPrefs.SetString(GameData.LEVEL_SAVE, value);
            PlayerPrefs.Save();
        }
    }
    public void SetDateTimeReciveDailyGift(DateTime value)
    {
        PlayerPrefs.SetString(StringHelper.DATE_RECIVE_GIFT_DAILY, value.ToBinary().ToString());
    }
    public DateTime GetDateTimeReciveDailyGift()
    {
        return GetDateTime(StringHelper.DATE_RECIVE_GIFT_DAILY, DateTime.MinValue);
    }
    public DateTime GetDateTime(string key, DateTime defaultValue)
    {
        string @string = PlayerPrefs.GetString(key);
        DateTime result = defaultValue;
        if (!string.IsNullOrEmpty(@string))
        {
            long dateData = Convert.ToInt64(@string);
            result = DateTime.FromBinary(dateData);
        }
        return result;
    }
   


}


