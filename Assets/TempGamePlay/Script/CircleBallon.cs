using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CircleBallon : MonoBehaviour
{
    public Ballon ballon;

    
    public void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.gameObject.GetComponent<Ballon>() != null && collision.gameObject.GetComponent<Ballon>() != ballon)
        //{
        //    if (!ballon.lsNearBallon.Contains(collision.gameObject.GetComponent<Ballon>()))
        //    {
        //        ballon.lsNearBallon.Add(collision.gameObject.GetComponent<Ballon>());
        //    }

        //}
    }



}
