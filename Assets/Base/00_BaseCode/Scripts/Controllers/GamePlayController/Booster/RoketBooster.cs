using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RoketBooster : MonoBehaviour
{
    public Button RoketBooster_Btn;
    public Text tvNum;
    public GameObject objNum;
    public GameObject parentTvCoin;
    public GameObject lockIcon;
    public GameObject unLockIcon;

     
    public bool wasUseTNT_Booster;

    public Transform post;
    GameObject selectedObject;
     public GameObject panelTut;
    public CanvasGroup canvasGroup;
    PlayerContain playerContain;
    public void Init(PlayerContain param)
    {
        playerContain = param;
        selectedObject = null;
        wasUseTNT_Booster = false;
        if (UseProfile.CurrentLevel_Chapper_I >= 9)//9
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
            RoketBooster_Btn.onClick.AddListener(HandleAtom_Booster);
            if (UseProfile.RocketBooster > 0)
            {
                objNum.SetActive(true);
                tvNum.text = UseProfile.RocketBooster.ToString();
                parentTvCoin.SetActive(false);
            }
            else
            {
                objNum.SetActive(false);
                tvNum.gameObject.SetActive(false);
                parentTvCoin.SetActive(true);
            }
            EventDispatcher.EventDispatcher.Instance.RegisterListener(EventID.ROCKET_BOOSTER, ChangeText);
        }
        void HandleLock()
        {


            RoketBooster_Btn.onClick.AddListener(HandleLockBtn);
        }
    }

    public void HandleLockBtn()
    {
        GameController.Instance.musicManager.PlayClickSound();
        GameController.Instance.moneyEffectController.SpawnEffectText_FlyUp
                              (
                              RoketBooster_Btn.transform.position,
                              "Unlock at level 9",
                              Color.white,
                              isSpawnItemPlayer: true
                              );
    }





    public void HandleAtom_Booster()
    {
        GameController.Instance.musicManager.PlayClickSound();
        if (UseProfile.RocketBooster >= 1)
        {

       
            UseProfile.RocketBooster -= 1;
            playerContain.HandleRoketBooster(RoketBooster_Btn.gameObject.transform, delegate { RoketBooster_Btn.interactable = true; });
            wasUseTNT_Booster = true;
            RoketBooster_Btn.interactable = false;
       
        }
        else
        {
            SuggetBox.Setup(GiftType.ROCKET_BOOSTER).Show();
        }

    }


    

    public void ChangeText(object param)
    {
        tvNum.text = UseProfile.RocketBooster.ToString();
        if (UseProfile.RocketBooster > 0)
        {
            objNum.SetActive(true);
            tvNum.gameObject.SetActive(true);
            tvNum.text = UseProfile.RocketBooster.ToString();
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
        EventDispatcher.EventDispatcher.Instance.RemoveListener(EventID.ROCKET_BOOSTER, ChangeText);
    }
}
