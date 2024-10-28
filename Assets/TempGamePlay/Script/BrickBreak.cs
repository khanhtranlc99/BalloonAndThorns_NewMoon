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

    public override void TakeDameSpike(int paramDame)
    {
     
        heart -= paramDame;
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
    public override void TakeDameSpikeEffect(BallBase paramBall )
    {

    }
    public override void HandleColorBallon()
    {
       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        TakeDameSpike(DamePlusCard.dame);
    }
    public override List<BarrialAir> GetBallondsAround( )
    {
       
        return null;
    }
    public override List<BarrialAir> GetBallondsLeftRight( )
    {
       
        return null;
    }
    public override IEnumerator Move()
    {
        yield return null;
    }
    public override void Destroy()
    {
  
    }
}
