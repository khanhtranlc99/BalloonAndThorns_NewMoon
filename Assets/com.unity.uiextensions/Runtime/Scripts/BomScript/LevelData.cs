using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEditor;
using System;
using DG.Tweening;
using TMPro;

public enum Difficult
{
    Normal,
    Hard,
    VeryHard,
  

}

public class LevelData : SerializedMonoBehaviour
{
    public InputThone inputThone;
    public List<BarrialAir> lsBallons;
    int countBallon = 0;
    public int limitTouch;
    public TMP_Text tvShowLimit;
    public int numbTarget;
    public List<DataBallon> lsDataBallon;

    public BarrialAir GetBallon(int id)
    {
        foreach(var item in lsBallons)
        {
            if(item.idInLevelT == id)
            {
                return item;
            }    
        }
        return null;
    }
    public void Init()
    {

        countBallon = 0;
        //tvShowLimit.text = "x" + limitTouch;
        foreach (var item in lsBallons)
        {

            item.Init();

        }

    }


    public void CountWin(Transform param)
    {
    
        countBallon += 1;
        if(countBallon >= numbTarget)
        {
            if(GamePlayController.Instance.stateGame == StateGame.Playing)
            {
                GamePlayController.Instance.playerContain.cameraFollow.HandleZoom(param);
                GamePlayController.Instance.HandleWin();
            }    

        }    
    }



    public void HandleCheckLose()
    {
    
        if(limitTouch < 1 && countBallon < numbTarget && GamePlayController.Instance.playerContain.inputThone.AllDestoy)
        {
            if (GamePlayController.Instance.stateGame == StateGame.Playing)
            {
    
                GamePlayController.Instance.stateGame = StateGame.Lose;
                LoseBox.Setup().Show();
            }
        }    
     
   
    }

    public void HandleSubtrack()
    {
        limitTouch -= 1;
        tvShowLimit.text = "x" + limitTouch;
    }    

    public void PlusBall()
    {
        limitTouch *= 2;
        tvShowLimit.text = "x" + limitTouch;
    }
    public void AllBallHit()
    {
        foreach(var item in lsBallons)
        {
            item.TakeDameSpike();
        }    
    }
    public void PlusBallBallon()
    {
        limitTouch += 1;
        tvShowLimit.text = "x" + limitTouch;
    }

    [Button]
    private void HandleFillId()
    {
         for(int i = 0; i < lsBallons.Count; i ++)
        {
            lsBallons[i].idInLevelT = i;
        }    
    }

    [Button]
    private void HandleSave()
    {
        for (int i = 0; i < lsBallons.Count; i++)
        {
            lsBallons[i].GetComponent<Ballon>().HandleSave();
        }
    }
    [Button]
    private void HandleLoad()
    {
        for (int i = 0; i < lsBallons.Count; i++)
        {
            lsBallons[i].GetComponent<Ballon>().HandleLoad();
        }
    }

    public void HandleExplosion()
    {
        
    }
}