using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThoneDemo : MonoBehaviour
{
    public GameObject parent;
    public SpriteRenderer spriteRenderer;
    // Update is called once per frame
    public bool isBooster = false;

    public void HandleBooster()
    {
        isBooster = true;
    }    
    private void FixedUpdate()
    {
        this.transform.localEulerAngles -= new Vector3(0, 0, 5);
    }

    private void OnMouseDrag()
    {
        if(isBooster)
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
            parent.transform.position = new Vector3(worldPosition.x, parent.transform.position.y, 0);
        }    
     
    }
    private void OnMouseUp()
    {
        if (isBooster)
        {
            isBooster = false;
        }
    }
}
