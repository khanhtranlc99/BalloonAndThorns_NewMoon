using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricBallCard : CardBase
{
    public int countNumb;
    public BallBase ballBase;
    public HitVfx vfxElectric;
    public override void Init()
    {

    }
    public override void HandleAction()
    {
        countNumb += 1;
        SimplePool2.Preload(vfxElectric.gameObject, 20);
        GamePlayController.Instance.playerContain.inputThone.listBallController.currentBallBases.Add(ballBase);
        GamePlayController.Instance.playerContain.cardController.CheckCard(id);
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