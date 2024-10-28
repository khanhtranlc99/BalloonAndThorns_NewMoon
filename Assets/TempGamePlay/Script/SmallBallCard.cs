using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallBallCard : CardBase
{
    public BallBase ballBase;
    public override void Init()
    {

    }
    public override void HandleAction()
    {
        GamePlayController.Instance.playerContain.inputThone.listBallController.currentBallBases.Add(ballBase);
    }

    public override bool CanShow()
    {
        return true;
    }
}

