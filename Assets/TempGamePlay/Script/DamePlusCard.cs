using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamePlusCard : CardBase
{
    public static int dame = 1;
    public override bool CanShow()
    {
        if (dame >= 10)
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
