using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Newtonsoft.Json;

public class Ballon : BarrialAir
{
    public Sprite eplosionBallon;
    public SpriteRenderer spriteRenderer;
    public CircleCollider2D circleCollider;
    public GameObject outLine;
    public bool isOff;
    public Vector3 positionX ; // Target position X
    public Vector3 positionY; // Target position Y
    public float speed = 2f;  // Speed of movement
    private bool movingUp = true;
    public bool isInit;
    public LevelData levelData;

  


    public virtual void Explosion()
    {
        circleCollider.enabled = false;
        spriteRenderer.sprite = eplosionBallon;
    
        //if (!isOff)
        //{
        //    isOff = true;
        //    GameController.Instance.musicManager.PlayRandomBallon();
        //    GamePlayController.Instance.gameScene.HandleSubtrackBallon();
        //    GamePlayController.Instance.playerContain.levelData.CountWin(this.transform);
        //    GamePlayController.Instance.HandlSpawnItemInGameBallon(1, this.transform.position);

        //}


    }

    public override void Init()
    {
        positionX = new Vector3(this.transform.position.x, this.transform.position.y + 0.2f, 0);
        positionY = new Vector3(this.transform.position.x, this.transform.position.y - 0.2f, 0);
        speed = Random.RandomRange(0.1f, 0.5f);
        isInit = true;
    }
    public override void TakeDameSpike()
    {
        if(isInit)
        {
            Explosion();
            var temp = GetBallShame(id);
            if (temp != null)
            {
                temp.TakeDameSpike();
            }

        }    
    }
 
    private void FixedUpdate()
    {
        if (!isOff)
        {
            if (movingUp)
            {
                transform.position = Vector3.MoveTowards(transform.position, positionX, speed * Time.fixedDeltaTime);
                if (Vector3.Distance(transform.position, positionX) < 0.01f)
                {
                    movingUp = false;
                }
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, positionY, speed * Time.fixedDeltaTime);
                if (Vector3.Distance(transform.position, positionY) < 0.01f)
                {
                    movingUp = true;
                }
            }
        }
   
    }

    [Button]
    private void HandleRandom()
    {
        var temp = Random.RandomRange(0, levelData.lsDataBallon.Count);
        spriteRenderer.sprite = levelData.lsDataBallon[temp].ballon;
        eplosionBallon = levelData.lsDataBallon[temp].eplosionBallon;
        id = levelData.lsDataBallon[temp].id;

    }

   
    public void HandleSave()
    {
        var lsid = new List<int>();
        foreach (var item in lsNearBallon)
        {
            lsid.Add(item.idInLevelT);
        }

        var temp = JsonConvert.SerializeObject(lsid);
        PlayerPrefs.SetString("HexaMap" + idInLevelT.ToString(), temp);



        var lsid_Up = new List<int>();
        foreach (var item in lsUpBallon)
        {
            lsid_Up.Add(item.idInLevelT);
        }

        var temp_Up = JsonConvert.SerializeObject(lsid_Up);
        PlayerPrefs.SetString("HexaMap_Up" + idInLevelT.ToString(), temp_Up);
    }
 
    public void HandleLoad()
    {
        var data = PlayerPrefs.GetString("HexaMap" + idInLevelT.ToString());
        var tempLoad = JsonConvert.DeserializeObject<List<int>>(data);
        foreach (var item in tempLoad)
        {

            lsNearBallon.Add((Ballon)levelData.GetBallon(item));
        }

        var data_Up = PlayerPrefs.GetString("HexaMap_Up" + idInLevelT.ToString());
        var tempLoad_Up = JsonConvert.DeserializeObject<List<int>>(data_Up);
        foreach (var item in tempLoad_Up)
        {

            lsUpBallon.Add((Ballon)levelData.GetBallon(item));
        }

    }
}

[System.Serializable]
public class DataBallon
{
    public int id;
    public Sprite ballon;
    public Sprite eplosionBallon;
    public Color color;
}
