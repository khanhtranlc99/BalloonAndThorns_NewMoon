using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;
public class BallMovement : BallBase
{
    private float speed = 5f;
    private float radius = 0.4f;
    public LayerMask wallLayer;

    public Vector2 direction;

    public HitVfx vfxTouch;

    public AudioSource audioSource;

    public BallMovement ballMovementPrefab;
    InputThone inputThone;
    public Collider2D tempCollider2D;
    public SpriteRenderer spriteRender;
    public override void Init(Vector2 param)
    {
        endGame = false;
        wasTouch = false;
        direction = param;
        speed = 18 ;
        radius = 0.2f;
        tempCollider2D = null;
        activeMove = true;
        if(inputThone == null)
        {
            inputThone = GamePlayController.Instance.playerContain.inputThone;
        }
        spriteRender.sprite = GamePlayController.Instance.playerContain.inputThone.listBallController.GetBalls;



    }
    float time = 0.005f;
    float timeRefresh;

    void FixedUpdate()
    {
      
        if (activeMove)
        {
          
            // Tính toán vị trí hiện tại của quả bóng
            Vector2 currentPosition = transform.position;

            // Thực hiện CircleCast để kiểm tra va chạm
            RaycastHit2D[] hits = Physics2D.CircleCastAll(currentPosition, radius, direction, speed * Time.fixedDeltaTime , wallLayer);                                
            foreach (var hit in hits)
                {
                    if (hit.collider != null && !hit.collider.isTrigger)
                    {
                        wasTouch = true;
                        audioSource.Play();
                        // Tính toán hướng phản chiếu bằng cách sử dụng pháp tuyến của bề mặt va chạm
                      
                    // Di chuyển bóng đến ngay ngoài rìa của BoxCollider2D
                 
                    if (tempCollider2D == null)
                        {
                        direction = Vector2.Reflect(direction, hit.normal);
                        currentPosition = hit.point + hit.normal * radius;
                        tempCollider2D = hits[0].collider;
                           timeRefresh = time;
                     
                        }
                        var temp = SimplePool2.Spawn(vfxTouch);
                        temp.transform.position = hit.point;

                        if (hit.collider.gameObject.GetComponent<BarrialAir>() != null)
                        {
                            hit.collider.gameObject.GetComponent<BarrialAir>().TakeDameSpike(DamePlusCard.dame);
                            hit.collider.gameObject.GetComponent<BarrialAir>().TakeDameSpikeEffect(this);
                            GamePlayController.Instance.playerContain.effectTouch.HandleEffectExplosion(hit.collider.gameObject.GetComponent<BarrialAir>());
                        }

                    }
                    else if (hit.collider != null && hit.collider.isTrigger)
                    {
                        if (wasTouch)
                        {
                            if (hit.collider.gameObject.tag == "Broad")
                            {
                                activeMove = false;
                                GamePlayController.Instance.playerContain.inputThone.HandleMoveCharetor(this);
                                //  SimplePool2.Despawn(this.gameObject);
                            }
                        }

                    }
                }
    
          
             
          
            // Di chuyển quả bóng
            transform.position = currentPosition + direction * speed * Time.fixedDeltaTime;
        }
       if (tempCollider2D != null)
        {
            timeRefresh -= Time.fixedDeltaTime;
            if (timeRefresh <= 0)
            {
                tempCollider2D = null;
            }

        }
        this.transform.localEulerAngles -= new Vector3(0, 0, 0.2f);
       
    }


    public void HandleBoosterX2()
    {

        // Sử dụng hướng của trục x của đối tượng làm initialDirection
        Vector3 initialDirection = transform.right;

        // Spawn và khởi tạo temp1 với hướng 45 độ
        var temp1 = SimplePool2.Spawn(ballMovementPrefab);
        temp1.transform.position = this.transform.position;


        // Xoay hướng ban đầu 45 độ quanh trục Z
        Vector3 direction1 = Quaternion.Euler(0, 0, 45) * transform.right;

        temp1.Init(direction1 );
        // GamePlayController.Instance.playerContain.inputThone.lsBallMovement.Add(temp1);
        // Spawn và khởi tạo temp2 với hướng -45 độ
        var temp2 = SimplePool2.Spawn(ballMovementPrefab);
        temp2.transform.position = this.transform.position;

        // Xoay hướng ban đầu -45 độ quanh trục Z
        Vector3 direction2 = Quaternion.Euler(0, 0, -90) * transform.up;

        temp2.Init(direction2  );


        //  GamePlayController.Instance.playerContain.inputThone.lsBallMovement.Add(temp2);

        SimplePool2.Despawn(this.gameObject);
    }

    public override IEnumerator Move(Vector2 param)
    {

        yield return this.transform.DOMove(param, 0.5f).WaitForCompletion();
    }
}
