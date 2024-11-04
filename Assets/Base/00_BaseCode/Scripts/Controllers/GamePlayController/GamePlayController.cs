using Crystal;
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
 

    public Transform limitLeft;
    public Transform limitRight;
  

    public List<RockWall> lsIdWallOffInGame;

   
    public CameraScale cameraScale;
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
    

        Init();



    }

    public void Init()
    {
        playerContain.cardController.Init();
        TutGameplay.Init();
       

        playerContain.Init();
      //
        gameScene.Init(playerContain.levelData);
        SimplePool2.ClearPool();
        SimplePool2.Preload(hitVfx.gameObject, 250, null);
        SimplePool2.Preload(itemInGameBallon.gameObject, 100, null);
       
        StartCoroutine(HandleSetPostWall());
        playerContain.inputThone.listBallController.Init();

    }
    public void SetPlayingState()
    {
        if(playerContain.levelData.AllBallonInit)
        {
          
            playerContain.levelData.HandleShowWall(delegate {
                
                playerContain.HandleScaleInput(delegate {

                    stateGame = StateGame.Playing;
                }); 
            });
        
        }
    }    
    public void HandleWin()
    {
        if ( stateGame == StateGame.Playing)
        {
            stateGame = StateGame.Win;
            //confeti_1.Play();
            //confeti_2.Play();

            if (playerContain.inputThone.lsBallMovement.Count > 0)
            {
                StartCoroutine(playerContain.inputThone.StopActiveMove(delegate { HandleShowWin();/* playerContain.inputThone.HandleSetUp();*/ }));
            }
            else
            {
                HandleShowWin();
                playerContain.inputThone.HandleSetUp();
            }

        }
    }    

    private void HandleShowWin()
    {
        if (Time.timeScale == 2)
        {
            Time.timeScale = 1;
           
        }
        playerContain.inputThone.gameObject.transform.localScale = Vector3.zero;
        Winbox.Setup().Show();
        //if (UseProfile.CurrentLevel == 4)
        //{
        //    DialogueRate.Setup().Show();
        //}
        //else
        //{
        //    Winbox.Setup().Show();
        //}
    }    

  
    private IEnumerator HandleSetPostWall()
    {
        yield return new WaitForEndOfFrame();
        foreach(var item in testPosts)
        {
            item.workPost.position = item.uiPost.position;
        }
     //   cameraScale.Init();
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
            playerContain.levelData.AllBallHit();
        }
      
    }
}
[System.Serializable]
public class TestPost
{
    public Transform uiPost;
    public Transform workPost;
}    