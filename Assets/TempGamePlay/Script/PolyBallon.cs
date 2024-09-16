using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolyBallon : MonoBehaviour
{
    public Ballon ballon;


    public void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.gameObject.GetComponent<Ballon>() != null && collision.gameObject.GetComponent<Ballon>() != ballon)
        //{
        //    if (!ballon.lsUpBallon.Contains(collision.gameObject.GetComponent<Ballon>()))
        //    {
        //        ballon.lsUpBallon.Add(collision.gameObject.GetComponent<Ballon>());
        //    }

        //}
    }
}
