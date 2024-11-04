using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseSoundWaveCard : CardBase
{
    public static int dame = 1;
    public SoundWaveCard soundWaveCard;
    public override bool CanShow()
    {
        if(!soundWaveCard.isActive)
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
