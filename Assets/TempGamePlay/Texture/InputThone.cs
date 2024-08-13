using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputThone : MonoBehaviour
{
    public BallController ballController;


    public LineRenderer lineRenderer;

    public ThoneDemo thoneDemo;

    Vector3 firstPost;
    Vector3 secondPost;
    bool temp111;

    public BallController currentBallController;



    // Update is called once per frame
    void Update()
    {

         if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Tạo một tia chớp từ vị trí của con trỏ chuột
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            // Kiểm tra xem tia chớp có chạm vào bất kỳ đối tượng nào không
            if (hit.collider != null && hit.collider.gameObject.tag == "Grid")
            {
                temp111 = true;
            }
            else
            {
                temp111 = false;
            }    

            if(temp111)
            {
                lineRenderer.positionCount = 2;
                firstPost = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                firstPost.z = 0;
            }    
           
        }
        if (Input.GetMouseButton(0))
        {
            if (temp111)
            {
                secondPost = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                secondPost.z = 0;

                // Tính khoảng cách giữa firstPost và secondPost
                float distance = Vector2.Distance(firstPost, secondPost);

                // Nếu khoảng cách nhỏ hơn hoặc bằng 1.3f, cập nhật vị trí bình thường
                if (distance <= 1.3f)
                {
                    lineRenderer.SetPosition(0, firstPost);
                    lineRenderer.SetPosition(1, secondPost);
                    thoneDemo.spriteRenderer.color = new Color32(255, 255, 255, 255);
                    thoneDemo.transform.position = secondPost;
                }
                else
                {
                    // Giới hạn secondPost ở khoảng cách 1.3
                    Vector3 direction = (secondPost - firstPost).normalized;
                    secondPost = firstPost + direction * 1.3f;

                    lineRenderer.SetPosition(0, firstPost);
                    lineRenderer.SetPosition(1, secondPost);
                    thoneDemo.spriteRenderer.color = new Color32(255, 255, 255, 255);
                    thoneDemo.transform.position = secondPost;
                }
            }
       
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (temp111)
            {
                lineRenderer.positionCount = 0;
                thoneDemo.spriteRenderer.color = new Color32(0, 0, 0, 0);
                Vector3 directionVector = firstPost - secondPost;

                // Optionally, normalize the direction vector if you need a unit vector
                Vector3 normalizedDirectionVector = directionVector.normalized;

                // Log or use the direction vector as needed
                Debug.Log("Direction Vector: " + directionVector);
                Debug.Log("Normalized Direction Vector: " + normalizedDirectionVector);
                var temp = Instantiate(ballController);
                temp.transform.position = secondPost;
                temp.AddForceBall(normalizedDirectionVector);

                currentBallController = temp;

            }
     
        }

    }
}
