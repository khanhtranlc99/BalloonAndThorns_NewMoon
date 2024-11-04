using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
public class EndOfElectricCard : CardBase
{
    public bool isActive = false;
    public HitVfx vfxElectric;
    public override void Init()
    {

    }
    [Button]
    public override void HandleAction()
    {
        isActive = true;
        SimplePool2.Preload(vfxElectric.gameObject,50);
        GamePlayController.Instance.playerContain.effectExplosion.effectExplosion += HandleEndOfElectric;
        GamePlayController.Instance.playerContain.cardController.CheckCard(id);
    }

    private void HandleEndOfElectric(BarrialAir param)
    {
        SimplePool2.Spawn(vfxElectric, param.gameObject.transform.position, Quaternion.identity);
        foreach (var item in param.GetBallondsAround())
        {

            SimplePool2.Spawn(vfxElectric, item.gameObject.transform.position, Quaternion.identity);
            item.TakeDameSpike(IncreaseElectricCard.dame);
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
