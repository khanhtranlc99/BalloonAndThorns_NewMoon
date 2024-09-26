﻿using Crystal;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;


public enum StateGame
{
    Loading = 0,
    Playing = 1,
    Win = 2,
    Lose = 3,
    Pause = 4
}

public class GamePlayController : Singleton<GamePlayController>
{
    public StateGame stateGame;
    public PlayerContain playerContain;
    public GameScene gameScene;
    public HitVfx hitVfx;
    public List<TestPost> testPosts;
    public ItemInGameBallon itemInGameBallon;

    public TutorialFunController TutGameplay;
    public TutorialFunController TutSpinerBooster;
    public TutorialFunController TutMoveSightingPointBooster;
    public TutorialFunController TutCloneBallsBooster;
    public TutorialFunController TutRocketBooster;

    public Transform limitLeft;
    public Transform limitRight;
    public GameObject backGround_1;
    public GameObject backGround_2;

    public List<RockWall> lsIdWallOffInGame;

    public BallController ballController;
    public RockWall GetRockWall(int id)
    {
        foreach(var item in lsIdWallOffInGame)
        {
            if(item.id == id)
            {
                return item;
            }    
        }
        return null;
    }    

    protected override void OnAwake()
    {
        stateGame = StateGame.Playing;

        Init();



    }

    public void Init()
    {
        TutGameplay.Init();
        TutSpinerBooster.Init();
        TutMoveSightingPointBooster.Init();
        TutCloneBallsBooster.Init();
        TutRocketBooster.Init();

        playerContain.Init();
        gameScene.Init(playerContain.levelData);
        SimplePool2.ClearPool();
        SimplePool2.Preload(hitVfx.gameObject, 40, null);
        SimplePool2.Preload(itemInGameBallon.gameObject, 40, null);
        stateGame = StateGame.Playing;
        StartCoroutine(HandleSetPostWall());
        if(RemoteConfigController.GetFloatConfig(FirebaseConfig.ID_BACK_GROUND, 1) == 1)
        {
            backGround_1.SetActive(true);
            backGround_2.SetActive(false);
        }
        if (RemoteConfigController.GetFloatConfig(FirebaseConfig.ID_BACK_GROUND, 1) == 2)
        {
            backGround_1.SetActive(false);
            backGround_2.SetActive(true);
        }

    }
   
    public void HandleWin()
    {
        GamePlayController.Instance.stateGame = StateGame.Win;
        //confeti_1.Play();
        //confeti_2.Play();
        GamePlayController.Instance.playerContain.inputThone.StopActiveMove();
       Invoke(nameof(HandleShowWin), 2);
    }    

    private void HandleShowWin()
    {
        if (Time.timeScale == 2)
        {
            Time.timeScale = 1;
            gameScene.HandleChangeNormal();
        }
     
        if (UseProfile.CurrentLevel == 4)
        {
            DialogueRate.Setup().Show();
        }
        else
        {
            Winbox.Setup().Show();
        }
    }    

  
    private IEnumerator HandleSetPostWall()
    {
        yield return new WaitForEndOfFrame();
        foreach(var item in testPosts)
        {
            item.workPost.position = item.uiPost.position;
        }    
    }
    [Button]
    private void Test()
    {
        foreach (var item in testPosts)
        {
            item.workPost.position = item.uiPost.position;
        }
    }    

    public void HandlSpawnItemInGameBallon(int param, Vector3 post)
    {
        var temp = SimplePool2.Spawn(itemInGameBallon);
        temp.transform.position = post;
        temp.Init(param);
    }

    public void HandlSpawnItemInGameBallon(Sprite param, Vector3 post)
    {
        var temp = SimplePool2.Spawn(itemInGameBallon);
        temp.transform.position = post;
        temp.Init(param);
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.K))
        {
            Winbox.Setup().Show();
            Debug.LogError("Winbox");
        }
      
    }
}
[System.Serializable]
public class TestPost
{
    public Transform uiPost;
    public Transform workPost;
}    