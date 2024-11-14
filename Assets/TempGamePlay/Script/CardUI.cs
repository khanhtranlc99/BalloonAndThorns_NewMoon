using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using DG.Tweening;

public class CardUI : MonoBehaviour
{
    Winbox winbox;
    public Text tvName;
    public Image backGround;
    public Image icon;
    public Button btnClick;
    public Text tvContent;
    public Button btnWatchAds;
    public bool isChoose;
    public CanvasGroup canvasGroup;
    public Sprite whiteSprite;
    public Sprite blueSprite;
    public Sprite yellowSprite;

    public void Init(Winbox pramWinBox, CardBase paramBase )
    {
        canvasGroup.alpha = 1;
        switch (paramBase.cardRank)
        {
            case CardRank.Normal:
                backGround.sprite = whiteSprite;
                break;
            case CardRank.Platium:
                backGround.sprite = blueSprite;
                break;
            case CardRank.Gold:
                backGround.sprite = yellowSprite;
                break;
        }
        isChoose = false;
        icon.sprite = paramBase.sprite;
        tvName.text = paramBase.name;
        tvContent.text = paramBase.content;
        btnClick.onClick.RemoveAllListeners();
        btnClick.onClick.AddListener(delegate { HandleBtnOnclick(); });
        btnWatchAds.onClick.RemoveAllListeners();
        btnWatchAds.onClick.AddListener(delegate { HandleOnClickWatch();  });
        btnWatchAds.gameObject.SetActive(false);
        winbox = pramWinBox;

        void HandleBtnOnclick()
        {
            if (!isChoose)
            {
                isChoose = true;
                paramBase.HandleAction();

                canvasGroup.DOFade(0, 0.3f).OnComplete(delegate { winbox.OnWatchCard();  });
            }        
        }
        void HandleOnClickWatch()
        {
            if (!isChoose)
            {
                isChoose = true;
                GameController.Instance.admobAds.ShowVideoReward(
             actionReward: () =>
             {
                 paramBase.HandleAction();
                 canvasGroup.DOFade(0, 0.3f).OnComplete(delegate {
                     if(winbox.isAllOff)
                     {
                         winbox.HandleOffPopup();
                     }                                                    
                });

             },
             actionNotLoadedVideo: () =>
             {
                 btnWatchAds.transform.SetAsLastSibling();
                 GameController.Instance.moneyEffectController.SpawnEffectText_FlyUp_UI
                  (
                     btnWatchAds.transform
                     ,
                  btnWatchAds.transform.position,
                  "No video",
                  Color.white,
                  isSpawnItemPlayer: true
                  );
             },
             actionClose: null,
             ActionWatchVideo.OpenMoreCard,
             UseProfile.CurrentLevel.ToString());

            }
        }

    }

    public void HandleClear()
    {
        btnClick.onClick.RemoveAllListeners();
    }    



}
