using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class ThoneDemo : MonoBehaviour
{
    public GameObject parent;
    public SpriteRenderer spriteRenderer;
    // Update is called once per frame
    public bool isBooster = false;
    public GameObject vfxBooster;

    public void HandleBooster()
    {
        isBooster = true;
        vfxBooster.SetActive(true);
        Debug.LogError("wasUseMoveSightingPointBooster");
    }    
    private void FixedUpdate()
    {
        this.transform.localEulerAngles -= new Vector3(0, 0, 5);
    }

    private void OnMouseDrag()
    {
        if(isBooster)
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
            parent.transform.position = new Vector3(worldPosition.x, parent.transform.position.y, 0);
            if (parent.transform.position.x <= GamePlayController.Instance.limitLeft.position.x)
            {
                parent.transform.position = new Vector3(GamePlayController.Instance.limitLeft.position.x, parent.transform.position.y, 0);

            }
            if (parent.transform.position.x >= GamePlayController.Instance.limitRight.position.x)
            {
                parent.transform.position = new Vector3(GamePlayController.Instance.limitRight.position.x, parent.transform.position.y, 0);

            }
        }    
     
    }
    private void OnMouseUp()
    {
        if (isBooster)
        {
            isBooster = false;
            GamePlayController.Instance.playerContain.moveSightingPointBooster.HandleOffBooster();
            vfxBooster.SetActive(false);
        }
    }

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
