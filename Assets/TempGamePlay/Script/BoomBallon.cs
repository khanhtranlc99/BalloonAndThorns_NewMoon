using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomBallon : Ballon
{
    public Sprite sprite;
    public GameObject demoSpike;
    public ParticleSystem vfxBoom;
    public List<BarrialAir> lsBarrialAir;
    public override void TakeDameSpike(int paramDame)
    {
        if (isInit)
        {


            countExplosion -= paramDame;


            if (countExplosion < 1)
            {
                spriteRenderer.sprite = eplosionBallon;
                HandleExplosion();
            }
            tvExplosion.text = "" + countExplosion;

        }
 

      


    }


    private void HandleExplosion()
    {
        if (!isOff)
        {
            isOff = true;
            GameController.Instance.musicManager.PlayRandomBallon();
            circleCollider.enabled = false;
            tvExplosion.gameObject.SetActive(false);
            GamePlayController.Instance.playerContain.levelData.CountWin(this.transform);
            demoSpike.gameObject.SetActive(false);
            vfxBoom.Play();
            foreach (var item in lsBarrialAir)
            {
                item.Destroy();
            }
            GamePlayController.Instance.playerContain.effectExplosion.HandleEffectExplosion(this);
        }
    }
    public override void Destroy()
    {
        base.Destroy();
        demoSpike.SetActive(false);
    }
}