﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MoveSightingPointBooster : MonoBehaviour
{
    public Button moveSightingPoint_Booster;
    public Text tvNum;
    public GameObject objNum;
    public GameObject parentTvCoin;
    public GameObject lockIcon;
    public GameObject unLockIcon;
 
    public bool wasUseMoveSightingPointBooster;

    public Transform post;
    GameObject selectedObject;
 
    public GameObject panelTut;
    public CanvasGroup canvasGroup;
    PlayerContain playerContain;
    public void Init(PlayerContain param)
    {
        playerContain = param;
        
        selectedObject = null;
        wasUseMoveSightingPointBooster = false;
 
        if (UseProfile.CurrentLevel >= 3)//3
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

        if (UseProfile.UnlimitScope)
        {
            objNum.SetActive(false);
            tvNum.gameObject.SetActive(false);
            parentTvCoin.SetActive(false);
        }

        void HandleUnlock()
        {
            moveSightingPoint_Booster.onClick.AddListener(Handle_Move_Sighting_Point);
            if (UseProfile.MoveSightingPointBooster > 0)
            {
                objNum.SetActive(true);
                tvNum.text = UseProfile.MoveSightingPointBooster.ToString();
                parentTvCoin.SetActive(false);
            }
            else
            {
                objNum.SetActive(false);
                tvNum.gameObject.SetActive(false);
                parentTvCoin.SetActive(true);
            }
            EventDispatcher.EventDispatcher.Instance.RegisterListener(EventID.MOVE_SIGHTING_POINT_BOOSTER, ChangeText);
        }
        void HandleLock()
        {


            moveSightingPoint_Booster.onClick.AddListener(HandleLockBtn);
        }
    }

    public void HandleLockBtn()
    {
        GameController.Instance.musicManager.PlayClickSound();
        GameController.Instance.moneyEffectController.SpawnEffectText_FlyUp
                              (
                              moveSightingPoint_Booster.transform.position,
                              "Unlock at level 5",
                              Color.white,
                              isSpawnItemPlayer: true
                              );
    }





    public void Handle_Move_Sighting_Point()
    {
        GameController.Instance.musicManager.PlayClickSound();
        if (UseProfile.UnlimitScope)
        {
 
            playerContain.HandleMoveSightingPointBooster();
            wasUseMoveSightingPointBooster = true;
            moveSightingPoint_Booster.interactable = false;
            playerContain.levelData.inputThone.postFireSpike.HandleScale();
            GamePlayController.Instance.TutMoveSightingPointBooster.NextTut();
            return;
        }
        if (UseProfile.MoveSightingPointBooster >= 1)
        {


            UseProfile.MoveSightingPointBooster -= 1;
            playerContain.HandleMoveSightingPointBooster();
            wasUseMoveSightingPointBooster = true;
            moveSightingPoint_Booster.interactable = false;
            playerContain.levelData.inputThone.postFireSpike.HandleScale();
            GamePlayController.Instance.TutMoveSightingPointBooster.NextTut();
        }
        else
        {
            SuggetBox.Setup(GiftType.MOVE_SIGHTING_POINT_BOOSTER).Show();
        }


    }

    public void HandleOffBooster()
    {
        wasUseMoveSightingPointBooster = false;
        moveSightingPoint_Booster.interactable = true;
        playerContain.levelData.inputThone.postFireSpike.OffScale();
        GamePlayController.Instance.playerContain.levelData.inputThone.SetupFirstPost();
    }    
   

    public void ChangeText(object param)
    {
        tvNum.text = UseProfile.MoveSightingPointBooster.ToString();
        if (UseProfile.MoveSightingPointBooster > 0)
        {
            objNum.SetActive(true);
            tvNum.gameObject.SetActive(true);
            tvNum.text = UseProfile.MoveSightingPointBooster.ToString();
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
        EventDispatcher.EventDispatcher.Instance.RemoveListener(EventID.MOVE_SIGHTING_POINT_BOOSTER, ChangeText);
    }
}
