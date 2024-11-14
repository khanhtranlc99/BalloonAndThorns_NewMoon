using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using DG.Tweening;
using System;
public class PlayerContain : MonoBehaviour
{
    public LevelData levelData;
    public InputThone inputThone;
    public GameObject vfxRocket;
    public Transform canvas;
    public CameraFollow cameraFollow;
    public static bool testc1 = false;
    public Transform postBot;
    public SpinerBooster spinerBooster;
    public MoveSightingPointBooster moveSightingPointBooster;
    public CloneBallsBooster cloneBallsBooster;
    public RoketBooster roketBooster;
    public CardController cardController;
    public EffectExplosion effectExplosion;
    public GameObject bg_Chapper_I;
    public GameObject bg_Chapper_II;
    public CannonSkinController cannonSkinController;
    public EffectTouch effectTouch;
    public void Init()
    {
      
            string pathLevel = StringHelper.PATH_CONFIG_LEVEL;
            levelData = Instantiate(Resources.Load<LevelData>(string.Format(pathLevel, UseProfile.CurrentLevel)));
          
            GamePlayController.Instance.gameScene.ShowText(levelData.levelDetail);
         if(levelData.chapperType == ChapperType.Chapper_I || levelData.chapperType == ChapperType.Chapper_III)
        {
            bg_Chapper_I.SetActive(true);
            bg_Chapper_II.SetActive(false);
        }
         else
        {
            bg_Chapper_I.SetActive(false);
            bg_Chapper_II.SetActive(true);
        }


        levelData.Init();
      
        inputThone.Init(this);
        cannonSkinController.Init();



        GameController.Instance.AnalyticsController.LoadingComplete();
        GameController.Instance.AnalyticsController.StartLevel(UseProfile.CurrentLevel);
        Debug.LogError("CurrentLevel_" + UseProfile.CurrentLevel);
    }
  

    public void HandleScaleInput(Action callBack)
    {
        //  inputThone.postFireSpike.transform.position = new Vector3(0,-8.28f,0);
        //Debug.LogError(inputThone.postFireSpike.transform.position);
        inputThone.iconCanon.transform.localEulerAngles = Vector3.zero;
        inputThone.transform.DOScale(new Vector3(1,1,1), 0.3f).OnComplete(delegate {

            GamePlayController.Instance.TutGameplay.StartTut();
            callBack?.Invoke();

        }) ;
     
    }

    public void HandleMoveSightingPointBooster()
    {
  
        //levelData.PlusBall();
      inputThone.moveCollider.HandleBooster();
    }

    public void HandleCloneBallsBooster()
    {
        var temp = new List<BallController>();
        foreach (var item in inputThone.lsBallMovement)
        {
            if (item.gameObject.activeSelf)
            {

               // temp.Add(item);

            }
        }
        foreach (var item in temp)
        {
            item.HandleBoosterX2();
        }
    }

    public void HandleRoketBooster(Transform paramPost, Action param)
    {
        var temp = SimplePool2.Spawn(vfxRocket);
        temp.transform.parent = canvas;
        temp.transform.position = paramPost.position;
        temp.transform.localScale = new Vector3(-1,1,1);
        temp.transform.DOMove(Vector3.zero, 1).OnComplete(delegate {

            try
            {
                levelData.AllBallHit();
            }
          catch
            {

            }
            //GamePlayController.Instance.playerContain.levelData.CountWin(null);
            testc1 = true;
            if(param!=null)
            {
                param?.Invoke();
            }    
            SimplePool2.Despawn(temp);
        });
        
    }

    public void NextLevel()
    {
        UseProfile.CurrentLevel += 1;
        if (UseProfile.CurrentLevel > 80)
        {
            UseProfile.CurrentLevel = 80;
        }

        Initiate.Fade("GamePlay", Color.black, 2f);
    }    
    public void HandleNextLevel()
    {
        Destroy(levelData.gameObject);
        Init();
   
    }    
}
