using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickBreak : BarrialAir
{
    public SpriteRenderer spriteRD;
    int heart = 3;
    public ParticleSystem partycale;
    public override void Init()
    {
        heart = 3;
    }

    public override void TakeDameSpike()
    {
     
        heart -= 1;
        if(heart == 2)
        {
            spriteRD.color = new Color32(255,255,255,100);
     
        }
        if (heart == 1)
        {
            spriteRD.color = new Color32(255, 255, 255, 80);
        }
        if (heart == 0)
        {
            Destroy(this.gameObject);
        }
        partycale.Play();
    }
    public override void TakeDameSpikeEffect(BallMovement paramBall, BallController ballController)
    {

    }
    public override void HandleColorBallon()
    {
       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        TakeDameSpike();
    }
}
