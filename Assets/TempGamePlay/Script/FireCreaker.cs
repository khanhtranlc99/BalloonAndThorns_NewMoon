using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCreaker : MonoBehaviour
{
    public SpriteRenderer spriteRender;
    public bool wasInit;
    public Vector3 vecMove;
    public float speed;
   public void Init(Vector3 vector)
    {
        wasInit = true;
        vecMove = vector;
    }   
    
 

    void Update()
    {
        if(wasInit)
        {

            this.transform.position += vecMove * Time.deltaTime * speed;


 

        }

       
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<BarrialAir>() != null)
        {
            collision.gameObject.GetComponent<BarrialAir>().Destroy();
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Layer_2"))
        {
            SimplePool2.Despawn(this.gameObject);

        }
    }
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
       
    //}
}
