using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseLightlingCard : CardBase
{
    public EndOfLightlingCard endOfLightlingCard;
    public LightlingBallCard lightlingBallCard;
    public static int dame = 1;
    public override bool CanShow()
    {
        if (!endOfLightlingCard.isActive)
        {
            return false;
        }
        if (lightlingBallCard.countNumb <= 0)
        {
            return false;
        }
        return true;
    }
    public override void Init()
    {

    }
    public override void HandleAction()
    {
        dame += 1;
        GamePlayController.Instance.playerContain.cardController.CheckCard(id);
    }
}

