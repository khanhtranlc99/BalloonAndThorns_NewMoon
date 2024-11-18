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
    public RawImage ads_navitve;
    public RawImage ads_navitve_1;
    public RawImage ads_navitve_2;
    public RawImage ads_navitve_3;
    public RawImage ads_navitve_4;
    public Image bg;

    public void Start()
    {
        btnNext.onClick.AddListener(HandleNextButton);
        btnStartGame.onClick.AddListener(HandleStartButton);

        if (GameController.Instance.admobAds.googleAdsMobe.iconTexture != null)
        {
            ads_navitve.texture = GameController.Instance.admobAds.googleAdsMobe.iconTexture;
            ads_navitve_1.texture = GameController.Instance.admobAds.googleAdsMobe.iconTexture;
            ads_navitve_2.texture = GameController.Instance.admobAds.googleAdsMobe.iconTexture;
            ads_navitve_3.texture = GameController.Instance.admobAds.googleAdsMobe.iconTexture;
            ads_navitve_4.texture = GameController.Instance.admobAds.googleAdsMobe.iconTexture;
        }

    
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
        if (scrollSnap.CurrentPage == 4)
        {
            panelNext.SetActive(false);
            panelStart.SetActive(true);
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
      GameController.Instance.admobAds.ShowInterstitial(true, actionIniterClose: () => {

          bg.DOFade(1, 0.5f).OnComplete(delegate { SceneManager.LoadSceneAsync(SceneName.GAME_PLAY); });
       

      }, actionWatchLog: "LoseBox");
    
    }
 

}
