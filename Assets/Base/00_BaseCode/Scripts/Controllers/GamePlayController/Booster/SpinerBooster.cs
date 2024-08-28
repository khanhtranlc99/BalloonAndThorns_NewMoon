using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SpinerBooster : MonoBehaviour
{
    public Button sniper_Btn;
    public Text tvNum;
    public GameObject objNum;
    public GameObject parentTvCoin;
    public GameObject lockIcon;
    public GameObject unLockIcon;
 
    public bool wasUseTNT_Booster;
    public Transform post;


    PlayerContain playerContain;
    public void Init(PlayerContain param)
    {
        playerContain = param;
        wasUseTNT_Booster = false;
        if (UseProfile.CurrentLevel >= 3)//5
        {

            //unLockIcon.gameObject.SetActive(true);
            lockIcon.gameObject.SetActive(false);
            HandleUnlock();
            //Debug.LogError("HandleUnlock");

        }
        else
        {
            //unLockIcon.gameObject.SetActive(false);
            lockIcon.gameObject.SetActive(true);
            objNum.SetActive(false);
            HandleLock();
            //Debug.LogError("HandleLock");
        }


        void HandleUnlock()
        {

            sniper_Btn.onClick.AddListener(HandleSniper_Booster);
            if (UseProfile.SniperBooster > 0)
            {
                objNum.SetActive(true);
                tvNum.text = UseProfile.SniperBooster.ToString();
                parentTvCoin.SetActive(false);
            }
            else
            {
                objNum.SetActive(false);
                tvNum.gameObject.SetActive(false);
                parentTvCoin.SetActive(true);
            }
            EventDispatcher.EventDispatcher.Instance.RegisterListener(EventID.SNIPER_BOOSTER, ChangeText);
        }
        void HandleLock()
        {


            sniper_Btn.onClick.AddListener(HandleLockBtn);
        }
    }

    public void HandleLockBtn()
    {
        GameController.Instance.musicManager.PlayClickSound();
        GameController.Instance.moneyEffectController.SpawnEffectText_FlyUp
                              (
                              sniper_Btn.transform.position,
                              "Unlock at level 3",
                              Color.white,
                              isSpawnItemPlayer: true
                              );
    }





    public void HandleSniper_Booster()
    {
        GameController.Instance.musicManager.PlayClickSound();
        if (UseProfile.SniperBooster >= 1)
        {
     
                UseProfile.SniperBooster -= 1;
            playerContain.HandleSpinerBooster();
            sniper_Btn.interactable = false;
            GamePlayController.Instance.TutSpinerBooster.NextTut();
        }
        else
        {
            SuggetBox.Setup(GiftType.SNIPER_BOOSTER).Show();
        }



    }

  



    public void ChangeText(object param)
    {
        tvNum.text = UseProfile.SniperBooster.ToString();
        if (UseProfile.SniperBooster > 0)
        {
            objNum.SetActive(true);
            tvNum.gameObject.SetActive(true);
            tvNum.text = UseProfile.SniperBooster.ToString();
            parentTvCoin.SetActive(false);
        }
        else
        {
            objNum.SetActive(false);
            tvNum.gameObject.SetActive(false);
            parentTvCoin.SetActive(true);
        }
    }
    public void OnDestroy()
    {
        EventDispatcher.EventDispatcher.Instance.RemoveListener(EventID.SNIPER_BOOSTER, ChangeText);
    }
}
