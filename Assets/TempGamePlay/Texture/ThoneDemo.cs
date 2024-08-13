using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThoneDemo : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    // Update is called once per frame
    void Update()
    {
        this.transform.localEulerAngles -= new Vector3(0, 0, 5);
    }


}
