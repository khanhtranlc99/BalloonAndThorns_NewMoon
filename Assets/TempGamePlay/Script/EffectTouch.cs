using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class EffectTouch : MonoBehaviour
{
    public Action<BarrialAir> effectTouch;

    public void HandleEffectExplosion(BarrialAir param)
    {
        if (effectTouch != null)
        {
            effectTouch?.Invoke(param);
        }
    }
}
