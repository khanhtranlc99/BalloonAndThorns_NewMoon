using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseElectricCard : CardBase
{
    public EndOfElectricCard endOfElectricCard;
    public ElectricBallCard electricBallCard;
    public static int dame = 1;
    public override bool CanShow()
    {
        if(!endOfElectricCard.isActive)
        {
            return false;
        }
        if (electricBallCard.countNumb <= 0)
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

