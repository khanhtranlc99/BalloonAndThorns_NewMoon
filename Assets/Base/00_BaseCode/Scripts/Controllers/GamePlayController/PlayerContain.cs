using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using DG.Tweening;
public class PlayerContain : MonoBehaviour
{
    public LevelData levelData;
    public InputThone inputThone;
    public GameObject vfxRocket;
    public Transform canvas;
    public CameraFollow cameraFollow;
    public static bool testc1 = false;

    public Transform postBot;
    public void Init()
    {
        string pathLevel = StringHelper.PATH_CONFIG_LEVEL_TEST;
        levelData = Instantiate(Resources.Load<LevelData>(string.Format(pathLevel, UseProfile.CurrentLevel)));
        levelData.Init();
        inputThone = levelData.inputThone;
        levelData.inputThone.postFireSpike.transform.position = new Vector3(levelData.inputThone.postFireSpike.transform.position.x, postBot.position.y,0);
    }

    public void Test_1()
    {
        inputThone.numOfReflect = 4;
    }

    public void Test_2()
    {

        levelData.PlusBall();
    }

    public void Test_3()
    {
        levelData.inputThone.postFireSpike.HandleBooster();
    }

    public void Test_4()
    {
        var temp = SimplePool2.Spawn(vfxRocket);
        temp.transform.parent = canvas;
        temp.transform.localPosition = Vector3.zero;
        temp.transform.localScale = new Vector3(-1,1,1);
        temp.transform.DOMove(Vector3.zero, 1).OnComplete(delegate {

            levelData.AllBallHit();
            //GamePlayController.Instance.playerContain.levelData.CountWin(null);
            testc1 = true;
            SimplePool2.Despawn(temp);
        });
        
    }

}
