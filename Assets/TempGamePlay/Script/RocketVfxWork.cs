using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RocketVfxWork : MonoBehaviour
{
     public void Init(BarrialAir param)
    {
        Vector3 direction = (param.transform.position - transform.position).normalized;

        // Xoay đối tượng theo hướng của param
        transform.up = direction;
        transform.DOMove(param.transform.position, 0.35f).OnComplete(delegate {

            param.TakeDameSpike(IncreaseRocketCard.dame);
            SimplePool2.Despawn( gameObject);


            });
    }
}
