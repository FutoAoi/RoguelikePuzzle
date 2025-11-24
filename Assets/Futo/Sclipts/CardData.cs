using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class CardData
{
    [SerializeField] private int _cardID;
    [SerializeField] private Sprite _sprit;
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private int _cost;
    [SerializeField] private EffectBase _effect;

    public int CardID => _cardID;
    public Sprite Sprit => _sprit;
    public string Name => _name;
    public string Description => _description;
    public int Cost => _cost;
    public EffectBase Effect => _effect;
}