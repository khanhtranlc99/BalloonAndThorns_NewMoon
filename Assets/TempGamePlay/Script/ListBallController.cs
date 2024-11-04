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
    public List<IdBall> lsIdBalls;
    public Sprite GetBalls 
    {
        get
        {
            foreach (var item in lsIdBalls)
            {
                if (item.id == UseProfile.id_ball_skin)
                {
                    return item.ballBase;
                }
            }
            return null;
        }
  
  
    }
    public List<SpriteRenderer> lsSpriteRender;
    public void Init()
    {
        SimplePool2.Preload(ballMovement.gameObject, 70);
        EventDispatcher.EventDispatcher.Instance.RegisterListener(EventID.CHANGE_ID_BALL, HandleReskin);
        HandleReskin(null);
    }
    public void HandleReskin(object param)
    {
        foreach(var item in lsSpriteRender)
        {
            item.sprite = GetBalls;
        }
       
    }
    private void OnDestroy()
    {
        EventDispatcher.EventDispatcher.Instance.RemoveListener(EventID.CHANGE_ID_BALL, HandleReskin);
    }


}

[System.Serializable]
public class IdBall
{
    public int id;
    public Sprite ballBase;
}
