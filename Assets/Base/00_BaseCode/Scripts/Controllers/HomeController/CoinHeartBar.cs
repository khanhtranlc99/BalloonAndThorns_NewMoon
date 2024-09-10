using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CoinHeartBar : MonoBehaviour
{
    public Text tvCoin;
 
    public Button btnCoin;
  
 

    public void Init ()
    {
        tvCoin.text = UseProfile.Coin.ToString();
      
        btnCoin.onClick.AddListener(delegate { GameController.Instance.musicManager.PlayClickSound(); ShopBox.Setup(ButtonShopType.Gold).Show(); });
      

        EventDispatcher.EventDispatcher.Instance.RegisterListener(EventID.CHANGE_COIN, HandleChangeCoin);
       
    }

    public void HandleChangeCoin(object param)
    {
        tvCoin.text = UseProfile.Coin.ToString();
    }
 

    public void OnDisable()
    {
        EventDispatcher.EventDispatcher.Instance.RemoveListener(EventID.CHANGE_COIN, HandleChangeCoin);
     

    }
    public void OnDestroy()
    {
        EventDispatcher.EventDispatcher.Instance.RemoveListener(EventID.CHANGE_COIN, HandleChangeCoin);
    
    }
     

}
