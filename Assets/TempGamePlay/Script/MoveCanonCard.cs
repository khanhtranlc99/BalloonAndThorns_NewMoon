using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCanonCard : CardBase
{
    public MoveSightingPointBooster moveSightingPointBooster;
    public override bool CanShow()
    {
        return true;
    }
    public override void Init()
    {

    }
    public override void HandleAction()
    {
        moveSightingPointBooster.Handle_Move_Sighting_Point();
    }
}

