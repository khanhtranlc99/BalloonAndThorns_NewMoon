using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainSceneController : SceneBase
{
    public Button btnHome;
    public Text tvLevel;
    public Text tvLevelBtn;
    public RandomWatchVideo btnReward;
 
    public override void Init()
    {
        tvLevel.text =   UseProfile.CurrentLevel_Chapper_I.ToString();
        tvLevelBtn.text = "Level " + UseProfile.CurrentLevel_Chapper_I;
        btnHome.onClick.AddListener(delegate { OnClickPlay(); });
        btnReward.Init();
    }



    private void OnClickPlay()
    {
 
    }
}
