using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutGameplay_Step_2 : TutorialBase
{
    public GameObject currentHand;
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
            currentHand.transform.parent = GamePlayController.Instance.gameScene.speedButton.gameObject.transform;
            var temp2 = GamePlayController.Instance.gameScene.speedButton.gameObject.transform.position;
            currentHand.transform.position = new Vector3(temp2.x+0.5f, temp2.y-0.8f, temp2.z);
       
            currentHand.transform.localScale = new Vector3(1,1,1);
            currentHand.gameObject.GetComponent<HandTutUI>().Init();
            currentHand.gameObject.GetComponent<HandTutUI>().post = new Vector3(-0.3f,0.3f);
        }
    }



    protected override void SetNameTut()
    {

    }
    public override void OnEndTut()
    {

    }
}