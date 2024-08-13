using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitVfx : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnEnable()
    {
        Invoke(nameof(DeSpawn), 1);
    }
  

    private void DeSpawn()
    {
        Debug.LogError("DeSpawn");
        SimplePool2.Despawn(this.gameObject);
    }    
  
}
