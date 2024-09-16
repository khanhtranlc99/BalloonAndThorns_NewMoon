using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
[System.Serializable]
public class RaycastPoint
{
    public Vector3 startPoint;
    public Vector3 endPoint;
}

public class InputThone : MonoBehaviour
{

    public Transform transformFirstBall;
    public Transform transformSecondBall;
    public BallMovement firstBalls;
    public BallMovement secondBalls;

    RaycastHit2D hit;
    Vector2 initialDirection;
    public LineRenderer lineRenderer;
    public LayerMask hitLayers;
    public List<GameObject> lsThoneDemos;
    public Material lineMaterial;
    public int numOfReflect;

    public BallMovement ballMovement;
    public List<BallMovement> lsBallMovement;
    public  List<RaycastPoint> lsRaycastPoints;
    public  bool wasDraw;

    public LevelData levelData;

    public bool AllDestoy
    {
        get
        {
            if(lsBallMovement.Count <= 0)
            {
                return true;
            }
            foreach(var item in lsBallMovement)
            {
                if(item.gameObject.activeSelf)
                {
                    return false;
                }    
            }
            return true;
        }    
    }    
    public void Init(LevelData param)
    {
        levelData = param;
        lineRenderer.positionCount = 0; // Khởi tạo với không điểm
        lineRenderer.startWidth = 0.15f;
        lineRenderer.endWidth = 0.15f;
        lineMaterial.SetFloat("width", 0.75f);
        lineMaterial.SetFloat("heigth", 0.1f);
        numOfReflect = 1;
        lsBallMovement = new List<BallMovement>();
        SimplePool2.Preload(ballMovement.gameObject, 20);
        HandleSetUpBall();
    }

    List<DataBallon> lsIdRemain;
    int random;

    DataBallon GetDataBallon
    {

        get
        {
            lsIdRemain = new List<DataBallon>();
            foreach (var item in levelData.lsDataBallon)
            {
                lsIdRemain.Add(item);
            }
            random = Random.RandomRange(0, lsIdRemain.Count);
            return lsIdRemain[random];
        }    
    }    


    private void HandleSetUpBall()
    {

      
        if (firstBalls == null && secondBalls == null)
        {
    

            var temp = Instantiate(ballMovement);
            temp.transform.position = transformFirstBall.position;
            temp.Init(GetDataBallon);
            firstBalls = temp;

            var temp2 = Instantiate(ballMovement);
            temp2.transform.position = transformSecondBall.position;
            temp2.Init(GetDataBallon);
            secondBalls = temp2;
            wasDraw = true;
        }    
        else
        {
            wasDraw = false;
            secondBalls.transform.DOMove(transformFirstBall.position,0.5f).OnComplete(delegate {

                wasDraw = true;
                firstBalls = secondBalls;
                secondBalls = null;
                var temp2 = Instantiate(ballMovement);
                temp2.transform.position = transformSecondBall.position;
                temp2.Init(GetDataBallon);
                secondBalls = temp2;
            });

        }    



    }    
  
    void Update()
    {
        //if(Input.GetMouseButtonDown(0))
        //{
        //    if(GamePlayController.Instance.playerContain.levelData.limitTouch > 0 && GamePlayController.Instance.stateGame == StateGame.Playing)
        //    {
        //         = true;
        //    }    
        
        //}    
        if (wasDraw)
        {
            if (Input.GetMouseButton(0))
            {
                
                // Lấy vị trí chuột trên màn hình
                Vector3 mousePosition = Input.mousePosition;

                // Chuyển đổi vị trí chuột từ màn hình sang thế giới
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, Camera.main.transform.position.z));
                if(worldPosition.y <= transformFirstBall.transform.position.y)
                {
                    HandleMouseEndLimit();
                    return;
                    
                }    
                // Tạo một Ray bắn từ vị trí của thoneDemo qua vị trí người dùng chạm
                Vector3 direction = (worldPosition - transformFirstBall.transform.position).normalized;

                // Lưu hướng của raycast để sử dụng khi khởi tạo bóng
                initialDirection = direction;

                // Sử dụng CircleCast thay cho Raycast, với bán kính hình tròn là 0.4f
                Vector3 currentDirection = direction;
                Vector3 currentOrigin = transformFirstBall.transform.position;

                // Khởi tạo LineRenderer
                lineRenderer.positionCount = 1;
                lineRenderer.SetPosition(0, currentOrigin);

                int lineIndex = 1;

                // Clear the list at the start of the raycasting process
                lsRaycastPoints.Clear();

                if (numOfReflect <= 1)
                {
                    hit = Physics2D.CircleCast(currentOrigin, 0.4f, currentDirection, Mathf.Infinity, hitLayers);

                    // Debug raycast với đầu là hình tròn
                    Debug.DrawRay(currentOrigin, currentDirection * 100f, Color.red, 0.05f);

                    if (hit.collider != null  )
                    {
                        // Tọa độ của điểm va chạm
                        Vector2 hitPoint = hit.point;

                        // Tính toán hướng vuông góc với raycast (normal của va chạm)
                        Vector2 normal = hit.normal;

                        // Vẽ một hình tròn tại điểm va chạm để kiểm tra
                        Vector2 circleCastOrigin = hitPoint + normal * 0.4f;
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
                        hit = Physics2D.CircleCast(currentOrigin, 0.4f, currentDirection, Mathf.Infinity, hitLayers);

                        // Debug raycast với đầu là hình tròn
                        Debug.DrawRay(currentOrigin, currentDirection * 100f, Color.red, 0.05f);

                        if (hit.collider != null)
                        {
                            // Tọa độ của điểm va chạm
                            Vector2 hitPoint = hit.point;

                            // Tính toán hướng vuông góc với raycast (normal của va chạm)
                            Vector2 normal = hit.normal;

                            // Vẽ một hình tròn tại điểm va chạm để kiểm tra
                            Vector2 circleCastOrigin = hitPoint + normal * 0.4f;
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
                wasDraw = false;
            
                foreach (var item in lsThoneDemos)
                {
                    item.gameObject.SetActive(false);
                }
                lineRenderer.positionCount = 0;
                // Khởi tạo bóng và gán vị trí ban đầu của nó
              
                    //var temp = SimplePool2.Spawn(ballMovement);
                    //temp.transform.position = firstBalls.transform.position;
                    //// Sử dụng hướng của raycast đầu tiên để khởi tạo bóng
                    //temp.ShootBallon(initialDirection);
                    //lsBallMovement.Add(temp);
                    //GamePlayController.Instance.playerContain.levelData.HandleSubtrack();
              if(firstBalls != null)
                {
                    firstBalls.ShootBallon(initialDirection);
                    firstBalls = null;
                    HandleSetUpBall();
               
                }    
          
            }
        }
         
   
    }


    private void HandleMouseEndLimit()
    {
       
        foreach (var item in lsThoneDemos)
        {
            item.gameObject.SetActive(false);
        }
        lineRenderer.positionCount = 0;
        Debug.LogError("HandleMouseEndLimit__" + lineRenderer.positionCount);
    }    

    public void StopActiveMove()
    {
        foreach(var item in lsBallMovement)
        {
            if(item != null)
            {
                item.activeMove = false;
            }    
        }    
    }    
}
