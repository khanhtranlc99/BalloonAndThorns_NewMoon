using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class ThoneDemo : MonoBehaviour
{
    public Transform postShoot;
  
    public SpriteRenderer spriteRenderer;
    // Update is called once per frame
   


    public void HandleScale()
    {
        ScaleIcon();
    }
    public void OffScale()
    {
        this.transform.DOKill();
        this.transform.localScale = new Vector3(1, 1, 1);
    }
    private void  ScaleIcon()
    {
        this.transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.5f).OnComplete(delegate {
            this.transform.DOScale(new Vector3(1, 1, 1), 0.5f).OnComplete(delegate {

                ScaleIcon();
            });

        });
    }    


}
