using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketCardItem : MonoBehaviour
{
    private float speed = 5f;
    private float radius = 0.4f;
    public LayerMask wallLayer;

    public Vector2 direction;
    public bool activeMove;
    //public HitVfx vfxTouch;
    public bool wasTouch;
    public AudioSource audioSource;
    public bool endGame;
 
    public void Init(Vector2 param)
    {
        endGame = false;
        wasTouch = false;
        direction = param;
        speed = 200;
        radius = 0.2f;

        activeMove = true;

    }


    void FixedUpdate()
    {
        if (activeMove)
        {
            // Tính toán vị trí hiện tại của quả bóng
            Vector2 currentPosition = transform.position;

            // Thực hiện CircleCast để kiểm tra va chạm
            RaycastHit2D[] hits = Physics2D.CircleCastAll(currentPosition, radius, direction, speed * Time.fixedDeltaTime, wallLayer);

            foreach (var hit in hits)
            {
                if (hit.collider != null && !hit.collider.isTrigger)
                {
                    wasTouch = true;
                    audioSource.Play();
                    // Tính toán hướng phản chiếu bằng cách sử dụng pháp tuyến của bề mặt va chạm
                   // direction = Vector2.Reflect(direction, hit.normal);
                    // Di chuyển bóng đến ngay ngoài rìa của BoxCollider2D
                  //  currentPosition = hit.point + hit.normal * radius;
                    //var temp = SimplePool2.Spawn(vfxTouch);
                    //temp.transform.position = hit.point;

                    if (hit.collider.gameObject.GetComponent<BarrialAir>() != null)
                    {
                        hit.collider.gameObject.GetComponent<BarrialAir>().Destroy();
                     
                    }

                }
                else if (hit.collider != null && hit.collider.isTrigger)
                {
                    if (wasTouch)
                    {
                        if (hit.collider.gameObject.tag == "Broad")
                        {
                        
                       
                            //  SimplePool2.Despawn(this.gameObject);
                        }
                    }

                }
            }

            // Di chuyển quả bóng
            transform.position = currentPosition + direction * speed * Time.fixedDeltaTime;
        }
      
    }

 

}
