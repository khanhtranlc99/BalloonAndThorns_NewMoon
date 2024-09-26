using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockWall : MonoBehaviour
{
    public int id;
    public bool isMoveAll;
    public GameObject vfx;
    public void HandleDieWall()
    {   
            isMoveAll = true;
            this.gameObject.GetComponent<SpriteRenderer>().color = new Color32(0,0,0,0);
        this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        if (vfx != null)
            {
                vfx.SetActive(true);
            }       
    }
}
