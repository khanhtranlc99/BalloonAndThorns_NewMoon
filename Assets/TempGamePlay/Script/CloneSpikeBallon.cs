using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneSpikeBallon : Ballon
{
    public Sprite sprite;
    public GameObject demoSpike;
 
    public override void TakeDameSpike()
    {

       

    }
    public override void TakeDameSpikeEffect(BallMovement paramBall)
    {
        spriteRenderer.sprite = eplosionBallon;

        if (!isOff)
        {
            isOff = true;
            GameController.Instance.musicManager.PlayRandomBallon();
            GamePlayController.Instance.gameScene.HandleSubtrackBallon();
            GamePlayController.Instance.playerContain.levelData.CountWin(this.transform);
            demoSpike.gameObject.SetActive(false);

            paramBall.HandleBoosterX2();

            //GamePlayController.Instance.HandlSpawnItemInGameBallon(sprite, this.transform.position);

            //GamePlayController.Instance.playerContain.levelData.PlusBallBallon();
        }

     
    }
}