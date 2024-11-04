using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEditor;
using System;
using DG.Tweening;
using TMPro;
using System;
public enum Difficult
{
    Normal,
    Hard,
    VeryHard,
  

}

public class LevelData : SerializedMonoBehaviour
{
    public int Max;
    public int Min;
    public List<BarrialAir> lsBallons;
    int countBallon = 0;
    public int limitTouch;
    public int numbTarget;
    public List<int> lsIdWallOff;
  
    public bool isMove = false;
    public bool AllExplosion
    {
        get
        {
            foreach(var item in lsBallons)
            {
                if(item.countExplosion > 0)
                {
                    return false;
                }
            }
            return true;
        }
    }
    public bool AllBallonInit
    {
        get
        {
            if (lsBallons.Count <= 0)
            {
                return true;
            }
            foreach (var item in lsBallons)
            {
                if (!item.isInit)
                {
                    return false;
                }
            }
            return true;
        }
    }
    public List<SpriteRenderer> lsBrickBreak;
    public void Init()
    {
        if (lsBrickBreak.Count > 0)
        {
            for (int i = 0; i < lsBrickBreak.Count; i++)
            {
                lsBrickBreak[i].color = new Color32(255, 255, 255, 0);
            }
        }
            countBallon = 0;
      

        foreach (var item in lsBallons)
        {
            item.gameObject.transform.position = new Vector3(item.gameObject.transform.position.x, item.gameObject.transform.position.y,1);
            item.Init();

        }

        foreach (var item in lsIdWallOff)
        {

          //  GamePlayController.Instance.GetRockWall(item).HandleDieWall();
             

        }

    }

    public void  HandleShowWall( Action callBack)
    {
       if (lsBrickBreak.Count > 0)
        {
            for (int i = 0; i < lsBrickBreak.Count; i++)
            {
                int index = i;
                lsBrickBreak[index].DOFade(1, 0.35f).OnComplete(delegate {

                    if (index == lsBrickBreak.Count - 1)
                    {
                        callBack?.Invoke();
                    }

                });
            }
        }
       else
        {
            callBack?.Invoke();
        }
    }    

    public void CountWin(Transform param)
    {
    
        countBallon += 1;
        if(countBallon >= numbTarget)
        {
            if(GamePlayController.Instance.stateGame == StateGame.Playing)
            {
             //   GamePlayController.Instance.playerContain.cameraFollow.HandleZoom(param);
                GamePlayController.Instance.HandleWin();
            }    

        }    
    }



    public void HandleCheckLose()
    {
    
        if(limitTouch < 1 && countBallon < numbTarget  )
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
     
    }    

    public void PlusBall()
    {
        limitTouch += 5;
       
    }
    public void AllBallHit()
    {
        foreach(var item in lsBallons)
        {
            
            item.Destroy();
        }    
    }
    public void PlusBallBallon()
    {
        limitTouch += 1;
     
    }

    public void HandleUnlimitSpike()
    {

       
    }
    public IEnumerator HandleActionMove(Action callBack)
    {
        List<Coroutine> runningCoroutines = new List<Coroutine>();
        foreach (var item in lsBallons)
        {
            runningCoroutines.Add(StartCoroutine(item.Move()));
        }
        foreach (var coroutine in runningCoroutines)
        {
            yield return coroutine;
        }
        callBack?.Invoke();
    }
 
     
    [Button]
    private void HandleColor()
    {
        foreach (var item in lsBallons)
        {

            item.HandleColorBallon();
        }
    }
    [Button]
    private void HandlePostFly()
    {
        foreach (var item in lsBallons)
        {
            item.countExplosion = UnityEngine.Random.Range(Min, Max);
            item.tvExplosion.text = "" + item.countExplosion;
            item.postFly = item.transform.position;
  
        }
        Debug.LogError("HandlePostFly");
    }
    [Button]
    private void HandleSetPostFly()
    {
        foreach (var item in lsBallons)
        {
        
            item.transform.position = item.postFly;
           
        }
        Debug.LogError("HandlePostFly");
    }
    [Button]
    private void HandleSetZeroPost()
    {
        foreach (var item in lsBallons)
        {

            item.transform.position = new Vector3(0,-12,0);
        }
        Debug.LogError("HandlePostZero");
    }


 

}