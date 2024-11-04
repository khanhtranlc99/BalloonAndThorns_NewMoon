using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
public class SoundWaveCard : CardBase
{
    public bool isActive = false;
    public HitVfx vfxSoundWave;
    public override void Init()
    {

    }
    [Button]
    public override void HandleAction()
    {
        isActive = true;
        SimplePool2.Preload(vfxSoundWave.gameObject, 50);
        GamePlayController.Instance.playerContain.effectTouch.effectTouch += HandleEndOfElectric;
        GamePlayController.Instance.playerContain.cardController.CheckCard(id);
    }

    private void HandleEndOfElectric(BarrialAir param)
    {
        SimplePool2.Spawn(vfxSoundWave, param.gameObject.transform.position, Quaternion.identity);
        foreach (var item in param.GetBallondsAround())
        {
 
            item.TakeDameSpike(IncreaseSoundWaveCard.dame);
        }
    }

    public override bool CanShow()
    {
        if(isActive)
        {
            return false;
        }    
        return true;
    }
}