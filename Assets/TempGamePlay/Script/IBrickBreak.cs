using System.Collections;

public interface IBrickBreak
{
    void Destroy();
    void HandleColorBallon();
    void Init();
    IEnumerator Move();
    void TakeDameSpike(int paramDame);
    void TakeDameSpikeEffect(BallBase paramBall);
}