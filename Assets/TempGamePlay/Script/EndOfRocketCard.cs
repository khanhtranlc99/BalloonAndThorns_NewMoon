using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfRocketCard : CardBase
{
    public bool isActive = false;
    public RocketVfxWork vfxRocket;
    List<BarrialAir> lsData
    {
        get
        {
            return GamePlayController.Instance.playerContain.levelData.lsBallons;
        }
    }

    public override void Init()
    {

    }
    public override void HandleAction()
    {
        isActive = true;
        SimplePool2.Preload(vfxRocket.gameObject, 50);
        GamePlayController.Instance.playerContain.effectExplosion.effectExplosion += HandleEndOfRocket;
    }

    private void HandleEndOfRocket(BarrialAir param)
    {
      
        var temp = new List<BarrialAir>();
        for(int i = 0; i < lsData.Count; i ++)
        {
            var tempBallon = lsData[Random.RandomRange(0, lsData.Count)];
            if(!tempBallon.isOff && temp.Count < 1)
            {
                temp.Add(tempBallon);
            }
        }
        for (int i = 0; i < temp.Count; i++)
        {
            var tempVfx = SimplePool2.Spawn(vfxRocket, param.gameObject.transform.position, Quaternion.identity);
            tempVfx.Init(temp[i]);
        }


    }
    public override bool CanShow()
    {
        if (isActive)
        {
            return false;
        }
        return true;
    }
}