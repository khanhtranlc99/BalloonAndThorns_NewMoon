using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class BroadContry : MonoBehaviour
{
    public List<LabelContry> lsLabelContries;
    public Button btnChangeScene;

    public void Start()
    {
        GameController.Instance.admobAds.HandleShowMerec();
        Init();
        btnChangeScene.onClick.AddListener(HandleChangeScene);
    }

    public void Init()
    {
        foreach(var item in lsLabelContries)
        {
            item.Init();
        }
  
    }
    public void HandleOffAll()
    {
        foreach (var item in lsLabelContries)
        {
            item.HandleOff();
        }
    }

    public void HandleChangeScene()
    {
        SceneManager.LoadScene(SceneName.SCENE_TUTORIAL);
        GameController.Instance.admobAds.HandleHideMerec();
    }


}
