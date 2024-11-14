using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootPlusCard : CardBase
{
 
    public static int numbShootPlus = 0;
    public override bool CanShow()
    {
        if(numbShootPlus >= 10)
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
        numbShootPlus  += 1;
        GamePlayController.Instance.playerContain.cardController.CheckCard(id);
    }
    private void OnDestroy()
    {
        numbShootPlus = 0;
    }
}

