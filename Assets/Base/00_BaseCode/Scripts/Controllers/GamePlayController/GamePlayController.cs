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

    protected override void OnAwake()
    {
        stateGame = StateGame.Playing;

        Init();



    }

    public void Init()
    {

        HandleSetPostWall();
        playerContain.Init();
        gameScene.Init(playerContain.levelData);
        SimplePool2.ClearPool();
        SimplePool2.Preload(hitVfx.gameObject, 40, null);
        SimplePool2.Preload(itemInGameBallon.gameObject, 40, null);
        stateGame = StateGame.Playing;

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
        Winbox.Setup().Show();
    }    

    [Button]
    private void HandleSetPostWall()
    {
        foreach(var item in testPosts)
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


}
[System.Serializable]
public class TestPost
{
    public Transform uiPost;
    public Transform workPost;
}    