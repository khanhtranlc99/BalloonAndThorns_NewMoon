using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ContinueGameBox : BaseBox
{
    public static ContinueGameBox _instance;
    public static ContinueGameBox Setup()
    {
        if (_instance == null)
        {
            _instance = Instantiate(Resources.Load<ContinueGameBox>(PathPrefabs.CONTINUE_GAME_BOX));
            _instance.Init();
        }
        _instance.InitState();
        return _instance;
    }
    public Button btnYes;
    public Button btnNo;
    private void Init()
    {
        btnYes.onClick.AddListener(HandleYesBtn);
        btnNo.onClick.AddListener(HandleNoBtn);
    }
    private void InitState()
    {

    }

    private void  HandleYesBtn()
    {
        GameController.Instance.isContinue = true;
        GameController.Instance.SetUp();
        Close();
    }
    private void HandleNoBtn()
    {
        UseProfile.CurrentLevel = 1;
        UseProfile.CurrentLevel_Chapper_I = 1;
        UseProfile.CurrentLevel_Chapper_II = 1;
        GameController.Instance.isContinue = false;
        GameController.Instance.SetUp();
        Close();
    }

}
