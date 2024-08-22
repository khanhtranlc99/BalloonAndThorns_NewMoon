using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallonPlusSpike : Ballon
{
    public Sprite sprite;
    public GameObject demoSpike;
     public override void TakeDameSpike()
    {

        spriteRenderer.sprite = eplosionBallon;

        if (!isOff)
        {
            isOff = true;
            GameController.Instance.musicManager.PlayRandomBallon();
            GamePlayController.Instance.gameScene.HandleSubtrackBallon();
            GamePlayController.Instance.playerContain.levelData.CountWin(this.transform);
            demoSpike.gameObject.SetActive(false);
            GamePlayController.Instance.HandlSpawnItemInGameBallon(sprite, this.transform.position);
            GamePlayController.Instance.playerContain.levelData.PlusBallBallon();
        }
     

    }
}
