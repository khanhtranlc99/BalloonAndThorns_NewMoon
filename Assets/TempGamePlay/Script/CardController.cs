using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardController : MonoBehaviour
{
    public List<CardBase> lsCardBase;
 
    public void Init()
    {

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

}
