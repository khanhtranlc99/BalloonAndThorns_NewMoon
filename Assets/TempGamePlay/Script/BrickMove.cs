using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;
public class BrickMove : MonoBehaviour
{
    public Vector3 postFirst;
    public Vector3 postSecond;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        Move();
    }
    public void Move()
    {
        this.transform.DOMove(postFirst, speed).OnComplete(delegate {

            this.transform.DOMove(postSecond, speed).OnComplete(delegate {

                Move();

            });

        });
    }    
    [Button]
    public void MoveFirst()
    {
        this.transform.position = postFirst;
        Debug.LogError("MoveFirst");
    }
    [Button]
    public void MoveSecond()
    {
        this.transform.position = postSecond;
   
    }
 
}
