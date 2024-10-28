using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
public class ListBallController : MonoBehaviour
{
    public List<BallBase> currentBallBases;
    public InputThone inputThone;
    public BallBase ballMovement;
    public List<BallBase> newList;
    public List<BallBase> GetListBallBase
    {
        get
        {
            newList = new List<BallBase>();
            if (currentBallBases.Count < 0)
            {
                for (int i = 0; i < inputThone.numbBall; i++)
                {
                    newList.Add(ballMovement);
                }
            }
            else
            {
                for (int i = 0; i < currentBallBases.Count; i++)
                {
                    newList.Add(currentBallBases[i]);
                }
                if(newList.Count < inputThone.numbBall)
                {
                    for (int i = newList.Count; i < inputThone.numbBall; i++)
                    {
                        newList.Add(ballMovement);
                    }

                }
            }
            return newList;
        }
    }
    public void Init()
    {
        SimplePool2.Preload(ballMovement.gameObject, 70);
    }    
  

}
