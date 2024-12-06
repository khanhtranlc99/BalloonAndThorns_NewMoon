using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class AdsStarLoadingAds : MonoBehaviour
{
    public GameObject iconLoad;
    bool wasLoad = false;
    public AdmobAdsGoogle admobAds;
    public Text txtLoading;
    public void Init()
    {
     
        wasLoad = true;
        StartCoroutine(HandleSpamInter());
        //    StartCoroutine(Helper.StartAction(delegate { HandleSpamInter(); }, () => admobAds.IsLoadedInterstitial() == true));
        //   HandleSpamInter();
        StartCoroutine(LoadingText());
    }

    private IEnumerator HandleSpamInter()
    {
 
        yield return new WaitForSeconds(5.5f);
       
        if (admobAds.admobSplash.IsLoadedAOA())
        {
            admobAds.admobSplash.HideBanner();
            admobAds.admobSplash.ShowOpenAppAdsReady(actionIniterClose: () => {

                StartCoroutine(ChangeScene());

            });
        }
        else if (admobAds.admobSplash.IsLoadedInterstitial())
        {
            admobAds.admobSplash.HideBanner();
            admobAds.admobSplash.ShowInterstitialAd(actionIniterClose: () => {

                StartCoroutine(ChangeScene());

            });
         
        }
        if( !admobAds.admobSplash.IsLoadedAOA() && !admobAds.admobSplash.IsLoadedInterstitial())
        {
            StartCoroutine(ChangeScene());
        }
     
        
    }


    private void Update()
    {
      
            iconLoad.transform.localEulerAngles -= new Vector3(0, 0, 100)*Time.deltaTime;
     
    }

    private IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(1);
        Initiate.Fade(SceneName.SELECT_LANGUAGE, Color.black, 2f);
        // SceneManager.LoadScene(SceneName.SELECT_LANGUAGE);
    }
    IEnumerator LoadingText()
    {
        var wait = new WaitForSeconds(1f);
        while (true)
        {
            txtLoading.text = "This action constain ads.";
            yield return wait;

            txtLoading.text = "This action constain ads..";
            yield return wait;

            txtLoading.text = "This action constain ads...";
            yield return wait;

        }
    }

}
