using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class EffectExplosion : MonoBehaviour
{
    public Action<BarrialAir> effectExplosion;

    public void HandleEffectExplosion(BarrialAir param)
    {
        if(effectExplosion != null)
        {
            effectExplosion?.Invoke(param);
        }
    }    
}
