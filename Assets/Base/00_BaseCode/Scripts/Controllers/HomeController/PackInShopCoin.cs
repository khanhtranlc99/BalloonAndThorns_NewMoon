using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PackInShopCoin : PackInShop
{
    public int idSkin;
    public SkinType skinType;
    public int coinGift;
    public GiftType currentGift;
    public int numGift = 1;
    public ButtonShopController buttonShopController;
    public SkinShopController skinShopController;
    public GameObject iconCoin;
    public override void Init()
    {
       
    }
    public   void InitState(PackInShopCoinType packInShopCoin)
    {
        btnBuy.onClick.RemoveAllListeners();
        switch (packInShopCoin)
        {
            case PackInShopCoinType.Equid:
                iconCoin.SetActive(false);
                tvBuy.text = "Equipped";
                break;
            case PackInShopCoinType.WasBought:
                iconCoin.SetActive(false);
                btnBuy.onClick.AddListener(delegate { HandleWasBought(); });
                tvBuy.text = "Equip";
                break;
            case PackInShopCoinType.Buy:
                iconCoin.SetActive(true);
                btnBuy.onClick.AddListener(delegate { HandleBuy(); });
                tvBuy.text = "" + coinGift;
                break;
        }
    }




    private void HandleBuy()
    {
        GameController.Instance.musicManager.PlayClickSound();
        if (UseProfile.Coin >= coinGift)
        {
            UseProfile.Coin -= coinGift;

            List<GiftRewardShow> giftRewardShows = new List<GiftRewardShow>();
            giftRewardShows.Add(new GiftRewardShow() { amount = numGift, type = currentGift });
            foreach (var item in giftRewardShows)
            {
                GameController.Instance.dataContain.giftDatabase.Claim(item.type, item.amount);
            }
            PopupRewardBase.Setup(false).Show(giftRewardShows, delegate {   });
            switch (skinType)
            {
                case SkinType.Cannon:
                    skinShopController.lsIdCannons.Add(idSkin);
                    UseProfile.lsIdSkinCanons = skinShopController.lsIdCannons;
                    break;
                case SkinType.Ball:
                    skinShopController.lsIdBalls.Add(idSkin);
                    UseProfile.lsIdSkinBalls = skinShopController.lsIdBalls;
                    break;
            }
            HandleWasBought();
        }
        else
        {
            buttonShopController.HandleOnClick(ButtonShopType.Gold);
            btnBuy.transform.SetAsLastSibling();
            GameController.Instance.moneyEffectController.SpawnEffectText_FlyUp_UI
                       (
                          btnBuy.transform
                          ,
                       btnBuy.transform.position,
                       "More Coin",
                       Color.white,
                       isSpawnItemPlayer: true
                       );
        }    
    }    

    private void HandleWasBought()
    {
        switch(skinType)
        {
            case SkinType.Cannon:
                UseProfile.id_cannon_skin =  idSkin;
                break;
            case SkinType.Ball:
                UseProfile.id_ball_skin = idSkin;
                break;
        }
        skinShopController.InitState();
    }
 


}
public enum PackInShopCoinType
{
    Equid,
    WasBought,
    Buy
}
public enum SkinType
{
    Ball,
    Cannon,
 
}