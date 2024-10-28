using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBallPlusCard : CardBase
{
    public static int speedBall = 20;
    public override bool CanShow()
    {
        return true;
    }
    public override void Init()
    {

    }
    public override void HandleAction()
    {
        speedBall += 1;
    }
}

