using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class Ballon : BarrialAir
{
    public Sprite eplosionBallon;
    public SpriteRenderer spriteRenderer;
    public List<DataBallon> lsDataBallon;
    public CircleCollider2D circleCollider;
    public GameObject outLine;

    public bool isOff;

    public Vector3 positionX ; // Target position X
    public Vector3 positionY; // Target position Y
    public float speed = 2f;  // Speed of movement
    private bool movingUp = true;
    public bool isInit;

    public virtual void Explosion()
    {
        spriteRenderer.sprite = eplosionBallon;
 
        if (!isOff)
        {
            isOff = true;
            GameController.Instance.musicManager.PlayRandomBallon();
            GamePlayController.Instance.gameScene.HandleSubtrackBallon();
            GamePlayController.Instance.playerContain.levelData.CountWin(this.transform);
            GamePlayController.Instance.HandlSpawnItemInGameBallon(GameController.Instance.dataContain.giftDatabase.GetIconItem(GiftType.Coin), this.transform.position);
       
        }
    }

    public override void Init()
    {
        positionX = new Vector3(this.transform.position.x, this.transform.position.y + 0.2f, 1);
        positionY = new Vector3(this.transform.position.x, this.transform.position.y - 0.2f, 1);
        speed = Random.RandomRange(0.1f, 0.5f);
        isInit = true;
    }
    public override void TakeDameSpike()
    {
        if(isInit)
        {
            Explosion();
        }    
    }
 
    private void FixedUpdate()
    {
        if(!isOff)
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
        if (GamePlayController.Instance.playerContain.inputThone.wasDraw)
        {
            Vector2 circleCenter = circleCollider.transform.position;
            float radius = circleCollider.radius * circleCollider.transform.localScale.x; // Bao gồm scale nếu có

            bool isRaycastPassingThrough = false; // Cờ để theo dõi nếu có raycast nào thỏa mãn điều kiện

            foreach (RaycastPoint raycastPoint in GamePlayController.Instance.playerContain.inputThone.lsRaycastPoints)
            {
                Vector2 startPoint = raycastPoint.startPoint;
                Vector2 endPoint = raycastPoint.endPoint;

                // Vector chỉ phương của đoạn thẳng từ startPoint đến endPoint
                Vector2 direction = (endPoint - startPoint).normalized;

                // Tính vector từ điểm đầu đến tâm hình tròn
                Vector2 startToCenter = circleCenter - startPoint;

                // Chiếu vector từ startPoint đến center lên direction để tìm điểm gần nhất trên đoạn thẳng
                float projectionLength = Vector2.Dot(startToCenter, direction);
                Vector2 closestPoint;

                if (projectionLength < 0)
                {
                    // Điểm gần nhất là startPoint
                    closestPoint = startPoint;
                }
                else if (projectionLength > Vector2.Distance(startPoint, endPoint))
                {
                    // Điểm gần nhất là endPoint
                    closestPoint = endPoint;
                }
                else
                {
                    // Điểm gần nhất nằm trên đoạn thẳng
                    closestPoint = startPoint + direction * projectionLength;
                }

                // Tính khoảng cách từ điểm gần nhất đến tâm hình tròn
                float distanceToCircle = Vector2.Distance(closestPoint, circleCenter);

                // Kiểm tra nếu khoảng cách này nhỏ hơn hoặc bằng bán kính hình tròn
                if (distanceToCircle <= radius)
                {
                
                    isRaycastPassingThrough = true; // Cập nhật cờ nếu có raycast thỏa mãn
                    break; // Có thể dừng vòng lặp nếu tìm thấy một raycast thỏa mãn
                }
            }

            // Cập nhật màu của spriteRenderer dựa trên giá trị của cờ
            if (isRaycastPassingThrough && !isOff)
            {
                outLine.SetActive(true);
            }
            else
            {
                outLine.SetActive(false);
            }
        }
        else
        {
            outLine.SetActive(false);
        }
    }

    [Button]
    private void HandleRandom()
    {
        var temp = Random.RandomRange(0, lsDataBallon.Count);
        spriteRenderer.sprite = lsDataBallon[temp].ballon;
        eplosionBallon = lsDataBallon[temp].eplosionBallon;
    }

    public override void TakeDameSpikeEffect(BallMovement paramBall)
    {
    
    }
}

[System.Serializable]
public class DataBallon
{
    public Sprite ballon;
    public Sprite eplosionBallon;

}
