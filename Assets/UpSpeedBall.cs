using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UpSpeedBall : MonoBehaviour
{
    public SpriteRenderer outLine_1;
    public SpriteRenderer outLine_2;
    public SpriteRenderer outLine_3;
    void Start()
    {
        OffOutLine();
    }
 
    private void OffOutLine()
    {
        outLine_3.DOColor(new Color32(0,0,0,0),0.25f).OnComplete(delegate {
            outLine_2.DOColor(new Color32(0, 0, 0, 0), 0.25f).OnComplete(delegate {
                outLine_1.DOColor(new Color32(0, 0, 0, 0), 0.25f).OnComplete(delegate {
                    OnnOutLine();
                });
            });
        });
    }

    private void OnnOutLine()
    {
        outLine_3.DOColor(new Color32(255, 255, 255, 255), 0.25f).OnComplete(delegate {
            outLine_2.DOColor(new Color32(255, 255, 255, 255), 0.25f).OnComplete(delegate {
                outLine_1.DOColor(new Color32(255, 255, 255, 255), 0.25f).OnComplete(delegate {
                    OffOutLine();
                });
            });
        });
    }

    private void OnDestroy()
    {
        outLine_3.DOKill();
        outLine_2.DOKill();
        outLine_1.DOKill();
    }
}
