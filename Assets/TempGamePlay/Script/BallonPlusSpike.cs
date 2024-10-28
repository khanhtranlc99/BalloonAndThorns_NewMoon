using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallonPlusSpike : Ballon
{
    public Sprite sprite;
    public GameObject demoSpike;
     public override void TakeDameSpike(int paramDame)
    {

        if (isInit)
        {


            countExplosion -= paramDame;


            if (countExplosion < 1)
            {
                spriteRenderer.sprite = eplosionBallon;
                HandleEplosion();
            }
            tvExplosion.text = "" + countExplosion;

        }


    }

    private void HandleEplosion()
    {
      

        if (!isOff)
        {
            isOff = true;
            GameController.Instance.musicManager.PlayRandomBallon();
            circleCollider.enabled = false;
            tvExplosion.gameObject.SetActive(false);
            GamePlayController.Instance.playerContain.levelData.CountWin(this.transform);
            demoSpike.gameObject.SetActive(false);
            GamePlayController.Instance.HandlSpawnItemInGameBallon(sprite, this.transform.position);
            GamePlayController.Instance.playerContain.levelData.PlusBallBallon();
            GamePlayController.Instance.playerContain.inputThone.numbBall += 1;
            GamePlayController.Instance.playerContain.effectExplosion.HandleEffectExplosion(this);
        }
        
    }

    public override void Destroy()
    {
        base.Destroy();
        demoSpike.SetActive(false);
    }
}
