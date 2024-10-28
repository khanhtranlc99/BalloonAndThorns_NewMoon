using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomBallCard : CardBase
{
    public BallBase ballBase;
    int countNumb;
    public override void Init()
    {
    
    }
    public override void HandleAction()
    {
        countNumb += 1;
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

