using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class BallonRocket : Ballon
{
    public GameObject vfxRocket;
    public override void Explosion()
    {
        base.Explosion();
        var temp = SimplePool2.Spawn(vfxRocket);
      
        temp.transform.position = transform.position;
        temp.transform.localScale = new Vector3(-1, 1, 1);
        temp.transform.DOMove(Vector3.zero, 1).OnComplete(delegate {

            try
            {
               GamePlayController.Instance.playerContain.levelData.AllBallHit();
            }
            catch
            {

            }
            //GamePlayController.Instance.playerContain.levelData.CountWin(null);
       
            SimplePool2.Despawn(temp);
        });
    }
}
