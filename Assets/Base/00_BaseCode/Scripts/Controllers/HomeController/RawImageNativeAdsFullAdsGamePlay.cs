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
    public void Init(Action callBack)
    {
        callbackClose = callBack;
        NativeAd nativeAd = GameController.Instance.admobAds.nativeFullGameplay.nativeAd;
        rawimageIcon.texture = nativeAd.GetIconTexture();
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
        GameController.Instance.admobAds.ShowBanner();
        this.gameObject.SetActive(false);

    }
    
}
