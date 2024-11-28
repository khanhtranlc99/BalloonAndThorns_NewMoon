using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class BroadContry : MonoBehaviour
{
    public List<LabelContry> lsLabelContries;
    public Button btnChangeScene;
    public RawImageNativeAds rawImageNativeAds;
    public RawImageNativeAds rawImageNativeAds_2;
    bool click;

    public void Start()
    {
        click = false;
        Init();
        btnChangeScene.onClick.AddListener(HandleChangeScene);
        if(GameController.Instance.admobAds.nativeGoogleAdsMobe_1.isLoadNativeOK)
        {
            rawImageNativeAds.Init(GameController.Instance.admobAds.nativeGoogleAdsMobe_1.nativeAd);
        }
        else
        {
            rawImageNativeAds.gameObject.SetActive(false);
        }
        if (GameController.Instance.admobAds.nativeGoogleAdsMobe_2.isLoadNativeOK)
        {
            rawImageNativeAds_2.Init(GameController.Instance.admobAds.nativeGoogleAdsMobe_2.nativeAd);
        }
        else
        {
            rawImageNativeAds_2.gameObject.SetActive(false);
        }

       GameController.Instance.admobAds.nativeGoogleAdsMobe_3.Init();
       GameController.Instance.admobAds.nativeGoogleAdsMobe_4.Init();
       GameController.Instance.admobAds.nativeGoogleAdsMobe_5.Init();
       GameController.Instance.admobAds.nativeGoogleAdsMobe_6.Init();
  
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
        HandleClickLeangue();
    }

    public void HandleChangeScene()
    {
        SceneManager.LoadScene(SceneName.SCENE_TUTORIAL);

    }
    public void HandleClickLeangue()
    {
 
        btnChangeScene.gameObject.SetActive(true);
        if (!click)
        {
            rawImageNativeAds.gameObject.SetActive(false);
            if (GameController.Instance.admobAds.nativeGoogleAdsMobe_2.isLoadNativeOK)
            {
                rawImageNativeAds_2.Init(GameController.Instance.admobAds.nativeGoogleAdsMobe_2.nativeAd);
                rawImageNativeAds_2.gameObject.SetActive(true);
            }
            else
            {
                rawImageNativeAds_2.gameObject.SetActive(false);
            }    
            click = true;
        }
    }

    private void Update()
    {
        GameController.Instance.admobAds.admobSplash.HideBanner();
    }
}
