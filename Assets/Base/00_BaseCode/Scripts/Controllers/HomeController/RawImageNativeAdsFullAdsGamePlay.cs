using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;
using UnityEngine.UI;
public class RawImageNativeAdsFullAdsGamePlay : MonoBehaviour
{
    public RawImage rawimageIcon;
    public RawImage rawimageBody;
    public RawImage rawimageAdChoice;
    public Text tvContent;
    public Text tvTiler;
    public Text tvButton;
    public GameObject btnShowAds;
    public bool isFullAds = false;
    Action callbackClose;
    bool isOn = false;
    public void Init(Action callBack)
    {
        callbackClose = callBack;
        NativeAd nativeAd = GameController.Instance.admobAds.nativeFullGameplay.nativeAd;
        if (nativeAd.GetIconTexture() != null)
        {
            rawimageIcon.texture = nativeAd.GetIconTexture();
            rawimageIcon.transform.parent.gameObject.SetActive(true);
        }
        else
        {
            rawimageIcon.transform.parent.gameObject.SetActive(false);
        }
        rawimageAdChoice.texture = nativeAd.GetAdChoicesLogoTexture();
        if (nativeAd.GetImageTextures().Count > 0)
        {
            List<Texture2D> goList = nativeAd.GetImageTextures();
            rawimageBody.texture = goList[0];
            List<GameObject> list = new List<GameObject>();
            list.Add(rawimageBody.gameObject);
            nativeAd.RegisterImageGameObjects(list);
        }
        tvTiler.text = nativeAd.GetHeadlineText(); // Tiêu đề
        tvContent.text = nativeAd.GetBodyText(); // Mô tả
        tvButton.text = nativeAd.GetCallToActionText(); // Nút kêu gọi    
        nativeAd.RegisterCallToActionGameObject(btnShowAds);
        GameController.Instance.admobAds.nativeFullGameplay.adLoader.OnNativeAdClosed += HandleAdNativeAdClicked;
    }
    private void HandleAdNativeAdClicked(object sender, EventArgs args)
    {
        callbackClose?.Invoke();
        this.gameObject.SetActive(false);

    }

    private void OnEnable()
    {
        isOn = true;
    }
    private void OnDisable()
    {
        isOn = false;
        GameController.Instance.admobAds.ShowBanner();


    }
    private void Update()
    {
        if(isOn)
        {
            GameController.Instance.admobAds.HideBanner();
        }
  
    }
}
