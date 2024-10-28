using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCollider : MonoBehaviour
{
    public bool isBooster = false;
    public GameObject vfxBooster;
    public InputThone inputThone;
    public GameObject parent;
    public void HandleBooster()
    {
        isBooster = true;
        vfxBooster.SetActive(true);

    }

    private void OnMouseDown()
    {
        GamePlayController.Instance.playerContain.inputThone.lockShoot = true;
    }

    private void OnMouseDrag()
    {
        if (isBooster)
        {

            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
            parent.transform.position = new Vector3(worldPosition.x, parent.transform.position.y, 0);
            if (parent.transform.position.x <= GamePlayController.Instance.limitLeft.position.x)
            {
                parent.transform.position = new Vector3(GamePlayController.Instance.limitLeft.position.x, parent.transform.position.y, 0);

            }
            if (parent.transform.position.x >= GamePlayController.Instance.limitRight.position.x)
            {
                parent.transform.position = new Vector3(GamePlayController.Instance.limitRight.position.x, parent.transform.position.y, 0);

            }
        }

    }
    private void OnMouseUp()
    {
        if (isBooster)
        {
           
            GamePlayController.Instance.playerContain.inputThone.lockShoot = false;
        }
    }

}
