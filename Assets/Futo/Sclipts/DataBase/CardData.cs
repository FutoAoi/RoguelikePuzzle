using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Datas/Card")]
public class CardData : ScriptableObject
{
    [Header("カード詳細")]
    [SerializeField, Tooltip("カードID")] private int _cardID;
    [SerializeField, Tooltip("カードのレアリティ")] private CardRarity _rarity;
    [SerializeField, Tooltip("カードの見た目")] private Sprite _sprite;
    [SerializeField, Tooltip("カードの名前")] private string _name;
    [TextArea(3, 10)]
    [SerializeField, Tooltip("カードの説明")] private string _description;
    [SerializeField, Tooltip("カードのコスト")] private int _cost;
    [SerializeField, Tooltip("最大回数")] private int _maxTimes;

    [SerializeReference, SubclassSelector] private IEffect[] _effect;

    public int CardID => _cardID;
    public Sprite Sprite => _sprite;
    public string Name => _name;
    public string Description => _description;
    public int Cost => _cost;
    public IEffect[] Effect => _effect;
    public int MaxTimes => _maxTimes;
    public CardRarity Rarity => _rarity;
}