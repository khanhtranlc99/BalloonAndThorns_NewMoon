using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using System;
 
[System.Serializable]
public class RaycastPoint
{
    public Vector3 startPoint;
    public Vector3 endPoint;
}

public class InputThone : MonoBehaviour
{
    public TMP_Text tvBall;
    public int numbBall ;
    public ThoneDemo postFireSpike;
    public BallMovement ballMovement;
    RaycastHit2D hit;
    Vector2 initialDirection;
    public LineRenderer lineRenderer;
    public LayerMask hitLayers;
    public List<GameObject> lsThoneDemos;
    public Material lineMaterial;
    public int numOfReflect = 1;
    public List<BallBase> lsBallMovement;
    public List<RaycastPoint> lsRaycastPoints;
    public bool wasDraw;
    public bool lockShoot;
    public BallBase lastPostBall;
    public GameObject mark;
    public int NumShoot;
    public Transform firstPost;
    public DownBallButton downBallButton;
 
    public bool AllBallDrop
    {
        get
        {
            if (lsBallMovement.Count <= 0)
            {
                return true;
            }
            foreach (var item in lsBallMovement)
            {
                if (item.activeMove)
                {
                    return false;
                }
            }
            return true;
        }
    }
    public bool AllBallMoveStartPost
    {
        get
        {
            
            foreach (var item in lsBallMovement)
            {
                if (!item.endGame)
                {
                    return false;
                }
            }
            return true;
        }
    }
    PlayerContain playerContain;
    public void Init(PlayerContain param)
    {
        if (UseProfile.CurrentLevel > 39)
        {
            lineRenderer.SetColors(Color.black, Color.black);
        }
        playerContain = param;

       // postFireSpike.Init();
        // Thiết lập LineRenderer
        lineRenderer.positionCount = 0; // Khởi tạo với không điểm
        lineRenderer.startWidth = 0.15f;
        lineRenderer.endWidth = 0.15f;
        lineMaterial.SetFloat("width", 0.75f);
        lineMaterial.SetFloat("heigth", 0.1f);
     //   numOfReflect = 1;
        lsBallMovement = new List<BallBase>();
 
        lastPostBall = null;
        lockShoot = false;
        tvBall.text = "x" + numbBall;
        NumShoot =  5 + ShootPlusCard.numbShootPlus;
        GamePlayController.Instance.gameScene.tvTarget.text = "" + NumShoot;
    }
    public SpriteRenderer iconCanon;
    public MoveCollider moveCollider;
    public ListBallController listBallController;
    void Update()
    {
        if (lockShoot) return;
        if (GamePlayController.Instance.gameScene.IsMouseClickingOnImage)
        {
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (GamePlayController.Instance.stateGame == StateGame.Playing)
            {
                wasDraw = true;
                GamePlayController.Instance.TutGameplay.NextTut();
            }

        }
        if (wasDraw)
        {
            if (Input.GetMouseButton(0))
            {

                // Lấy vị trí chuột trên màn hình
                Vector3 mousePosition = Input.mousePosition;

                // Chuyển đổi vị trí chuột từ màn hình sang thế giới
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, Camera.main.transform.position.z));
                if (worldPosition.y <= postFireSpike.postShoot.position.y)
                {
                    HandleMouseEndLimit();
                    return;

                }
                // Tạo một Ray bắn từ vị trí của thoneDemo qua vị trí người dùng chạm
                Vector3 direction = (worldPosition - postFireSpike.postShoot.position).normalized;

                // Lưu hướng của raycast để sử dụng khi khởi tạo bóng
                initialDirection = direction;

                // Sử dụng CircleCast thay cho Raycast, với bán kính hình tròn là 0.4f
                Vector3 currentDirection = direction;
                Vector3 currentOrigin = postFireSpike.postShoot.position;

                // Khởi tạo LineRenderer
                lineRenderer.positionCount = 1;
                lineRenderer.SetPosition(0, currentOrigin);

                int lineIndex = 1;

                // Clear the list at the start of the raycasting process
                lsRaycastPoints.Clear();

                // Calculate the direction from the cannon to the mouse position
                Vector2 direction2 = (worldPosition - iconCanon.transform.position).normalized;

                // Calculate the angle in degrees
                float angle = Mathf.Atan2(direction2.y, direction2.x) * Mathf.Rad2Deg;

                // Apply rotation to the cannon (rotating on the Z-axis)
                iconCanon.transform.rotation = Quaternion.Euler(0, 0, angle - 90f);




                if (numOfReflect <= 1)
                {
                    hit = Physics2D.CircleCast(currentOrigin, 0.2f, currentDirection, Mathf.Infinity, hitLayers);

                    // Debug raycast với đầu là hình tròn
                    Debug.DrawRay(currentOrigin, currentDirection * 100f, Color.red, 0.05f);

                    if (hit.collider != null)
                    {
                        // Tọa độ của điểm va chạm
                        Vector2 hitPoint = hit.point;

                        // Tính toán hướng vuông góc với raycast (normal của va chạm)
                        Vector2 normal = hit.normal;

                        // Vẽ một hình tròn tại điểm va chạm để kiểm tra
                        Vector2 circleCastOrigin = hitPoint + normal * 0.2f;
                        lsThoneDemos[0].gameObject.SetActive(true);
                        lsThoneDemos[0].transform.position = circleCastOrigin;

                        // Tăng số điểm của LineRenderer
                        lineRenderer.positionCount = lineIndex + 1;
                        lineRenderer.SetPosition(lineIndex, circleCastOrigin);
                        lineIndex++;

                        // Tạo và lưu thông tin raycast vào lsRaycastPoints
                        RaycastPoint raycastPoint = new RaycastPoint
                        {
                            startPoint = currentOrigin,
                            endPoint = (Vector3)circleCastOrigin
                        };
                        lsRaycastPoints.Add(raycastPoint);

                        // Tính toán hướng phản chiếu từ điểm va chạm
                        Vector2 reflectDirection = Vector2.Reflect(currentDirection, normal);

                        // Vẽ raycast phản chiếu từ điểm va chạm
                        Debug.DrawRay(circleCastOrigin, reflectDirection * 100f, Color.blue, 0.05f);
                        float shortSegmentLength = 2.0f; // Chiều dài của đoạn ngắn, có thể điều chỉnh
                        Vector2 reflectSegmentEnd = circleCastOrigin + reflectDirection.normalized * shortSegmentLength;

                        // Thêm điểm kết thúc của đoạn ngắn vào LineRenderer
                        lineRenderer.positionCount = lineIndex + 1;
                        lineRenderer.SetPosition(lineIndex, reflectSegmentEnd);
                        lineIndex++;

                        // Tạo và lưu thông tin phản chiếu vào lsRaycastPoints
                        raycastPoint = new RaycastPoint
                        {
                            startPoint = circleCastOrigin,
                            endPoint = (Vector3)reflectSegmentEnd
                        };
                        lsRaycastPoints.Add(raycastPoint);
                    }
                }
                else
                {
                    for (int i = 0; i < numOfReflect; i++)
                    {
                        // CircleCast để phát hiện va chạm với các lớp được chỉ định bởi hitLayers
                        hit = Physics2D.CircleCast(currentOrigin, 0.2f, currentDirection, Mathf.Infinity, hitLayers);

                        // Debug raycast với đầu là hình tròn
                        Debug.DrawRay(currentOrigin, currentDirection * 100f, Color.red, 0.05f);

                        if (hit.collider != null)
                        {
                            // Tọa độ của điểm va chạm
                            Vector2 hitPoint = hit.point;

                            // Tính toán hướng vuông góc với raycast (normal của va chạm)
                            Vector2 normal = hit.normal;

                            // Vẽ một hình tròn tại điểm va chạm để kiểm tra
                            Vector2 circleCastOrigin = hitPoint + normal * 0.2f;
                            lsThoneDemos[i].gameObject.SetActive(true);
                            lsThoneDemos[i].transform.position = circleCastOrigin;

                            // Tăng số điểm của LineRenderer
                            lineRenderer.positionCount = lineIndex + 1;
                            lineRenderer.SetPosition(lineIndex, circleCastOrigin);
                            lineIndex++;

                            // Tạo và lưu thông tin raycast vào lsRaycastPoints
                            RaycastPoint raycastPoint = new RaycastPoint
                            {
                                startPoint = currentOrigin,
                                endPoint = (Vector3)circleCastOrigin
                            };
                            lsRaycastPoints.Add(raycastPoint);

                            // Tính toán hướng phản chiếu từ điểm va chạm
                            Vector2 reflectDirection = Vector2.Reflect(currentDirection, normal);

                            // Vẽ raycast phản chiếu từ điểm va chạm
                            Debug.DrawRay(circleCastOrigin, reflectDirection * 100f, Color.blue, 0.05f);

                            // Cập nhật hướng và gốc cho lần phản chiếu tiếp theo
                            currentDirection = reflectDirection;
                            currentOrigin = circleCastOrigin;
                        }
                        else
                        {
                            // Nếu không có va chạm, dừng vòng lặp
                            break;
                        }
                    }
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                lockShoot = true;
                wasDraw = false;
                lastPostBall = null;
                foreach (var item in lsThoneDemos)
                {
                    item.gameObject.SetActive(false);
                }
                lineRenderer.positionCount = 0;
                // Khởi tạo bóng và gán vị trí ban đầu của nó
                var tempInitialDirection = initialDirection.normalized;
                GamePlayController.Instance.gameScene.HandleSubtrackNumShoot();
                StartCoroutine(Shoot(tempInitialDirection));
                downBallButton.gameObject.SetActive(true);
           
              //  postFireSpike.postShoot.gameObject.SetActive(false);
            }
        }


    }
    int tempBall;
    private IEnumerator Shoot(Vector2 param)
    {
        tempBall = numbBall;
        var tempListBall = listBallController.GetListBallBase;
        for (int i = 0; i < tempListBall.Count; i++)
        {

            var temp = SimplePool2.Spawn(tempListBall[i]);
            temp.transform.position =    postFireSpike.postShoot.position;
            lsBallMovement.Add(temp);
        
      
        }
        foreach(var item in lsBallMovement)
        {
            yield return new WaitForSeconds(0.05f);
            tempBall -= 1;
            tvBall.text = "x" + tempBall;
            item.Init(param);
        }    
        //mark.SetActive(false);
    }


    private void HandleMouseEndLimit()
    {
        wasDraw = false;
        foreach (var item in lsThoneDemos)
        {
            item.gameObject.SetActive(false);
        }
        lineRenderer.positionCount = 0;
        Debug.LogError("HandleMouseEndLimit__" + lineRenderer.positionCount);
    }

    public IEnumerator StopActiveMove(Action callBack)
    {
        StopAllCoroutines();
        for  (int i = 0; i < lsBallMovement.Count; i ++)
        {
            lsBallMovement[i].activeMove = false;
        }
        yield return new WaitForSeconds(0.75f);
        List<Coroutine> runningCoroutines = new List<Coroutine>();
        foreach (var item in lsBallMovement)
        {
            runningCoroutines.Add(StartCoroutine(item.Move(postFireSpike.postShoot.position)));
        }
        foreach (var coroutine in runningCoroutines)
        {
            yield return coroutine;
        }
        HandleOffBall();
        callBack?.Invoke();
    }

  

    public void HandleOffBall()
    {
       
            downBallButton.gameObject.SetActive(false);
            foreach (var item in lsBallMovement)
            {
                SimplePool2.Despawn(item.gameObject);
            }
            lsBallMovement.Clear();
     
    }

    public void HandleMoveCharetor(BallBase ball)
    {
        if (lastPostBall == null)
        {
            lastPostBall = ball;

        }
        if (ball.transform.position != lastPostBall.transform.position)
        {
            ball.transform.DOMove(lastPostBall.transform.position, 0.2f);
        }
        if (AllBallDrop)
        {
            postFireSpike.transform.DOMoveX(lastPostBall.transform.position.x, 0.5f).OnComplete(delegate
            {
                for (int i = 0; i < lsBallMovement.Count; i++)
                {
                    int index = i;
                    lsBallMovement[index].transform.DOMove(postFireSpike.postShoot.gameObject.transform.position, 0.35f).OnComplete(delegate
                    {
                       
                        if (index == lsBallMovement.Count - 1)
                        {

                            //  postFireSpike.postShoot.gameObject.SetActive(true);
                            HandleSetUp();
                          
                        }

                    });
                }
            });

        }
    }

    public void HandleSetUp()
    {
     
 
        downBallButton.gameObject.SetActive(false);
  
        if (playerContain.levelData.isMove)
        {
            StartCoroutine(playerContain.levelData.HandleActionMove(SetUp));
        }    
         else
        {
            SetUp();
        }    

     
      

        void SetUp()
        {
            HandleOffBall();

     
            if (NumShoot <= 0 && !playerContain.levelData.AllExplosion)
            {
                if (GamePlayController.Instance.stateGame == StateGame.Playing)
                {

                    GamePlayController.Instance.stateGame = StateGame.Lose;
                    LoseBox.Setup().Show();
                }
             
            }
            else
            {
                tvBall.text = "x" + numbBall;
                postFireSpike.postShoot.gameObject.SetActive(true);
                lockShoot = false;
            }
        
          //  mark.SetActive(true);
        }


    }

}
