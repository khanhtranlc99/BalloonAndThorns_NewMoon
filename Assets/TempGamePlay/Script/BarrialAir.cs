using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BarrialAir : MonoBehaviour
{
    public int id;
    public int idInLevelT;
    public List<Ballon> lsNearBallon;
    public List<Ballon> lsUpBallon;
    public abstract void Init();
    public abstract void TakeDameSpike();

    public Ballon GetBallShame(int id)
    {
        foreach (var item in lsNearBallon)
        {
            if(item.id == id)
            {
                return item;
            }
        }
        return null;
    }
}
