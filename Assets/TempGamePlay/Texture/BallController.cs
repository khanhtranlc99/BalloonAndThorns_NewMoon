using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public Rigidbody2D rigidbody2D;
    public SpriteRenderer spriteRenderer;
    public HitVfx vfxTouch;
    public AudioSource audioSource;

    void Update()
    {
        this.transform.localEulerAngles -= new Vector3(0, 0, 5);
    }

    public void AddForceBall( Vector2 param)
    {
        rigidbody2D.AddForce(new Vector2(param.x, param.y) * 500);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        GamePlayController.Instance.playerContain.levelData.HandleCheckLose();

        // Lấy vị trí va chạm đầu tiên
        Vector2 contactPoint = collision.contacts[0].point;

        // Spawn đối tượng vfxTouch tại vị trí va chạm
        var temp = SimplePool2.Spawn(vfxTouch);
        temp.transform.position = contactPoint;
        audioSource.Play();
        Debug.LogError("BallController");
       
    }

}
