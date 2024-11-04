using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class EndOfLightlingCard : CardBase
{
    public bool isActive = false;
    public HitVfx vfxLightling;
    public override void Init()
    {

    }
    [Button]
    public override void HandleAction()
    {
        isActive = true;
        SimplePool2.Preload(vfxLightling.gameObject, 50);
        GamePlayController.Instance.playerContain.effectExplosion.effectExplosion += HandleEndOfElectric;
        GamePlayController.Instance.playerContain.cardController.CheckCard(id);
    }

    private void HandleEndOfElectric(BarrialAir param)
    {
        SimplePool2.Spawn(vfxLightling, param.gameObject.transform.position, Quaternion.identity);
        foreach (var item in param.GetBallondsLeftRight())
        {
            item.TakeDameSpike(IncreaseLightlingCard.dame);
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