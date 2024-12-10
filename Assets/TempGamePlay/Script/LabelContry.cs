using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LabelContry : MonoBehaviour
{
    public BroadContry broadContry;
    public Button btnChoose;
    public Image changeState;
    public Sprite iconOn;
    public Sprite iconOff;

    public void Init()
    {
        btnChoose.onClick.AddListener(delegate { broadContry.HandleOffAll(); HandleOn();  });
    }
    public void HandleOn()
    {
        changeState.sprite = iconOn;
        this.GetComponent<Image>().color = new Color32(99, 152,255,255);
    }
    public void HandleOff()
    {
        changeState.sprite = iconOff;
        this.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
    }
}
