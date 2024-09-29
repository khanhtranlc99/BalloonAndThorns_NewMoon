using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SpinerBooster : MonoBehaviour
{
    public Button sniper_Btn;
    public Text tvNum;
    public GameObject objNum;
    public GameObject parentTvCoin;
    public GameObject lockIcon;
    public GameObject unLockIcon;
    public bool wasUseSniperBooster = false;
 


    PlayerContain playerContain;
    public void Init(PlayerContain param)
    {
     
        playerContain = param;
        wasUseSniperBooster = false;
        if (UseProfile.CurrentLevel >= 3)//5
        {

            //unLockIcon.gameObject.SetActive(true);
            lockIcon.gameObject.SetActive(false);
            HandleUnlock();
            //Debug.LogError("HandleUnlock");

        }
        else
        {
            //unLockIcon.gameObject.SetActive(false);
            lockIcon.gameObject.SetActive(true);
            objNum.SetActive(false);
            HandleLock();
            //Debug.LogError("HandleLock");
        }

 
        void HandleUnlock()
        {

            sniper_Btn.onClick.AddListener(HandleSniper_Booster);
            if (UseProfile.SniperBooster > 0)
            {
                objNum.SetActive(true);
                tvNum.text = UseProfile.SniperBooster.ToString();
                parentTvCoin.SetActive(false);
            }
            else
            {
                objNum.SetActive(false);
                tvNum.gameObject.SetActive(false);
                parentTvCoin.SetActive(true);
            }
            EventDispatcher.EventDispatcher.Instance.RegisterListener(EventID.SNIPER_BOOSTER, ChangeText);
        }
        void HandleLock()
        {


            sniper_Btn.onClick.AddListener(HandleLockBtn);
        }
    }

    public void HandleLockBtn()
    {
        GameController.Instance.musicManager.PlayClickSound();
        GameController.Instance.moneyEffectController.SpawnEffectText_FlyUp
                              (
                              sniper_Btn.transform.position,
                              "Unlock at level 3",
                              Color.white,
                              isSpawnItemPlayer: true
                              );
    }





    public void HandleSniper_Booster()
    {
        GameController.Instance.musicManager.PlayClickSound();
        if (UseProfile.SniperBooster >= 1)
        {
     
            UseProfile.SniperBooster -= 1;  
            sniper_Btn.interactable = false;
            GamePlayController.Instance.TutSpinerBooster.NextTut();
            GamePlayController.Instance.playerContain.levelData.inputThone.enabled = false;
            postFireSpike = GamePlayController.Instance.playerContain.levelData.inputThone.postFireSpike;
            wasUseSniperBooster = true;
            postFireSpike.spriteRenderer.sprite = fireCreaker;
            postFireSpike.transform.localScale = new Vector3(2, 2, 2);
            postFireSpike.transform.localEulerAngles = new Vector3(0, 0, 0);
            if (RemoteConfigController.GetFloatConfig(FirebaseConfig.ID_BACK_GROUND, 1) == 2)
            {
                lineRenderer.SetColors(Color.black, Color.black);
               
            }
        }
        else
        {
            SuggetBox.Setup(GiftType.SNIPER_BOOSTER).Show();

        }



    }

  



    public void ChangeText(object param)
    {
        tvNum.text = UseProfile.SniperBooster.ToString();
        if (UseProfile.SniperBooster > 0)
        {
            objNum.SetActive(true);
            tvNum.gameObject.SetActive(true);
            tvNum.text = UseProfile.SniperBooster.ToString();
            parentTvCoin.SetActive(false);
        }
        else
        {
            objNum.SetActive(false);
            tvNum.gameObject.SetActive(false);
            parentTvCoin.SetActive(true);
        }
    }
    public void OnDestroy()
    {
        EventDispatcher.EventDispatcher.Instance.RemoveListener(EventID.SNIPER_BOOSTER, ChangeText);
    }
    public int numOfReflect;
    RaycastHit2D hit;
 
    public ThoneDemo postFireSpike;
    public LineRenderer lineRenderer;
    Vector2 initialDirection;
    public List<RaycastPoint> lsRaycastPoints;
    public LayerMask hitLayers;
   
    public Sprite fireCreaker;
    public Sprite normalSprite;
    public BallMovement ballMovement;




    void Update()
    {
     
        if (!wasUseSniperBooster  )
        {
            return;
        }
        if ( GamePlayController.Instance.gameScene.IsMouseClickingOnImage)
        {
            return;
        }
        if (Input.GetMouseButton(0))
        {          // Lấy vị trí chuột trên màn hình


            // Chuyển đổi vị trí chuột từ màn hình sang thế giới
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Tạo một Ray bắn từ vị trí của thoneDemo qua vị trí người dùng chạm
            Vector3 direction = (worldPosition - postFireSpike.transform.position).normalized;

            // Lưu hướng của raycast để sử dụng khi khởi tạo bóng
            initialDirection = direction;

            // Sử dụng CircleCast thay cho Raycast, với bán kính hình tròn là 0.4f
            Vector3 currentDirection = direction;
            Vector3 currentOrigin = postFireSpike.transform.position;

            // Khởi tạo LineRenderer
            lineRenderer.positionCount = 1;
            lineRenderer.SetPosition(0, currentOrigin);

            int lineIndex = 1;

            // Clear the list at the start of the raycasting process
            lsRaycastPoints.Clear();


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
                //Vector2 reflectDirection = Vector2.Reflect(currentDirection, normal);

                // Vẽ raycast phản chiếu từ điểm va chạm
                //Debug.DrawRay(circleCastOrigin, reflectDirection * 100f, Color.blue, 0.05f);
                //float shortSegmentLength = 2.0f; // Chiều dài của đoạn ngắn, có thể điều chỉnh
                //Vector2 reflectSegmentEnd = circleCastOrigin + reflectDirection.normalized * shortSegmentLength;

                ////// Thêm điểm kết thúc của đoạn ngắn vào LineRenderer
                //lineRenderer.positionCount = lineIndex + 1;
                //lineRenderer.SetPosition(lineIndex, reflectSegmentEnd);
                //lineIndex++;

                //// Tạo và lưu thông tin phản chiếu vào lsRaycastPoints
                //raycastPoint = new RaycastPoint
                //{
                //    startPoint = circleCastOrigin,
                //    endPoint = (Vector3)reflectSegmentEnd
                //};
                //lsRaycastPoints.Add(raycastPoint);

                postFireSpike.gameObject.transform.up = initialDirection;
            }

        }
        if (Input.GetMouseButtonUp(0))
        {

            var temp = SimplePool2.Spawn(ballMovement);
            temp.transform.position = postFireSpike.transform.position;
            temp.transform.up = initialDirection;
            temp.Init(initialDirection, 0);

            lineRenderer.positionCount = 0;
            postFireSpike.spriteRenderer.sprite = normalSprite;
            postFireSpike.transform.localScale = new Vector3(1, 1, 1);
            wasUseSniperBooster = false;
            GamePlayController.Instance.playerContain.levelData.inputThone.enabled = true;
            sniper_Btn.interactable = true;
        }
  

    }
}
