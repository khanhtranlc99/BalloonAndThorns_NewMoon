using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;
using UnityEngine.SceneManagement;
using DG.Tweening;
public class HomeController : Singleton<HomeController>
{
    public Button btnNext;
    public Button btnStartGame;
    public List<Image> lsImage;
    public HorizontalScrollSnap scrollSnap;
    public GameObject panelNext;
    public GameObject panelStart;
    public RawImageNativeAds ads_navitve;
    public RawImageNativeAds ads_navitve_1;
    public RawImageNativeAds ads_navitve_2;
    public RawImageNativeAdsFullAds ads_navitve_Full;

    public Image bg;
    bool isOpen = false;
    public void Start()
    {
        isOpen = false;
        btnNext.onClick.AddListener(HandleNextButton);
        btnStartGame.onClick.AddListener(HandleStartButton);

        if (GameController.Instance.admobAds.nativeGoogleAdsMobe_3.isLoadNativeOK  )
        {
            ads_navitve.Init(GameController.Instance.admobAds.nativeGoogleAdsMobe_3.nativeAd);
        }
        else
        {
            ads_navitve.gameObject.SetActive(false);
        }
        if (GameController.Instance.admobAds.nativeGoogleAdsMobe_4.isLoadNativeOK)
        {
            ads_navitve_1.Init(GameController.Instance.admobAds.nativeGoogleAdsMobe_4.nativeAd);
        }
        else
        {
            ads_navitve_1.gameObject.SetActive(false);
        }
        if (GameController.Instance.admobAds.nativeGoogleAdsMobe_5.isLoadNativeOK)
        {
            ads_navitve_2.Init(GameController.Instance.admobAds.nativeGoogleAdsMobe_5.nativeAd);
        }
        else
        {
            ads_navitve_2.gameObject.SetActive(false);
        }
        if (GameController.Instance.admobAds.nativeGoogleAdsMobe_6.isLoadNativeOK)
        {
            ads_navitve_Full.Init(GameController.Instance.admobAds.nativeGoogleAdsMobe_6.nativeAd);
        }
        else
        {
            ads_navitve_Full.gameObject.SetActive(false);
        }
        GameController.Instance.admobAds.admobSplash.HideBanner();


    }

 
    private void Update()
    {
        for (int i = 0; i < lsImage.Count; i++)
        {
            if (i <= scrollSnap.CurrentPage)
            {
                lsImage[i].color = Color.yellow;
            }
            else
            {
                lsImage[i].color = Color.white;
            }
        }
        if (scrollSnap.CurrentPage == 3)
        {
            panelNext.SetActive(false);
            panelStart.SetActive(true);
            //if (!isOpen)
            //{
            //    isOpen = true;
            //    if (GameController.Instance.admobAds.nativeGoogleAdsMobe_6.isLoadNativeOK)
            //    {
            //        ads_navitve_Full.gameObject.SetActive(true);
            //    }

            //}

        }
        else
        {
            panelNext.SetActive(true);
            panelStart.SetActive(false);
        }


    }
    private void HandleNextButton()
    {
        scrollSnap.NextScreen();
    }
    private void HandleStartButton()
    {
        if (GameController.Instance.admobAds.IsLoadedAOA())
        {
            GameController.Instance.admobAds.ShowOpenAppAdsReady(actionIniterClose: () => {

                bg.DOFade(1, 0.5f).OnComplete(delegate { SceneManager.LoadSceneAsync(SceneName.GAME_PLAY); });

            });
        }
        else if (GameController.Instance.admobAds.IsLoadedInterstitial())
        {
            GameController.Instance.admobAds.ShowInterstitialAd(actionIniterClose: () => {

                bg.DOFade(1, 0.5f).OnComplete(delegate { SceneManager.LoadSceneAsync(SceneName.GAME_PLAY); });

            });
        }


        if (!GameController.Instance.admobAds.IsLoadedAOA() && !GameController.Instance.admobAds.IsLoadedInterstitial())
        {
            bg.DOFade(1, 0.5f).OnComplete(delegate { SceneManager.LoadSceneAsync(SceneName.GAME_PLAY); });
        }
      

    }
 

}
