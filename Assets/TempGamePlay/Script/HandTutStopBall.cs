using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class HandTutStopBall : MonoBehaviour
{
    public Transform post;
    public Transform post2;
    private void OnEnable()
    {
        HandleHandTut();
    }

    public void HandleHandTut()
    {
        this.transform.DOMove(post.position,0.4f).OnComplete(delegate {
            this.transform.DOMove(post2.position, 0.4f).OnComplete(delegate {

                HandleHandTut();
            });

        });
    }

    private void OnDisable()
    {
        this.DOKill();
    }

}
