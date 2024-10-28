using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseLightlingCard : CardBase
{
    public static int dame = 1;
    public override bool CanShow()
    {
        return true;
    }
    public override void Init()
    {

    }
    public override void HandleAction()
    {
        dame += 1;
    }
}

