using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonSkinController : MonoBehaviour
{
    public SpriteRenderer cannon;
    public List<IdBall> lsIdCanon;
    public Sprite GetCanon
    {
        get
        {
            foreach (var item in lsIdCanon)
            {
                if (item.id == UseProfile.id_cannon_skin)
                {
                    return item.ballBase;
                }
            }
            return null;
        }


    }

    public void Init()
    {
        EventDispatcher.EventDispatcher.Instance.RegisterListener(EventID.CHANGE_ID_CANNON, HandleReskin);
        HandleReskin(null);
    }

    public void HandleReskin(object param)
    {
        cannon.sprite = GetCanon;
    }

    private void OnDestroy()
    {
        EventDispatcher.EventDispatcher.Instance.RemoveListener(EventID.CHANGE_ID_CANNON, HandleReskin);
    }
}
