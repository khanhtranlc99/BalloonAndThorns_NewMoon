using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutGameplay_Step_1 : TutorialBase
{
    GameObject currentHand;
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
            //currentHand.transform.parent = GamePlayController.Instance.playerContain.TNT_Booster.btnTNT_Booster.transform;
            //currentHand.transform.localScale = new Vector3(1, 1, 1);
            //currentHand.transform.localEulerAngles = new Vector3(0, 0, 120);
        
            currentHand.transform.position = Vector3.zero;
            currentHand.gameObject.GetComponent<HandTutGamePlay>().Init(GamePlayController.Instance.playerContain.levelData.inputThone.postFireSpike);
        }
    }
    


    protected override void SetNameTut()
    {

    }
    public override void OnEndTut()
    {

    }
}
