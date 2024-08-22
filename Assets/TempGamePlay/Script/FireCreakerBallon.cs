using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FireCreakeType
{ 
    UpDown,
    RightLeft
}
public class FireCreakerBallon : Ballon
{

 
    public GameObject demoSpike;
    public FireCreaker fireCreaker_1;
  
    public FireCreakeType fireCreakeType;

    public override void Init()
    {
        base.Init();
        SimplePool2.Preload(fireCreaker_1.gameObject, 2);
    }

    public override void TakeDameSpike()
    {

        spriteRenderer.sprite = eplosionBallon;

        if (!isOff)
        {
            isOff = true;
            GameController.Instance.musicManager.PlayRandomBallon();
            GamePlayController.Instance.gameScene.HandleSubtrackBallon();
            GamePlayController.Instance.playerContain.levelData.CountWin(this.transform);
            demoSpike.gameObject.SetActive(false);
            
            switch(fireCreakeType)
            {
                case FireCreakeType.UpDown:

                    var tempUp = SimplePool2.Spawn(fireCreaker_1);
                    tempUp.transform.position = this.transform.position;
                    tempUp.Init(new Vector3(0,1,0));
     

                    var tempDown = SimplePool2.Spawn(fireCreaker_1);
                    tempDown.transform.position = this.transform.position;
                    tempDown.Init(new Vector3(0, -1, 0));
                    tempDown.transform.localEulerAngles = new Vector3(0,0,180);
                    break;

                case FireCreakeType.RightLeft:

                    var tempLeft = SimplePool2.Spawn(fireCreaker_1);
                    tempLeft.transform.position = this.transform.position;
                    tempLeft.Init(new Vector3(-1, 0, 0));
                    tempLeft.transform.localEulerAngles = new Vector3(0, 0, 90);


                    var tempRight = SimplePool2.Spawn(fireCreaker_1);
                    tempRight.transform.position = this.transform.position;
                    tempRight.Init(new Vector3(1, 0, 0));
                    tempRight.transform.localEulerAngles = new Vector3(0, 0, -90);

                    break;
            }

        }


    }
}