using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using System;
using MoreMountains.NiceVibrations;
using UnityEngine.Events;

public class GameScene : BaseScene
{
    public Transform iconBallon;
    public Text tvTarget;
    public Text tvLevel;
    public Button settinBtn;
    public Button reStartBtn;
    int targetBallond;
 
    public void Init(LevelData levelData)
    {
        settinBtn.onClick.AddListener(delegate { SettingBox.Setup(false).Show(); });
        //reStartBtn.onClick.AddListener(delegate { Initiate.Fade("GamePlay", Color.black, 2f); });
        tvLevel.text = "Level " + UseProfile.CurrentLevel;
        targetBallond = levelData.numbTarget;
        tvTarget.text = "" + targetBallond;
    }


    public void HandleSubtrackBallon()
    {
        targetBallond -= 1;
        tvTarget.text = "" + targetBallond;
        iconBallon.transform.DOScale(new Vector3(1.2f,1.2f,1.2f), 0.3f).OnComplete(delegate {
            iconBallon.transform.DOScale(new Vector3(1, 1, 1), 0.3f).OnComplete(delegate {

         

            });
        });
    }    
    public override void OnEscapeWhenStackBoxEmpty()
    {
     
    }
}
