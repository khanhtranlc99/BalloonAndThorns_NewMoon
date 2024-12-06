using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
public class BroadContry : MonoBehaviour
{
    public List<LabelContry> lsLabelContries;
    public Button btnChangeScene;
    public RawImageNativeAds rawImageNativeAds;
    public RawImageNativeAds rawImageNativeAds_2;
    bool click;
    bool isNative_lg_1;
    bool isNative_lg_2;
    public Image blind;
    public Text tvPanel;
    public void Awake()
    {
        click = false;
        Init();
        btnChangeScene.onClick.AddListener(HandleChangeScene);

    
        isNative_lg_1 = RemoteConfigController.GetBoolConfig(FirebaseConfig.IS_SHOW_NATIVE_LANGUAGE_1_1, true);
        isNative_lg_2 = RemoteConfigController.GetBoolConfig(FirebaseConfig.IS_SHOW_NATIVE_LANGUAGE_1_2, true);

        GameController.Instance.admobAds.nativeGoogleAdsMobe_1.Init(delegate { ShowNative_1(); });
        GameController.Instance.admobAds.nativeGoogleAdsMobe_2.Init(delegate { ShowNative_2(); });
        blind.DOFade(0, 10).OnComplete(delegate { blind.gameObject.SetActive(false); });
        StartCoroutine(ShowText());
    }
    public IEnumerator ShowText()
    {
        yield return new WaitForSeconds(1);
        tvPanel.text = "Waits 10 second...";
        yield return new WaitForSeconds(1);
        tvPanel.text = "Waits 9 second..";
        yield return new WaitForSeconds(1);
        tvPanel.text = "Waits 8 second.";
        yield return new WaitForSeconds(1);
        tvPanel.text = "Waits 7 second...";
        yield return new WaitForSeconds(1);
        tvPanel.text = "Waits 6 second..";
        yield return new WaitForSeconds(1);
        tvPanel.text = "Waits 5 second.";
        yield return new WaitForSeconds(1);
        tvPanel.text = "Waits 4 second...";
        yield return new WaitForSeconds(1);
        tvPanel.text = "Waits 3 second..";
        yield return new WaitForSeconds(1);
        tvPanel.text = "Waits 2 second.";
        yield return new WaitForSeconds(1);
        tvPanel.text = "Waits 1 second...";
    }    
    private void ShowNative_1()
    {
        if(isNative_lg_1)
        {
            rawImageNativeAds.gameObject.SetActive(true);
            rawImageNativeAds.Init(GameController.Instance.admobAds.nativeGoogleAdsMobe_1.nativeAd);
        }    
       
    }
    private void ShowNative_2()
    {
        if (isNative_lg_2)
        {
            rawImageNativeAds_2.gameObject.SetActive(true);
            rawImageNativeAds_2.Init(GameController.Instance.admobAds.nativeGoogleAdsMobe_2.nativeAd);
        }
      
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
                if(isNative_lg_2)
                {
                    rawImageNativeAds_2.gameObject.SetActive(true);
                }     
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
        if(GameController.Instance.admobAds.nativeGoogleAdsMobe_1.isLoadNativeOK && GameController.Instance.admobAds.nativeGoogleAdsMobe_2.isLoadNativeOK)
        {
            if(blind.gameObject.activeSelf)
            {
                blind.DOKill();
                blind.gameObject.SetActive(false);
            }    
        }    
    }
}
