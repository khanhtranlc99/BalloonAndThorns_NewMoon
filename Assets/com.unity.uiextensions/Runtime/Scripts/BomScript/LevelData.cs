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
    public List<int> lsIdWallOff;
 

    public void Init()
    {

        countBallon = 0;
        if (UseProfile.UnlimitSpike)
        {
            tvShowLimit.text = "";
        }
        else
        {
            tvShowLimit.text = "x" + limitTouch;
        }    

        foreach (var item in lsBallons)
        {
            item.gameObject.transform.position = new Vector3(item.gameObject.transform.position.x, item.gameObject.transform.position.y,1);
            item.Init();

        }

        foreach (var item in lsIdWallOff)
        {

            GamePlayController.Instance.GetRockWall(item).HandleDieWall();
             

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
        if (UseProfile.UnlimitSpike)
        {
            return;
        }    
        limitTouch -= 1;
        tvShowLimit.text = "x" + limitTouch;
    }    

    public void PlusBall()
    {
        limitTouch += 5;
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

    public void HandleUnlimitSpike()
    {

        tvShowLimit.text = "";
    }
    [Button]
    private void HandleColor()
    {
        foreach (var item in lsBallons)
        {

            item.HandleColorBallon();
        }
    }    
}