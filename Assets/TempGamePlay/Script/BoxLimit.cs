using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxLimit : MonoBehaviour
{
   
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<BarrialAir>() != null && collision.gameObject.GetComponent<BarrialAir>().isInit)
        {
            LoseBox.Setup().Show();
        }
    }
}
