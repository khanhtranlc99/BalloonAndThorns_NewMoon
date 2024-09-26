using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallTest : MonoBehaviour
{
    public Rigidbody2D rigidbody2D;

    public void AddForceBall(Vector2 param)
    {
        rigidbody2D.AddForce(param, ForceMode2D.Impulse);
    }
}