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

    public ThoneDemo postFireSpike;
    public BallController ballMovement;

    RaycastHit2D hit;
    Vector2 initialDirection;

    public LineRenderer lineRenderer;
    public LineRenderer lineRenderer_Demo;
    public LayerMask hitLayers;
    public List<GameObject> lsThoneDemos;
    public Material lineMaterial;
    public int numOfReflect;
    public List<BallController> lsBallMovement;
    public List<RaycastPoint> lsRaycastPoints;
    public bool wasDraw;

    public Vector3 firstPost;
    public Vector3 secondPost;


    public Transform leftPost;
    public Transform rightPost;
    public Transform upPost;
    public Transform botPost;
    public Color lineColor = Color.black;
    public bool AllDestoy
    {
        get
        {
            if (lsBallMovement.Count <= 0)
            {
                return true;
            }
            foreach (var item in lsBallMovement)
            {
                if (item.gameObject.activeSelf)
                {
                    return false;
                }
            }
            return true;
        }
    }

    public bool HasBallInGame
    {
        get
        {
            foreach (var item in lsBallMovement)
            {
                if (item.gameObject.activeSelf == true)
                {
                    return true;
                }
            }
            return false;
        }

    }

    public SpriteRenderer outlineLimit;
    public GameObject objText;


    public int numberOfPoints = 50; // Number of points to draw the trajectory
    public float timeBetweenPoints = 0.1f; // Time interval between points
    public LayerMask collisionMask;
    public Material blackMaterial;
    private bool CanShoot
    {
        get
        {
 
            if (GamePlayController.Instance.gameScene.IsMouseClickingOnImage)
            {

                return false;
            }
            if (GamePlayController.Instance.playerContain.levelData.limitTouch > 0 && !GamePlayController.Instance.playerContain.moveSightingPointBooster.wasUseMoveSightingPointBooster)
            {

                return true;
            }
      
            return false;
        }    
    }
 
 


