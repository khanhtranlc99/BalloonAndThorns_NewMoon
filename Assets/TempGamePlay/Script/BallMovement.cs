using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
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
    public BallMovement ballMovementPrefab;
    public int id;
    public bool moveAll;
    public bool moveOut = false;
    public void Init(Vector2 param, int idParam)
    {
        id = idParam;
        wasTouch = false;
        direction = param.normalized;
        speed = 10;
        radius = 0.4f;
     
        activeMove = true;
        moveAll = false;
    }
    public void Init(Vector2 param, int idParam, bool test)
    {
        id = idParam;
        wasTouch = false;
        direction = param.normalized;
        speed = 10;
        radius = 0.4f;

        activeMove = true;
        moveAll = false;
        moveOut = true;
        this.gameObject.GetComponent<SpriteRenderer>().sortingOrder = -1;
    }


    void FixedUpdate()
    {
        if (activeMove && !moveOut)
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

              

                        if (hit.collider.gameObject.GetComponent<BarrialAir>() != null)
                        {
                            hit.collider.gameObject.GetComponent<BarrialAir>().TakeDameSpike();
                            hit.collider.gameObject.GetComponent<BarrialAir>().TakeDameSpikeEffect(this, null);
                        }
                    if (hit.collider.gameObject.tag == "Broad")
                    {
                        SimplePool2.Despawn(this.gameObject);
                    }    
                        if (!moveAll)
                    {
                        var temp = SimplePool2.Spawn(vfxTouch);
                        temp.transform.position = hit.point;
                    }

                }
                    else if (hit.collider != null && hit.collider.isTrigger)
                    {
                        if (hit.collider.gameObject.GetComponent<BarrialAir>() != null)
                        {
                            hit.collider.gameObject.GetComponent<BarrialAir>().TakeDameSpike();
                            hit.collider.gameObject.GetComponent<BarrialAir>().TakeDameSpikeEffect(this, null);
                    }

                    if (hit.collider.gameObject.GetComponent<RockWall>() != null && hit.collider.gameObject.GetComponent<RockWall>().isMoveAll == true)
                    {

                        moveAll = true;
                        var temp1 = SimplePool2.Spawn(ballMovementPrefab);
                        temp1.transform.position = this.transform.position;
                        //GamePlayController.Instance.playerContain.levelData.inputThone.lsBallMovement.Add(temp1);
                        temp1.Init(direction,0, true);
                        SimplePool2.Despawn(this.gameObject);
                    }
                }
               
          
            }

          
            // Di chuyển quả bóng
            transform.position = currentPosition + direction * speed * Time.fixedDeltaTime;

          
        }

        if(moveOut)
        {
            Vector2 currentPosition = transform.position;
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

    public void HandleBoosterX2()
    {
        
        // Sử dụng hướng của trục x của đối tượng làm initialDirection
        Vector3 initialDirection = transform.right;

        // Spawn và khởi tạo temp1 với hướng 45 độ
        var temp1 = SimplePool2.Spawn(ballMovementPrefab);
        temp1.transform.position = this.transform.position;

        // Xoay hướng ban đầu 45 độ quanh trục Z
        Vector3 direction1 = Quaternion.Euler(0, 0, 45) * initialDirection;
        //GamePlayController.Instance.playerContain.levelData.inputThone.lsBallMovement.Add(temp1);
        temp1.Init(direction1, GamePlayController.Instance.playerContain.levelData.inputThone.lsBallMovement.Count);

        // Spawn và khởi tạo temp2 với hướng -45 độ
        var temp2 = SimplePool2.Spawn(ballMovementPrefab);
        temp2.transform.position = this.transform.position;

        // Xoay hướng ban đầu -45 độ quanh trục Z
        Vector3 direction2 = Quaternion.Euler(0, 0, -45) * initialDirection;
        //GamePlayController.Instance.playerContain.levelData.inputThone.lsBallMovement.Add(temp2);
        temp2.Init(direction2, GamePlayController.Instance.playerContain.levelData.inputThone.lsBallMovement.Count);




        SimplePool2.Despawn(this.gameObject);
    }
    
}
