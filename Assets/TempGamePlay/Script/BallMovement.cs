using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    private float speed = 5f;
    private float radius = 0.4f;
    public LayerMask wallLayer;
 
    public Vector2 direction;
    public bool activeMove;
    public HitVfx vfxTouch;
    public bool wasTouch;
    public AudioSource audioSource;
    public void Init(Vector2 param)
    {
        wasTouch = false;
         direction = param;
        speed = 20;
        radius = 0.4f;
     
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
                    audioSource.Play();
                    // Tính toán hướng phản chiếu bằng cách sử dụng pháp tuyến của bề mặt va chạm
                    direction = Vector2.Reflect(direction, hit.normal);

                    // Di chuyển bóng đến ngay ngoài rìa của BoxCollider2D
                    currentPosition = hit.point + hit.normal * radius;

                    var temp = SimplePool2.Spawn(vfxTouch);
                    temp.transform.position = hit.point;

                    if (hit.collider.gameObject.GetComponent<BarrialAir>() != null)
                    {
                        hit.collider.gameObject.GetComponent<BarrialAir>().TakeDameSpike();
                    }
                    if (hit.collider.gameObject.tag == "Broad")
                    {
                        //if(wasTouch)
                        //{
                            SimplePool2.Despawn(this.gameObject);
                        //}            
                    }
                    //else
                    //{
                    //    wasTouch = true;
                    //}    
                }
                else if (hit.collider != null && hit.collider.isTrigger)
                {
                    if (hit.collider.gameObject.GetComponent<BarrialAir>() != null)
                    {
                        hit.collider.gameObject.GetComponent<BarrialAir>().TakeDameSpike();
                    }
                }
            }

            // Di chuyển quả bóng
            transform.position = currentPosition + direction * speed * Time.fixedDeltaTime;
        }
        this.transform.localEulerAngles -= new Vector3(0, 0, 5);
    }
    private void OnDisable()
    {
        try
        {
            GamePlayController.Instance.playerContain.levelData.HandleCheckLose();
        }    
      catch
        {

        }
    }

}
