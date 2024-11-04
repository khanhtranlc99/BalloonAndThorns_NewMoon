using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutSpinerBooster_Step_1 : TutorialBase
{
    GameObject currentHand;

    public override bool IsCanEndTut()
    {
        if (currentHand != null)
        {
            Destroy(currentHand.gameObject);
        }

        return base.IsCanShowTut();

    }

    public override void StartTut()
    {
        if (UseProfile.CurrentLevel_Chapper_I == 7)
        {
            if (currentHand != null)
            {
                return;
            }
            currentHand = SimplePool2.Spawn(handTut);
            currentHand.transform.parent = GamePlayController.Instance.playerContain.spinerBooster.sniper_Btn.transform;
            currentHand.transform.localScale = new Vector3(1, 1, 1);
            currentHand.transform.localEulerAngles = new Vector3(0, 0, 120);
            currentHand.transform.position = new Vector3(post.x + 0.5f, post.y + 0.7f, post.z);
            currentHand.gameObject.GetComponent<HandTutUI>().Init();
        }
    }
    Vector3 post
    {
        get
        {
            return GamePlayController.Instance.playerContain.spinerBooster.sniper_Btn.transform.position;
        }
    }


    protected override void SetNameTut()
    {

    }
}
