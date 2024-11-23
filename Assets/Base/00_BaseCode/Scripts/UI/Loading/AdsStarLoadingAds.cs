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
    public void Init()
    {
     
        wasLoad = true;
        StartCoroutine(HandleSpamInter());
    //    StartCoroutine(Helper.StartAction(delegate { HandleSpamInter(); }, () => admobAds.IsLoadedInterstitial() == true));
    //   HandleSpamInter();
    }

    private IEnumerator HandleSpamInter()
    {
 
        yield return new WaitForSeconds(4);
       
        if (admobAds.admobSplash.IsLoadedAOA())
        {
            admobAds.admobSplash.ShowOpenAppAdsReady(actionIniterClose: () => {

                StartCoroutine(ChangeScene());

            });
        }
        else if (admobAds.admobSplash.IsLoadedInterstitial())
        {
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
        SceneManager.LoadScene(SceneName.SELECT_LANGUAGE);
    }
}
