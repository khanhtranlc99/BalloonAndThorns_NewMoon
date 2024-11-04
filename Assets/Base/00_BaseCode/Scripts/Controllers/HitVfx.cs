using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitVfx : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnEnable()
    {
        StartCoroutine(DeSpawn());

    }
  

    private IEnumerator  DeSpawn()
    {
        yield return new WaitForSeconds(0.4f);
        SimplePool2.Despawn(this.gameObject);
    }    
  
}
