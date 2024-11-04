using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPlusCard : CardBase
{
    public InputThone inputThone;
    public override bool CanShow()
    {
        if (inputThone.numbBall >= 40)
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
        inputThone.numbBall += 1;
        GamePlayController.Instance.playerContain.cardController.CheckCard(id);
    }
}
