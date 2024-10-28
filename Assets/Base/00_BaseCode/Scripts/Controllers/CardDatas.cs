using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Datas/CardDatas", fileName = "CardDatas.asset")]
public class CardDatas : ScriptableObject
{
    public List<DataContent> lsDataContents;



    public DataContent GetDataContent()
    {
        return null;
    }
        

}

[System.Serializable]
public class DataContent
{
    public CardType cardType;
    public Sprite sprite;
    public string content;
   
}