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

    public List<Ballon> lsBallons;
    int countBallon = 0;
    public int limitTouch;
    public TMP_Text tvShowLimit;
    public ThoneDemo thoneDemo;


    public void Init()
    {

        countBallon = 0;
        tvShowLimit.text = "" + limitTouch;

    }


    public void CountWin()
    {
        countBallon += 1;
        if(countBallon >= lsBallons.Count )
        {
            if(GamePlayController.Instance.stateGame == StateGame.Playing)
            {
                GamePlayController.Instance.HandleWin();
            }    

        }    
    }



    public void HandleCheckLose()
    {
    
        if(limitTouch < 1)
        {
            if (GamePlayController.Instance.stateGame == StateGame.Playing)
            {
                GamePlayController.Instance.playerContain.inputThone.currentBallController.rigidbody2D.velocity = Vector2.zero;
                GamePlayController.Instance.stateGame = StateGame.Lose;
                LoseBox.Setup().Show();
            }
        }    
        else
        {
            limitTouch -= 1;
            tvShowLimit.text = "" + limitTouch;
        }    
   
    }

}