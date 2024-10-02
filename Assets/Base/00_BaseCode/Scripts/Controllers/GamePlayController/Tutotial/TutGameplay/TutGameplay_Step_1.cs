using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class TutGameplay_Step_1 : TutorialBase
{
    GameObject currentHand;
    Vector3 postTarget
    { 
     get
        {
            return GamePlayController.Instance.playerContain.levelData.inputThone.postFireSpike.transform.position;
        }    
    }
    Vector3 postTargetSecond
    {
        get
        {
            return GamePlayController.Instance.playerContain.levelData.inputThone.botPost.position;
        }
    }
    public override bool IsCanEndTut()
    {
        if (currentHand != null)
        {
            Destroy(currentHand.gameObject);
        }

        return true;

    }

    public override void StartTut()
    {
        if (UseProfile.CurrentLevel == 1)
        {
            if (currentHand != null)
            {
                return;
            }
            currentHand = SimplePool2.Spawn(handTut);
            currentHand.transform.position = new Vector3(postTarget.x + 0.5f  , postTarget.y , -5);
            MoveHand();

        }
    }

    private void MoveHand()
    {
    
        currentHand.transform.DOMove(new Vector3(postTargetSecond.x -0.5f, postTargetSecond.y, -5), 1).OnComplete(delegate {

            currentHand.transform.position = new Vector3(postTarget.x + 0.5f, postTarget.y , -5);
            MoveHand();
        });
    }    
    


    protected override void SetNameTut()
    {

    }
    public override void OnEndTut()
    {

    }
}
