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
 
    public Text tvLevel;
    public Button settinBtn;
    public Button reStartBtn;
 
    public void Init(LevelData levelData)
    {
        settinBtn.onClick.AddListener(delegate { SettingBox.Setup(false).Show(); });
        reStartBtn.onClick.AddListener(delegate { Initiate.Fade("GamePlay", Color.black, 2f); });
        tvLevel.text = "Level " + UseProfile.CurrentLevel;
    }

    public override void OnEscapeWhenStackBoxEmpty()
    {
     
    }
}
