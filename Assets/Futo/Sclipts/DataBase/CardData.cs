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
    [SerializeField, Tooltip("効果エフェクト")] private EffectBase _effect;
    [SerializeField, Tooltip("最大回数")] private int _maxTimes;

    public int CardID => _cardID;
    public Sprite Sprite => _sprite;
    public string Name => _name;
    public string Description => _description;
    public int Cost => _cost;
    public EffectBase Effect => _effect;
    public int MaxTimes => _maxTimes;
    public CardRarity Rarity => _rarity;
}