using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
public class CardController : MonoBehaviour
{
    public List<CardBase> lsCardBase;
    public List<int> lsDataCard;
    public CardBase GetCardBase(int idCard)
    {
        foreach(var item in lsCardBase)
        {
            if(item.id == idCard)
            {
                return item;
            }
        }
        return null;
    }

    public void Init()
    {
        if (GameController.Instance.isContinue)
        {
            var data = JsonConvert.DeserializeObject<List<int>>(UseProfile.DataCardSave);

            if (data != null && data.Count > 0)
            {
                foreach (var item in data)
                {
                    GetCardBase(item).HandleAction();
                }
            }
            GameController.Instance.isContinue = false;
            UseProfile.isSaveDataCard = false;
        }
    }    

    public void HandleActiveCard(CardType cardTypeParam)
    {
        foreach(var item in lsCardBase)
        {
            if(item.cardType == cardTypeParam)
            {
                item.HandleAction();
            }
        }
    }    
    public List<CardBase> GetCard()
    {
        var tempListCanShow = new List<CardBase>();
        var tempListGet = new List<CardBase>();
        foreach (var item in lsCardBase)
        {
            if(item.CanShow())
            {
                tempListCanShow.Add(item);
            }
        }
        while(tempListGet.Count < 3)
        {
            var tempCardBase = tempListCanShow[Random.RandomRange(0, tempListCanShow.Count)];
            tempListGet.Add(tempCardBase);
            tempListCanShow.Remove(tempCardBase);
           
        }
        return tempListGet;

    }
    public  List<CardBase> GetCardLevel_1()
    {
        var tempListCanShow = new List<CardBase>();
        tempListCanShow.Add(GetCardBase(11));
        tempListCanShow.Add(GetCardBase(12));
        tempListCanShow.Add(GetCardBase(13));
        return tempListCanShow;
    }

    public void CheckCard(int id)
    {
        
            lsDataCard.Add(id);
       
    }

 
  
    private void OnApplicationPause(bool pause)
    {
        UseProfile.DataCardSave = JsonConvert.SerializeObject(lsDataCard);
        UseProfile.isSaveDataCard = true;
    }
    //[Button]
    //public void Test()
    //{
    //   var temp = JsonConvert.SerializeObject(lsDataCard);
    //    Debug.LogError("temp_" + temp);

    //    var data = JsonConvert.DeserializeObject<List<DataCard>>(temp);
    //    Debug.LogError("Count_" + data.Count);
    //}

}

 