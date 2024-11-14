using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using DG.Tweening;
public class BigBallon : BarrialAir
{
    public Sprite eplosionBallon;
    public SpriteRenderer spriteRenderer;
    public List<DataBallon> lsDataBallon;
    public CircleCollider2D circleCollider;
    public GameObject outLine;

 

    public Vector3 positionX; // Target position X
    public Vector3 positionY; // Target position Y
    public float speed = 2f;  // Speed of movement
    private bool movingUp = true;
    public List<PostSmallBall> lsBallons;
    public Color colorBallon;
    public void Explosion()
    {
        spriteRenderer.sprite = eplosionBallon;

        if (!isOff)
        {
            isOff = true;
            GameController.Instance.musicManager.PlayRandomBallon();
            circleCollider.enabled = false;
            tvExplosion.gameObject.SetActive(false);
            GamePlayController.Instance.playerContain.levelData.CountWin(this.transform);
            foreach(var item in lsBallons)
            {
                item.ballon.gameObject.SetActive(true);
                item.ballon.spriteRenderer.sortingOrder = spriteRenderer.sortingOrder + 1;
             
                item.ballon.gameObject.transform.DOMove(item.post.position, 0.1f).OnComplete(delegate {

                    item.ballon.Init();
                    GamePlayController.Instance.playerContain.levelData.lsBallons.Add(item.ballon);
                });
            }
            GamePlayController.Instance.playerContain.effectExplosion.HandleEffectExplosion(this);

            spriteRenderer.DOKill();
            transform.DOKill();
        }
    }


  

    public override void Init()
    {
        tvExplosion.text = "" + countExplosion;
        this.transform.DOMove(postFly, 2).OnComplete(delegate {
            positionX = new Vector3(this.transform.position.x, this.transform.position.y + 0.2f, 1);
            positionY = new Vector3(this.transform.position.x, this.transform.position.y - 0.2f, 1);
            speed = Random.RandomRange(0.1f, 0.5f);
            isInit = true;
            foreach (var item in lsBallons)
            {
                item.ballon.gameObject.transform.position = new Vector3(item.ballon.gameObject.transform.position.x, item.ballon.gameObject.transform.position.y, 1);
                item.ballon.postFly = new Vector3(item.post.position.x, item.post.position.y, 1);
            }
            GamePlayController.Instance.SetPlayingState();
        });
     
     

    }
    public override void TakeDameSpike(int paramDame)
    {
        if (isInit)
        {


            countExplosion -= paramDame;


            if (countExplosion < 1)
            {
                spriteRenderer.sprite = eplosionBallon;
                Explosion();
                spriteRenderer.DOKill();
            }
            tvExplosion.text = "" + countExplosion;

        }
    }
 
