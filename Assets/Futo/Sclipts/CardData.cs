using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class CardData
{
    [Header("カード詳細")]
    [SerializeField,Tooltip("カードID")] private int _cardID;
    [SerializeField,Tooltip("カードの見た目")] private Sprite _sprite;
    [SerializeField,Tooltip("カードの名前")] private string _name;
    [SerializeField,Tooltip("カードの説明")] private string _description;
    [SerializeField,Tooltip("カードのコスト")] private int _cost;
    [SerializeField,Tooltip("効果エフェクト")] private EffectBase _effect;

    public int CardID => _cardID;
    public Sprite Sprit => _sprite;
    public string Name => _name;
    public string Description => _description;
    public int Cost => _cost;
    public EffectBase Effect => _effect;
}