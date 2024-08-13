using Crystal;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
    public ParticleSystem confeti_1;
    public ParticleSystem confeti_2;

    protected override void OnAwake()
    {
        //  GameController.Instance.currentScene = SceneType.GamePlay;

     
        Init();

    }

    public void Init()
    {

    
        playerContain.Init();
        gameScene.Init(playerContain.levelData);
        SimplePool2.ClearPool();
        SimplePool2.Preload(hitVfx.gameObject, 40, null);
        stateGame = StateGame.Playing;
    }
   
    public void HandleWin()
    {
        GamePlayController.Instance.stateGame = StateGame.Win;
        confeti_1.Play();
        confeti_2.Play();
        playerContain.inputThone.currentBallController.rigidbody2D.velocity = Vector2.zero;
        Invoke(nameof(HandleShowWin), 2);
    }    

    private void HandleShowWin()
    {
        Winbox.Setup().Show();
    }    



}
