using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
public class ItemInGameBallon : MonoBehaviour
{
    public TMP_Text tvNumb;
    public SpriteRenderer spriteRenderer;
    public void Init(int param)
    {
        tvNumb.DOFade(1, 0.1f);
        tvNumb.text = "" + param;
        spriteRenderer.sprite = null;
        this.transform.localScale = Vector3.zero;
        this.transform.DOScale(new Vector3(1,1,1),0.5f).OnComplete(delegate {

            this.transform.DOMoveY( this.transform.position.y + 0.3f, 0.5f).OnComplete(delegate {

                tvNumb.DOFade(0, 0.2f).OnComplete(delegate { SimplePool2.Despawn(this.gameObject); });
           
            });
        });
    }
    public void Init(Sprite param)
    {
        tvNumb.text = "";
        spriteRenderer.sprite = param;
        spriteRenderer.color = Color.white;
        this.transform.localScale = Vector3.zero;
        this.transform.DOScale(new Vector3(0.75f, 0.75f, 0.75f), 0.5f).OnComplete(delegate {

            this.transform.DOMoveY(this.transform.position.y + 0.3f, 0.5f).OnComplete(delegate {

                spriteRenderer.DOColor(new Color32(0, 0, 0, 0), 0.2f).OnComplete(delegate { SimplePool2.Despawn(this.gameObject); });

            });
        });
    }

}
