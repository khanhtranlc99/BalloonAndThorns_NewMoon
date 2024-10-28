using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public abstract class BarrialAir : MonoBehaviour
{
    public Vector3 postFly;
    public int countExplosion;
    public TMP_Text tvExplosion;
    public List< ParticleSystem> vfxExprosion;
    public bool isInit;
    public bool isFade;
    public bool isOff;
    public abstract void Init();
    public abstract void TakeDameSpike(int paramDame);
    public abstract void TakeDameSpikeEffect(BallBase paramBall );

    public abstract void HandleColorBallon();
    public abstract IEnumerator Move();
    public abstract List<BarrialAir> GetBallondsAround( );
    public abstract List<BarrialAir> GetBallondsLeftRight();
    public abstract void Destroy();

}
