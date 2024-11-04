using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseRocketCard : CardBase
{
    public EndOfRocketCard endOfRocketCard;
  
    public static int dame = 1;
    public override bool CanShow()
    {    
        if (dame > 3)
        {
            return false;
        }
        if (!endOfRocketCard.isActive)
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
