using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCrackerCard : CardBase
{
    public SpinerBooster spinerBooster;
    public override bool CanShow()
    {
        return true;
    }
    public override void Init()
    {

    }
    public override void HandleAction()
    {
        spinerBooster.HandleSniper_Booster();
        Debug.LogError("FireCrackerCard");
    }
}

