using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class NativeAds_Box : MonoBehaviour
{
    public Text tvTime;
    public GameObject btnCountDown;
    public Button btnClose;
    public RawImageNativeAdsFullAdsGamePlay rawImageNativeAds;
    float timeCountDown;
    Action callBackAds;
    public void Init()
    {
        btnClose.onClick.AddListener(delegate { rawImageNativeAds.gameObject.SetActive(false); callBackAds?.Invoke(); });
        timeCountDown  = RemoteConfigController.GetFloatConfig(FirebaseConfig.TIME_OFF_NATIVE_FULL,3);
    }    

    public void HandleShowNativeGamePlay(Action CallBack)
    {
        callBackAds = CallBack;
        GameController.Instance.admobAds.HideBanner();
        btnClose.gameObject.SetActive(false);
        btnCountDown.gameObject.SetActive(true);
        if (GameController.Instance.admobAds.nativeFullGameplay.isLoadNativeOK)
        {
         
            rawImageNativeAds.gameObject.SetActive(true);
            rawImageNativeAds.Init(CallBack);
            StartCoroutine(countTime());
        }    
        else
        {
            CallBack?.Invoke();
            rawImageNativeAds.gameObject.SetActive(false);
        }
    }

    IEnumerator countTime()
    {
        tvTime.text = "" + timeCountDown;
        yield return new WaitForSeconds(1);
        timeCountDown -= 1;
        tvTime.text = "" + timeCountDown;
        if(timeCountDown > 0)
        {
            StartCoroutine(countTime());
        }
        else
        {
         
            StopAllCoroutines();
            btnClose.gameObject.SetActive(true);
            btnCountDown.gameObject.SetActive(false);
        }
    }

  
}
