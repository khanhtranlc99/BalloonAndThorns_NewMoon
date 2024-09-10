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
    public void Init()
    {
        string pathLevel = StringHelper.PATH_CONFIG_LEVEL_TEST;
        levelData = Instantiate(Resources.Load<LevelData>(string.Format(pathLevel, UseProfile.CurrentLevel)));
        levelData.Init();
        inputThone = levelData.inputThone;
    

        spinerBooster.Init(this);
        moveSightingPointBooster.Init(this);
        cloneBallsBooster.Init(this);
        roketBooster.Init(this);
        GamePlayController.Instance.TutGameplay.StartTut();
        GamePlayController.Instance.TutSpinerBooster.StartTut();
        GamePlayController.Instance.TutMoveSightingPointBooster.StartTut();
 
        GamePlayController.Instance.TutRocketBooster.StartTut();
        StartCoroutine(SetPost());

        GameController.Instance.AnalyticsController.LoadingComplete();
        GameController.Instance.AnalyticsController.StartLevel(UseProfile.CurrentLevel);
    }
    private IEnumerator SetPost()
    {
        yield return new WaitForSeconds(0.1f);
        levelData.inputThone.postFireSpike.transform.position = new Vector3(levelData.inputThone.postFireSpike.transform.position.x, postBot.position.y, 0);
    }

    public void HandleSpinerBooster()
    {
        inputThone.numOfReflect = 4;
      
    }

    public void HandleMoveSightingPointBooster()
    {
  
        //levelData.PlusBall();
        levelData.inputThone.postFireSpike.HandleBooster();
    }

    public void HandleCloneBallsBooster()
    {
        var temp = new List<BallMovement>();
        foreach (var item in levelData.inputThone.lsBallMovement)
        {
            if (item.gameObject.activeSelf)
            {

                temp.Add(item);

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
        if (UseProfile.CurrentLevel > 70)
        {
            UseProfile.CurrentLevel = 70;
        }

        Initiate.Fade("GamePlay", Color.black, 2f);
    }    

}
