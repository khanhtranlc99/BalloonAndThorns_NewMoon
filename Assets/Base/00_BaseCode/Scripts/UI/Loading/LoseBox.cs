using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LoseBox : BaseBox
{
    public static LoseBox _instance;
    public static LoseBox Setup()
    {
        if (_instance == null)
        {
            _instance = Instantiate(Resources.Load<LoseBox>(PathPrefabs.LOSE_BOX));
            _instance.Init();
        }
        _instance.InitState();
        return _instance;
    }
 
    public Button btnRetry;



    public void Init()
    {
        GameController.Instance.musicManager.PlayLoseSound();
        btnRetry.onClick.AddListener(delegate { Initiate.Fade("GamePlay", Color.black, 2f); });
    }   
    public void InitState()
    {
       
    }
   

}
