﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandTutGamePlay : MonoBehaviour
{



    public int numOfReflect;
    RaycastHit2D hit;
    Vector2 initialDirection;
    public ThoneDemo postFireSpike;
    public LineRenderer lineRenderer;
    public List<GameObject> lsThoneDemos;
    public List<RaycastPoint> lsRaycastPoints;
    public LayerMask hitLayers;
    public Transform param;
    bool wasInit = false;
    public SpriteRenderer iconCanon;

    public void Init (InputThone paramInput)
    {
        iconCanon = paramInput.iconCanon;
        postFireSpike = paramInput.postFireSpike;
        wasInit = true;
   
        //if (RemoteConfigController.GetFloatConfig(FirebaseConfig.ID_BACK_GROUND, 1) == 2)
        //{
        //    lineRenderer.SetColors(Color.black, Color.black);
        //}
    }

     


    void Update()
    {
        if (!wasInit)
        {
            return;
        }
        // Lấy vị trí chuột trên màn hình
  

        // Chuyển đổi vị trí chuột từ màn hình sang thế giới
        Vector3 worldPosition = param.position;

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

        Vector2 direction2 = (worldPosition - iconCanon.transform.position).normalized;

        // Calculate the angle in degrees
        float angle = Mathf.Atan2(direction2.y, direction2.x) * Mathf.Rad2Deg;

        // Apply rotation to the cannon (rotating on the Z-axis)
        iconCanon.transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
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
}
