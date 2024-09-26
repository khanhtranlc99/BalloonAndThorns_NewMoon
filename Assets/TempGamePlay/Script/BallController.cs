using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public Rigidbody2D rigidbody2D;
    public SpriteRenderer spriteRenderer;
    public HitVfx vfxTouch;
    public AudioSource audioSource;
    public BallController ballMovementPrefab;


    void Update()
    {
        this.transform.localEulerAngles -= new Vector3(0, 0, 5);
    }

    public void AddForceBall( Vector2 param)
    {
        rigidbody2D.AddForce(param  , ForceMode2D.Impulse);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        

        // Lấy vị trí va chạm đầu tiên
        Vector2 contactPoint = collision.contacts[0].point;

        // Spawn đối tượng vfxTouch tại vị trí va chạm
        var temp = SimplePool2.Spawn(vfxTouch);
        temp.transform.position = contactPoint;
        audioSource.Play();

        if (collision.gameObject.tag == "Broad")
        {
            SimplePool2.Despawn(this.gameObject);
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<BarrialAir>() != null)
        {
            collision.gameObject.GetComponent<BarrialAir>().TakeDameSpike();
            collision.gameObject.GetComponent<BarrialAir>().TakeDameSpikeEffect(null, this);
        }
        
    }

    public void HandleBoosterX2()
    {

        // Sử dụng hướng của trục x của đối tượng làm initialDirection
        Vector3 initialDirection = transform.right;

        // Spawn và khởi tạo temp1 với hướng 45 độ
        var temp1 = SimplePool2.Spawn(ballMovementPrefab);
        temp1.transform.position = this.transform.position;
        Debug.LogError("temp1"+ temp1.gameObject.name);

        // Xoay hướng ban đầu 45 độ quanh trục Z
        Vector3 direction1 = Quaternion.Euler(0, 0, 45) * transform.right;
     
        temp1.AddForceBall(direction1* 1.8f);
        GamePlayController.Instance.playerContain.levelData.inputThone.lsBallMovement.Add(temp1);
        // Spawn và khởi tạo temp2 với hướng -45 độ
        var temp2 = SimplePool2.Spawn(ballMovementPrefab);
        temp2.transform.position = this.transform.position;

        // Xoay hướng ban đầu -45 độ quanh trục Z
        Vector3 direction2 = Quaternion.Euler(0, 0, -45) * transform.up;
 
        temp2.AddForceBall(direction2 * 1.8f);
        Debug.LogError("temp2" + temp2.gameObject.name);
  
        GamePlayController.Instance.playerContain.levelData.inputThone.lsBallMovement.Add(temp2);

        SimplePool2.Despawn(this.gameObject);
    }
    private void OnDisable()
    {
        try
        {
            GamePlayController.Instance.playerContain.levelData.HandleCheckLose();
        }
        catch
        {

        }
    }

}