    private void FixedUpdate()
    {
        if (!isOff)
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

        if (GamePlayController.Instance.playerContain.spinerBooster.wasUseSniperBooster && !isOff)
        {

            Vector2 circleCenter = circleCollider.transform.position;
            float radius = circleCollider.radius * circleCollider.transform.localScale.x; // Bao gồm scale nếu có

            bool isRaycastPassingThrough = false; // Cờ để theo dõi nếu có raycast nào thỏa mãn điều kiện

            foreach (RaycastPoint raycastPoint in GamePlayController.Instance.playerContain.spinerBooster.lsRaycastPoints)
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

        if (GamePlayController.Instance.playerContain.inputThone.wasDraw && !isOff)
        {
            CheckIfLineCrossesCircle();



        }
        else
        {
            if (!GamePlayController.Instance.playerContain.spinerBooster.wasUseSniperBooster)
            {
                outLine.SetActive(false);
            }
        }
    }

    public void CheckIfLineCrossesCircle()
    {
        LineRenderer lineRenderer = GamePlayController.Instance.playerContain.inputThone.lineRenderer;
        if (lineRenderer == null)
            return; // Ensure there's a LineRenderer available

        // Create an array to store the positions
        Vector3[] positions = new Vector3[lineRenderer.positionCount];

        // Populate the positions array with points from the LineRenderer
        lineRenderer.GetPositions(positions);

        // Loop through all points in the trajectory and check if any pass through the balloon
        foreach (var point in positions)
        {
            float distanceToBallon = Vector2.Distance(transform.position, point);

            // Check if the point is close enough to the balloon
            if (distanceToBallon <= circleCollider.radius)
            {
                // Activate the outline when the trajectory passes through the balloon
                outLine.SetActive(true);
                return;
            }
        }

        // Deactivate the outline when the trajectory no longer passes through the balloon
        outLine.SetActive(false);
    }
    public override void TakeDameSpikeEffect(BallBase paramBall )
    {

    }
    [Button]
    private void HandleRandom()
    {
        var temp = Random.RandomRange(0, lsDataBallon.Count);
        spriteRenderer.sprite = lsDataBallon[temp].ballon;
        eplosionBallon = lsDataBallon[temp].eplosionBallon;
        colorBallon = lsDataBallon[temp].color;
        foreach (var item in vfxExprosion)
        {
            item.startColor = colorBallon;
        }
    }
    public override void HandleColorBallon()
    {
        HandleRandom();
    }
    public override IEnumerator Move()
    {
        isOff = true;
        yield return this.transform.DOMoveY(transform.position.y, 0.5f).WaitForCompletion();
    }
    public override List<BarrialAir> GetBallondsAround()
    {
        var tempList = new List<BarrialAir>();
        Vector2 origin = new Vector2(transform.position.x, transform.position.y);

        // Bán kính của vòng tròn cast
        float radius = 1;

        // Hướng và khoảng cách của CircleCast
        Vector2 direction = Vector2.zero; // Hướng về mọi phía
        float distance = 0.1f; // Khoảng cách nhỏ để kiểm tra xung quanh

        // Thực hiện CircleCast
        RaycastHit2D[] hits = Physics2D.CircleCastAll(origin, radius, direction, distance);

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.GetComponent<BarrialAir>() != null && !hit.collider.gameObject.GetComponent<BarrialAir>().isOff)
                {
                    if (hit.collider.gameObject.GetComponent<BarrialAir>() != this)
                    {
                        tempList.Add(hit.collider.gameObject.GetComponent<BarrialAir>());
                    }
                }
            }
        }
        return tempList;
    }
    public override List<BarrialAir> GetBallondsLeftRight( )
    {
        var tempList = new List<BarrialAir>();
        // Vị trí ban đầu của Raycast (có thể dùng transform.position hoặc một vị trí khác)
        Vector2 origin = new Vector2(transform.position.x, transform.position.y);

        // Khoảng cách tối đa của Raycast
        float distance = 5.0f; // Khoảng cách kiểm tra

        // Bắn ray về phía bên trái
        RaycastHit2D[] hitsLeft = Physics2D.RaycastAll(origin, Vector2.left, distance);
        foreach (RaycastHit2D hit in hitsLeft)
        {
            if (hit.collider.gameObject.GetComponent<BarrialAir>() != null && hit.collider.gameObject.GetComponent<BarrialAir>() != this)
            {
                tempList.Add(hit.collider.gameObject.GetComponent<BarrialAir>());
            }
        }

        // Bắn ray về phía bên phải
        RaycastHit2D[] hitsRight = Physics2D.RaycastAll(origin, Vector2.right, distance);
        foreach (RaycastHit2D hit in hitsRight)
        {
            if (hit.collider.gameObject.GetComponent<BarrialAir>() != null && hit.collider.gameObject.GetComponent<BarrialAir>() != this)
            {
                tempList.Add(hit.collider.gameObject.GetComponent<BarrialAir>());
            }
        }
        return tempList;
    }
    public override void Destroy()
    {
        Explosion();
    }
    public void OnDisable()
    {
        spriteRenderer.DOKill();
        transform.DOKill();
    }
    public void OnDestroy()
    {
        spriteRenderer.DOKill();
        transform.DOKill();
    }
}

[System.Serializable]
public class PostSmallBall
{
    public Ballon ballon;
    public Transform post;
}    
