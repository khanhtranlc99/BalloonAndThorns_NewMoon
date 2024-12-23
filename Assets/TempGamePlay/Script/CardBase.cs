using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum CardType
{
    SniperCard,
    DamePlusCard,
    BallPlusCard,
    ShootPlusCard,
    FireCrackerCard,
    BoomBallCard,
    SeeThroughBallCard,
    ElectricBallCard,
    LightlingBallCard,
    SoundWaveCard,
    EndOfElectricCard,
    EndOfLightlingCard,
    EndOfExplosionCard,
    EndOfRocketCard,
    IncreaseRocketCard,
    IncreaseElectricCard,
    IncreaseLightlingCard,
    IncreaseSoundWaveCard,
}    
public enum CardRank
{
    Normal,
    Platium,
    Gold
    
}    
public abstract class CardBase : MonoBehaviour
{
    public int id;
    public CardRank cardRank;
    public CardType cardType;
    public Sprite sprite;
    public string name;
    public string content;

    public abstract void Init();
    public abstract bool CanShow();
    public abstract void HandleAction();

}
