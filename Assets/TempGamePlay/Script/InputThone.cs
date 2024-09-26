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
    public BallMovement ballMovement;
 
    RaycastHit2D hit;
    Vector2 initialDirection;
 
    public LineRenderer lineRenderer;
    public LayerMask hitLayers;
    public List<GameObject> lsThoneDemos;
    public Material lineMaterial;
    public int numOfReflect;
    public List<BallController> lsBallMovement;
    public  List<RaycastPoint> lsRaycastPoints;
    public  bool wasDraw;

    public Vector3 firstPost;
    public Vector3 secondPost;
 

    public Transform leftPost;
    public Transform rightPost;
    public Transform upPost;
    public Transform botPost;

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

    void Start()
    {
        // Thiết lập LineRenderer
        wasDraw = false;
        lineRenderer.positionCount = 0; // Khởi tạo với không điểm
        lineRenderer.startWidth = 0.15f;
        lineRenderer.endWidth = 0.15f;
        lineMaterial.SetFloat("width", 0.75f);
        lineMaterial.SetFloat("heigth", 0.1f);
        if (UseProfile.UnlimitScope)
        {
            numOfReflect = 4;
        }
        else
        {
            numOfReflect = 1;
        }

        lsBallMovement = new List<BallController>();
        SimplePool2.Preload(ballMovement.gameObject, 5);
        if (RemoteConfigController.GetFloatConfig(FirebaseConfig.ID_BACK_GROUND, 1) == 2)
        {
            lineRenderer.SetColors(Color.black, Color.black);
        }
        firstPost = postFireSpike.transform.position;
    }

    void Update()
    {
      
        if (Input.GetMouseButtonDown(0))
        {
            if (GamePlayController.Instance.playerContain.levelData.limitTouch > 0)
            {
                outlineLimit.gameObject.SetActive(true);
                wasDraw = true;
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
                lineRenderer.positionCount = 2;
                lineRenderer.SetPosition(0, new Vector3(firstPost.x, firstPost.y, 0));
                lineRenderer.SetPosition(1, new Vector3(secondPost.x, secondPost.y, 0));
              
                postFireSpike.transform.position = new Vector3(secondPost.x, secondPost.y, 0);
                objText.transform.position = new Vector3(secondPost.x+0.5f, secondPost.y+0.5f, 0);


            }
            if (Input.GetMouseButtonUp(0))
            {



                GamePlayController.Instance.TutCloneBallsBooster.StartTut();
                wasDraw = false;

                foreach (var item in lsThoneDemos)
                {
                    item.gameObject.SetActive(false);
                }
                lineRenderer.positionCount = 0;
                postFireSpike.transform.localScale = Vector3.zero;
                postFireSpike.transform.position = firstPost;
                objText.transform.position = new Vector3(firstPost.x + 0.5f, firstPost.y + 0.5f, 0);
                // Khởi tạo bóng và gán vị trí ban đầu của nó


                //    var temp = SimplePool2.Spawn(ballMovement);
                //    temp.transform.position = postFireSpike.transform.position;
                //// Sử dụng hướng của raycast đầu tiên để khởi tạo bóng
            
                //temp.Init(initialDirection, lsBallMovement.Count);
                initialDirection = firstPost - secondPost;
                Debug.LogError("initialDirection" + initialDirection);
                var temp = SimplePool2.Spawn(GamePlayController.Instance.ballController);
                temp.transform.position = postFireSpike.transform.position;

                temp.AddForceBall(initialDirection);
                lsBallMovement.Add(temp);

                GamePlayController.Instance.playerContain.levelData.HandleSubtrack();
        
                StartCoroutine(StartAgain());
                outlineLimit.gameObject.SetActive(false);
                objText.gameObject.SetActive(false);
            }

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
