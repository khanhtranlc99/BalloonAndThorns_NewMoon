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
            return GamePlayController.Instance.playerContain.inputThone.postFireSpike.transform.position;
        }    
    }
    Vector3 postTargetSecond
    {
        get
        {
            return GamePlayController.Instance.playerContain.inputThone.postFireSpike.transform.position;
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
            currentHand.gameObject.GetComponent<HandTutGamePlay>().Init(GamePlayController.Instance.playerContain.inputThone );
            //currentHand.transform.position = new Vector3(postTarget.x + 0.5f  , postTarget.y , -5);

        }
    }

    


    protected override void SetNameTut()
    {

    }
    public override void OnEndTut()
    {

    }
}
