using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownBallButton : MonoBehaviour
{
    public InputThone inputThone;
    public float time;
    public bool wasCount;
    public GameObject handTut;
    private void OnEnable()
    {
        time = 0;
        StartCountTime();
    }
    public void StartCountTime()
    {
        wasCount = true;

    }
    public void StopCountTime()
    {
     
        wasCount = false;
        time = 0;
        handTut.gameObject.SetActive(false);

    }



    public void OnMouseDown()
    {
        StartCoroutine(inputThone.StopActiveMove(delegate { inputThone.HandleSetUp(); }));
        StopCountTime();
    }

    public void Update()
    {
        if(wasCount)
        {
            time += Time.deltaTime;
            if(time > 18)
            {
                handTut.gameObject.SetActive(true);
                wasCount = false;
            }
        }
    }
    public void OnDisable()
    {
        StopCountTime();
    }

}
