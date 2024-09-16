using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : BarrialAir 
{
    private float speed = 5f;
    private float radius = 0.4f;
    public LayerMask wallLayer;
 
    public Vector2 direction;
    public bool activeMove;
    public HitVfx vfxTouch;
    public bool wasTouch;
    public AudioSource audioSource;
    public SpriteRenderer spriteRenderer;
    public Sprite explosionSprite;

    public bool isStanding;
    public CircleCollider2D collider2D;

    public  void Init(DataBallon param)
    {
        spriteRenderer.sprite = param.ballon;
        explosionSprite  = param.eplosionBallon;
        id = param.id;
        isStanding = false;
    }
    public override void Init()
    {

    }
    public override void TakeDameSpike()
    {
        collider2D.enabled = false;
        spriteRenderer.sprite = explosionSprite;
        activeMove = false;
        isStanding = false;

    }
    public void ShootBallon(Vector2 param)
    {
        wasTouch = false;
         direction = param;
        speed = 20;
        radius = 0.4f;   
        activeMove = true;
    }

    public Vector3 positionX; // Target position X
    public Vector3 positionY; // Target position X
    private bool movingUp = true;
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

                        Destroy(this.gameObject);
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

                        if (hit.collider.gameObject.GetComponent<BarrialAir>().id != this.id)
                        {
                            positionX = new Vector3(this.transform.position.x, this.transform.position.y + 0.2f, 0);
                            positionY = new Vector3(this.transform.position.x, this.transform.position.y - 0.2f, 0);
                            //this.transform.position = hit.transform.position;
                            collider2D.enabled = true;
                            activeMove = false;
                            speed = Random.RandomRange(0.1f, 0.5f);
                            isStanding = true;
                        }
                        else
                        {
                            TakeDameSpike();
                            hit.collider.gameObject.GetComponent<BarrialAir>().TakeDameSpike();
                            Debug.LogError("BarrialAirBarrialAir");
                        }

                    }
                }
            }

            // Di chuyển quả bóng
            transform.position = currentPosition + direction * speed * Time.fixedDeltaTime;
            //this.transform.localEulerAngles -= new Vector3(0, 0, 5);
        }
        if(isStanding)
        {
            if (movingUp)
            {
                transform.position = Vector3.MoveTowards(transform.position, positionX, speed * Time.fixedDeltaTime);
                if (Vector3.Distance(transform.position, positionX) < 0.01f)
                {
                    movingUp = false;
                }
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, positionY, speed * Time.fixedDeltaTime);
                if (Vector3.Distance(transform.position, positionY) < 0.01f)
                {
                    movingUp = true;
                }
            }


        }
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
