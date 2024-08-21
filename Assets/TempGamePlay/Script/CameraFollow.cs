using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraFollow : MonoBehaviour
{
  
   
    private struct PointInSpace
    {
        public Vector3 Position;
        public float Time;
    }

    [SerializeField]
    public Transform target;
    [SerializeField]
    public Vector3 offset;


    [SerializeField]
    private float delay = 0.5f;

    [SerializeField]

    private float speed = 5;

    private Queue<PointInSpace> pointsInSpace = new Queue<PointInSpace>();

    public bool isActive = false;
    void FixedUpdate()
    {
        if(isActive)
        {
            // Add the current target position to the list of positions
            pointsInSpace.Enqueue(new PointInSpace() { Position = new Vector2(target.transform.position.x, target.transform.position.y), Time = Time.time });
            // Move the camera to the position of the target X seconds ago 
            while (pointsInSpace.Count > 0 && pointsInSpace.Peek().Time <= Time.time - delay + Mathf.Epsilon)
            {
                transform.position = Vector3.Lerp(transform.position, pointsInSpace.Dequeue().Position + offset, Time.deltaTime * speed);
             
            }
        }    
    }


    public void HandleZoom(Transform param)
    {
        if(PlayerContain.testc1)
        {
            return;
        }    
        if(param != null)
        {
            target = param;
            Camera.main.DOOrthoSize(5, 0.7f);
            this.transform.DOMove(new Vector3(target.position.x, target.position.y,-5),0.5f);
         
        }    

        
    }    
}
