using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ballon : MonoBehaviour
{
    public Sprite eplosionBallon;
    public SpriteRenderer spriteRenderer;
    public GameObject objString;
    public bool isOff;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
          
            spriteRenderer.sprite = eplosionBallon;
            objString.SetActive(false);
            if(!isOff)
            {
                isOff = true;
                GameController.Instance.musicManager.PlayRandomBallon();
                GamePlayController.Instance.playerContain.levelData.CountWin();
            }    
       
        }
    }
 
}