public void Init()
    {
        // Thiết lập LineRenderer
        wasDraw = false;
        lineRenderer.positionCount = 0; // Khởi tạo với không điểm
        lineRenderer.startWidth = 0.15f;
        lineRenderer.endWidth = 0.15f;
        lineMaterial.SetFloat("width", 0.75f);
        lineMaterial.SetFloat("heigth", 0.1f);
       

        lsBallMovement = new List<BallController>();
        SimplePool2.Preload(ballMovement.gameObject, 5);
        if (RemoteConfigController.GetFloatConfig(FirebaseConfig.ID_BACK_GROUND, 1) == 2)
        {
            lineRenderer.SetColors(Color.black, Color.black);
            lineRenderer_Demo.material = blackMaterial;
        }
        firstPost = postFireSpike.transform.position;
        collisionMask = LayerMask.GetMask("Default");

    }
    public void SetupFirstPost()
    {
        firstPost = postFireSpike.transform.position;
    }    
   

    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
         
            if (CanShoot)
            {
                outlineLimit.gameObject.SetActive(true);
                wasDraw = true;
            }
            else
            {
                wasDraw = false;
            }    
            objText.gameObject.SetActive(true);

        }
        if (wasDraw)
        {
            if (Input.GetMouseButton(0))
            {
                secondPost = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                if (secondPost.x < leftPost.position.x)
                {
                    secondPost = new Vector3(leftPost.position.x, secondPost.y);
                }
                if (secondPost.x > rightPost.position.x)
                {
                    secondPost = new Vector3(rightPost.position.x, secondPost.y);
                }

                if (secondPost.y < botPost.position.y)
                {
                    secondPost = new Vector3(secondPost.x, botPost.position.y);
                }
                if (secondPost.y > upPost.position.y)
                {
                    secondPost = new Vector3(secondPost.x, upPost.position.y);
                }


               

                postFireSpike.transform.position = new Vector3(secondPost.x, secondPost.y, 0);
                objText.transform.position = new Vector3(secondPost.x + 0.5f, secondPost.y + 0.5f, 0);

                initialDirection = (firstPost - postFireSpike.transform.position).normalized * 1.5f ;
                DrawTrajectory(ballMovement.rigidbody2D, postFireSpike.transform.position, initialDirection);
                lineRenderer_Demo.positionCount = 2;
                lineRenderer_Demo.SetPosition(0, new Vector3(firstPost.x,firstPost.y,-10));
                lineRenderer_Demo.SetPosition(1, new Vector3(postFireSpike.transform.position.x, postFireSpike.transform.position.y, -10));

            }
            if (Input.GetMouseButtonUp(0))
            {



                GamePlayController.Instance.TutCloneBallsBooster.StartTut();
                wasDraw = false;

                foreach (var item in lsThoneDemos)
                {
                    item.gameObject.SetActive(false);
                }
               
          
                objText.transform.position = new Vector3(firstPost.x + 0.5f, firstPost.y + 0.5f, 0);


             
                var temp = SimplePool2.Spawn(ballMovement);

                temp.transform.position = postFireSpike.transform.position;
                temp.AddForceBall(initialDirection);
           
                lineRenderer.positionCount = 0;

                lsBallMovement.Add(temp);

                GamePlayController.Instance.playerContain.levelData.HandleSubtrack();

                StartCoroutine(StartAgain());
                outlineLimit.gameObject.SetActive(false);
                objText.gameObject.SetActive(false);
                postFireSpike.transform.localScale = Vector3.zero;
                postFireSpike.transform.position = firstPost;
                lineRenderer_Demo.positionCount = 0;
            }

        }





    }
    int tempList;
    public float distanceStartEnd;
    void DrawTrajectory(Rigidbody2D rb, Vector2 start, Vector2 initialForce)
    {
        Vector2 currentPosition = start;
        Vector2 initialVelocity = initialForce / rb.mass;

        float gravity = Physics2D.gravity.y * rb.gravityScale;
        float timeStep = timeBetweenPoints;

        lineRenderer.positionCount = numberOfPoints;

        for (int i = 0; i < numberOfPoints; i++)
        {
            float t = i * timeStep;
            Vector2 displacement = initialVelocity * t + 0.5f * new Vector2(0, gravity) * t * t;
            Vector2 newPoint = currentPosition + displacement;

            if (newPoint.y < -10)
            {
                lineRenderer.positionCount = i;
                break;
            }

            // Set the position of each point in the LineRenderer
 
            lineRenderer.SetPosition(i, newPoint);


 
            if (i > 0)
            {
              
                //Vector2 direction = (lineRenderer.GetPosition(i) - lineRenderer.GetPosition(i - 1)).normalized;
                Vector2 direction = (lineRenderer.GetPosition(i) - lineRenderer.GetPosition(i - 1)).normalized;
                float distance = Vector2.Distance(lineRenderer.GetPosition(i), lineRenderer.GetPosition(i - 1));

                // Perform a raycast from the previous point to the current point
                RaycastHit2D hit = Physics2D.CircleCast(lineRenderer.GetPosition(i - 1), 0.5f, direction, distance, collisionMask);

                if (hit.collider != null)
                {
                    // Calculate the reflection direction
                    Vector2 reflectDirection = Vector2.Reflect(direction, hit.normal);

                    // Update the position to the collision point
                    Vector2 collisionPoint = hit.point + hit.normal * 0.5f;
                    lineRenderer.SetPosition(i, collisionPoint);
                    lineRenderer.positionCount = i + 1;

                
                    // Start drawing the reflected trajectory

                    break; // Exit the loop after handling the first collision
                }

                Debug.DrawRay(lineRenderer.GetPosition(i - 1), direction * distance, Color.green, 0.1f);
            }
        }
        distanceStartEnd = Vector3.Distance(lineRenderer.GetPosition(0), lineRenderer.GetPosition(lineRenderer.positionCount - 1));
       
    }

    void DrawReflectedTrajectory(Rigidbody2D rb, Vector2 start, Vector2 reflectDirection)
    {
        Vector2 currentPosition = start;
        Vector2 initialVelocity = reflectDirection * 10f; // Adjust the speed after reflection if necessary

        float gravity = Physics2D.gravity.y * rb.gravityScale;
        float timeStep = timeBetweenPoints;

        int reflectedPoints = numberOfPoints / 2; // Limit the number of points for reflected trajectory
        lineRenderer.positionCount += reflectedPoints; // Increase the lineRenderer point count for the reflected trajectory

        for (int i = 0; i < reflectedPoints; i++)
        {
            float t = i * timeStep;
            Vector2 displacement = initialVelocity * t + 0.5f * new Vector2(0, gravity) * t * t;
            Vector2 newPoint = currentPosition + displacement;

            if (newPoint.y < -10)
            {
                lineRenderer.positionCount -= (reflectedPoints - i); // Adjust the position count to stop at the ground
                break;
            }

            // Set the position of each reflected point in the LineRenderer
            lineRenderer.SetPosition(lineRenderer.positionCount - reflectedPoints + i, newPoint);
        }
    }
    private IEnumerator StartAgain()
    {
        yield return new WaitForSeconds(1);
       
        postFireSpike.transform.DOScale(new Vector3(1,1,1),1);
        objText.gameObject.SetActive(true);

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

    public void StopActiveMove()
    {
        foreach(var item in lsBallMovement)
        {
            if(item != null)
            {
                item.gameObject.SetActive(false);
            }    
        }    
    }    
}
