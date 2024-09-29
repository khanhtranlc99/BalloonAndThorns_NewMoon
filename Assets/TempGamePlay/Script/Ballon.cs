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
    public Color colorBallon;

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
            foreach (var item in vfxExprosion)
            {
                item.Play();
            }
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
      
        if(GamePlayController.Instance.playerContain.spinerBooster.wasUseSniperBooster && !isOff)
        {

        }    

        if (GamePlayController.Instance.playerContain.inputThone.wasDraw && !isOff)
        {
            CheckIfLineCrossesCircle();
         

      
        }
        else
        {
            outLine.SetActive(false);
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

    [Button]
    private void HandleRandom()
    {
        var temp = Random.RandomRange(0, lsDataBallon.Count);
        spriteRenderer.sprite = lsDataBallon[temp].ballon;
        eplosionBallon = lsDataBallon[temp].eplosionBallon;
        colorBallon = lsDataBallon[temp].color;
        foreach(var item in vfxExprosion)
        {
            item.startColor = colorBallon;
        }    
     
    
    }

    public override void TakeDameSpikeEffect(BallMovement paramBall, BallController ballController)
    {
    
    }

    public override void HandleColorBallon()
    {
        HandleRandom();
    }
}

[System.Serializable]
public class DataBallon
{
    public Sprite ballon;
    public Sprite eplosionBallon;
    public Color color;

}
