using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfExplosionCard : CardBase
{
    public bool isActive = false;
    public HitVfx vfxExplosion;
    public override void Init()
    {

    }
    public override void HandleAction()
    {
        isActive = true;
        SimplePool2.Preload(vfxExplosion.gameObject, 50);
        GamePlayController.Instance.playerContain.effectExplosion.effectExplosion += HandleEndOfElectric;
        GamePlayController.Instance.playerContain.cardController.CheckCard(id);
    }

    private void HandleEndOfElectric(BarrialAir param)
    {
        SimplePool2.Spawn(vfxExplosion, param.gameObject.transform.position, Quaternion.identity);
        foreach (var item in param.GetBallondsAround())
        {

         
            item.TakeDameSpike(1);
        }
    }

    public override bool CanShow()
    {
        if (isActive)
        {
            return false;
        }
        return true;
    }
}
