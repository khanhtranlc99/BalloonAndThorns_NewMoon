using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BarrialAir : MonoBehaviour
{
    public abstract void Init();
    public abstract void TakeDameSpike();
    public abstract void TakeDameSpikeEffect(BallMovement paramBall);

}
