using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperCard : CardBase
{
    public InputThone inputThone;
    public override bool CanShow()
    {
        if(inputThone.numOfReflect >= 4)
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
        inputThone.numOfReflect += 1;
        GamePlayController.Instance.playerContain.cardController.CheckCard(id);
    }
}
