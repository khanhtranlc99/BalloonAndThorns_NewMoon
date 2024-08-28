using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class HandTutUI : MonoBehaviour
{
    Vector3 firstPost;
    public Vector2 post;
    public void Init()
    {
        StartCoroutine(SetUp());
    }

    private IEnumerator SetUp()
    {
        yield return new WaitForEndOfFrame();
        firstPost = this.transform.position;
        HandleMove();
    }    

    private void HandleMove()
    {
        this.transform.DOMove(new Vector3(firstPost.x + post.x, firstPost.y + post.y, firstPost.z), 0.5f).OnComplete(delegate {

            this.transform.DOMove(new Vector3(firstPost.x, firstPost.y, firstPost.z), 0.5f).OnComplete(delegate {

                HandleMove();
            });
        });
    }

    private void OnDestroy()
    {
        this.transform.DOKill();
    }
}
