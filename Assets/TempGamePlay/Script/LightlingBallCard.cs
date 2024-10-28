using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightlingBallCard : CardBase
{
    int countNumb;
    public BallBase ballBase;
    public HitVfx vfxLightling;
    public override void Init()
    {

    }
    public override void HandleAction()
    {
        countNumb += 1;
        SimplePool2.Preload(vfxLightling.gameObject, 20);
        GamePlayController.Instance.playerContain.inputThone.listBallController.currentBallBases.Add(ballBase);
    }

    public override bool CanShow()
    {
        if (countNumb >= 3)
        {
            return false;
        }
        return true;
    }
}