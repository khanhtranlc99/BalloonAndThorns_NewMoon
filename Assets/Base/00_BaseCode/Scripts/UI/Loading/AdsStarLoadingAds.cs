using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class AdsStarLoadingAds : MonoBehaviour
{
    public GameObject iconLoad;
    bool wasLoad = false;
    public AdmobAds admobAds;
    public void Init()
    {
     
        wasLoad = true;
        StartCoroutine(Helper.StartAction(delegate { HandleSpamInter(); }, () => admobAds.IsLoadedInterstitial() == true));
     //   HandleSpamInter();
    }

    private void HandleSpamInter()
    {
        admobAds.ShowInterstitial(false, actionIniterClose: () => {

            StartCoroutine(ChangeScene());

        }, actionWatchLog: "AdsStarLoadingAds");
    }


    private void Update()
    {
      
            iconLoad.transform.localEulerAngles -= new Vector3(0, 0, 50)*Time.deltaTime;
     
    }

    private IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneName.SELECT_LANGUAGE);
    }
}
