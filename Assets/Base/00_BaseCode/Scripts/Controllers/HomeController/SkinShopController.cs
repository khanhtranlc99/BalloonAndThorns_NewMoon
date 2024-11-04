using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class SkinShopController : MonoBehaviour
{
    public List<PackInShopCoin> lsCannonSkin;
    public List<PackInShopCoin> lsBallSkin;
    public List<int> lsIdBalls;
    public List<int> lsIdCannons;

    public void InitState()
    {
        lsIdBalls = UseProfile.lsIdSkinBalls;
        lsIdCannons = UseProfile.lsIdSkinCanons;
        foreach (var item in lsBallSkin)
        {
            if (lsIdBalls.Contains(item.idSkin))
            {
                item.InitState(PackInShopCoinType.WasBought);
            }
            else
            {
                item.InitState(PackInShopCoinType.Buy);
            }
            if (item.idSkin == UseProfile.id_ball_skin)
            {
                item.InitState(PackInShopCoinType.Equid);
            }
        }
        foreach (var item in lsCannonSkin)
        {
            if (lsIdCannons.Contains(item.idSkin))
            {
                item.InitState(PackInShopCoinType.WasBought);
            }
            else
            {
                item.InitState(PackInShopCoinType.Buy);
            }
            if (item.idSkin == UseProfile.id_cannon_skin)
            {
                item.InitState(PackInShopCoinType.Equid);
            }
        }
    }

}
