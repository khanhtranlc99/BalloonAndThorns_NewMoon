using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CloneBallsBooster : MonoBehaviour
{
    public Button cloneBallsBooster_Btn;
    public Text tvNum;
    public GameObject objNum;
    public GameObject parentTvCoin;
    public GameObject lockIcon;
    public GameObject unLockIcon;

    public Transform post;

   
    public GameObject parent;
    public bool wasUseFreezeBooster;
    PlayerContain playerContain;
    public void Init(PlayerContain param)
    {
        playerContain = param;

        wasUseFreezeBooster = false;
        if (UseProfile.CurrentLevel >= 5)//7
        {

            //unLockIcon.gameObject.SetActive(true);
            lockIcon.gameObject.SetActive(false);
            HandleUnlock();


        }
        else
        {
            //unLockIcon.gameObject.SetActive(false);
            lockIcon.gameObject.SetActive(true);
            objNum.SetActive(false);
            HandleLock();

        }


        void HandleUnlock()
        {
            cloneBallsBooster_Btn.onClick.AddListener(HandleFreezeBooster);
            if (UseProfile.CloneBallsBooster > 0)
            {
                objNum.SetActive(true);
                tvNum.text = UseProfile.CloneBallsBooster.ToString();
                parentTvCoin.SetActive(false);
            }
            else
            {
                objNum.SetActive(false);
                tvNum.gameObject.SetActive(false);
                parentTvCoin.SetActive(true);
            }
            EventDispatcher.EventDispatcher.Instance.RegisterListener(EventID.CLONE_BALLS_BOOSTER, ChangeText);
        }
        void HandleLock()
        {
            cloneBallsBooster_Btn.onClick.AddListener(HandleLockBtn);
        }
    }

    public void HandleLockBtn()
    {
        GameController.Instance.musicManager.PlayClickSound();
        GameController.Instance.moneyEffectController.SpawnEffectText_FlyUp
                              (
                              cloneBallsBooster_Btn.transform.position,
                              "Unlock at level 7",
                              Color.white,
                              isSpawnItemPlayer: true
                              );
    }





    public void HandleFreezeBooster()
    {
        GameController.Instance.musicManager.PlayClickSound();
        if (UseProfile.CloneBallsBooster >= 1)
        {
       
      
        }
        else
        {
            SuggetBox.Setup(GiftType.CLONE_BALLS_BOOSTER).Show();
        }

    }

 

    public void ChangeText(object param)
    {
        tvNum.text = UseProfile.CloneBallsBooster.ToString();
        if (UseProfile.CloneBallsBooster > 0)
        {
            objNum.SetActive(true);
            tvNum.gameObject.SetActive(true);
            tvNum.text = UseProfile.CloneBallsBooster.ToString();
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
        EventDispatcher.EventDispatcher.Instance.RemoveListener(EventID.CLONE_BALLS_BOOSTER, ChangeText);
    }
}
