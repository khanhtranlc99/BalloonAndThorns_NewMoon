using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class ShopBox : BaseBox
{
    public static ShopBox instance;
    public static ShopBox Setup(ButtonShopType param = ButtonShopType.Gift, bool isSaveBox = false, Action actionOpenBoxSave = null)
    {
        if (instance == null)
        {
            instance = Instantiate(Resources.Load<ShopBox>(PathPrefabs.SHOP_BOX));
            instance.Init(param);
        }

        instance.InitState();
        return instance;
    }
    public ButtonShopController shopController;
    public List<PackInShop> lsPackInShops;
    public PackInShop GetPackShop(TypePackIAP paramtypePackIAP)
    {
        foreach(var item in lsPackInShops)
        {
            if(item.typePackIAP == paramtypePackIAP)
            {
                return item;
            }
        }
        return null;
    }    
    public List<PackFreeInShop> lsPackInShopFree;
    
    public float countTime;
    public bool wasCountTime;
    public Text tvCountTime;
    public Text tvCountTime_2;
 
    public CoinHeartBar coinHeartBar;
    public Button btnClose;
    public  GameObject paramPost;

   

    private void Init(ButtonShopType buttonShopType)
    {
        shopController.Init(buttonShopType);
        foreach(var item in lsPackInShops)
        {
            item.Init();
        }
        foreach (var item in lsPackInShopFree)
        {
            item.Init();
        }
        
        coinHeartBar.Init();
        btnClose.onClick.AddListener(delegate {
            GameController.Instance.musicManager.PlayClickSound();
            GamePlayController.Instance.playerContain.inputThone.enabled = true;
            Close(); });
        EventDispatcher.EventDispatcher.Instance.RegisterListener(EventID.SHOP_CHECK, CheckOffPack);
    }
    private void InitState()
    {
        ResetDay();
        CheckOffPack();
    }

    private void ResetDay()
    {
    
        wasCountTime = false;
        countTime  = TimeManager.TimeLeftPassTheDay(DateTime.Now);

         
        if (UseProfile.NeedCheckShop )
        {
            UseProfile.NeedCheckShop = false;
            foreach (var item in lsPackInShopFree)
            {
                item.HandleOn();
            }    

        }
        wasCountTime = true; 
    }
    private void Update()
    {
        if(wasCountTime)
        {
           if(countTime > 0)
            {
                countTime -=  1*Time.deltaTime;
                tvCountTime.text = "REFRESH IN : " + TimeManager.ShowTime2((long)countTime);
                tvCountTime_2.text = "REFRESH IN : " + TimeManager.ShowTime2((long)countTime);
           

            }
           else
            {
                tvCountTime.text = " "  ;
                tvCountTime_2.text = " "  ;
            }    
        }
    }

    public void CheckOffPack()
    {
        if(UseProfile.UnlimitScope)
        {
            GetPackShop(TypePackIAP.SniperPacks).btnBuy.interactable = false;

        }
        if (UseProfile.UnlimitSpike)
        {
            GetPackShop(TypePackIAP.SpikePacks).btnBuy.interactable = false;
        }
        if (GameController.Instance.useProfile.IsRemoveAds)
        {
            GetPackShop(TypePackIAP.RemoveAdsPacks).btnBuy.interactable = false;
        }
        if (UseProfile.UnlimitScope && UseProfile.UnlimitSpike)
        {
           
            GetPackShop(TypePackIAP.VipPacks).btnBuy.interactable = false;
        }
     
    }
    public void CheckOffPack(object param)
    {
        if (UseProfile.UnlimitScope)
        {
            GetPackShop(TypePackIAP.SniperPacks).btnBuy.interactable = false;
        }
        if (UseProfile.UnlimitSpike)
        {
            GetPackShop(TypePackIAP.SpikePacks).btnBuy.interactable = false;
        }
        if (UseProfile.UnlimitScope && UseProfile.UnlimitSpike)
        {
            GetPackShop(TypePackIAP.VipPacks).btnBuy.interactable = false;
        }
       
    }
   
    private void OnDestroy()
    {
        EventDispatcher.EventDispatcher.Instance.RemoveListener(EventID.SHOP_CHECK, CheckOffPack);
    }
}
