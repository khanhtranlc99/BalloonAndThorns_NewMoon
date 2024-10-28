using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BallBase : MonoBehaviour
{
    public bool activeMove;
    public bool wasTouch;
    public bool endGame;
    public abstract void Init(Vector2 param);
    public abstract IEnumerator Move(Vector2 param);
}
