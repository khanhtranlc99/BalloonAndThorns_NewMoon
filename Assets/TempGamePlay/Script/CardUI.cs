using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CardUI : MonoBehaviour
{
    public Image backGround;
    public Image icon;
    public Button btnClick;
    public Text tvContent;

    public void Init(CardBase paramBase, Action paramAction)
    {
        icon.sprite = paramBase.sprite;
   //     icon.SetNativeSize();
        tvContent.text = paramBase.content;
        btnClick.onClick.RemoveAllListeners();
        btnClick.onClick.AddListener(delegate { paramBase.HandleAction(); paramAction?.Invoke(); });
        switch (paramBase.cardRank)
        {
            case CardRank.Normal:
                backGround.color = Color.white;
                break;
            case CardRank.Platium:
                backGround.color = Color.blue;
                break;
            case CardRank.Gold:
                backGround.color = Color.yellow;
                break;
        }

    }

}
