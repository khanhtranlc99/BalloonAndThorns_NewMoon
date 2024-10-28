using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownBallButton : MonoBehaviour
{
    public InputThone inputThone;

    public void OnMouseDown()
    {
        StartCoroutine(inputThone.StopActiveMove(delegate { inputThone.HandleSetUp(); }));
      
    }
}
