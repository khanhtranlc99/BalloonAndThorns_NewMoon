using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomBallon : Ballon
{
    public Sprite sprite;
    public GameObject demoSpike;
    public ParticleSystem vfxBoom;
    public List<BarrialAir> lsBarrialAir;
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
            vfxBoom.Play();
            foreach(var item in lsBarrialAir)
            {
                item.TakeDameSpike();
            }

        }


    }
}