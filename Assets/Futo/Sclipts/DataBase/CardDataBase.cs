using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DataBase/CardDataBase")]
public class CardDataBase : ScriptableObject
{
    [Header("カードデータベース")]
    [SerializeField,Tooltip("カードリスト")] private List<CardData> _cards = new();

    private Dictionary<int, CardData> _cardDictionary;
    
    void Initialize()
    {
        if(_cardDictionary == null)
        {
            _cardDictionary = new Dictionary<int, CardData>();
            foreach(var card in _cards)
            {
                if(!_cardDictionary.ContainsKey(card.CardID))
                {
                    _cardDictionary.Add(card.CardID, card);
                }
                else
                {
                    Debug.LogWarning($"重複したキーがあります:{card.CardID}");
                }
            }
        }
    }

    public CardData GetCardData(int ID)
    {
        Initialize();
        if(_cardDictionary.TryGetValue(ID, out var cardData))
        {
            return cardData;
        }
        Debug.LogWarning($"ID{ID}のカードが見つかりません");
        return null;
    }
}

[System.Serializable]
public class CardData
{
    [Header("カード詳細")]
    [SerializeField, Tooltip("カードID")] private int _cardID;
    [SerializeField, Tooltip("カードのレアリティ")] private CardRarity _rarity;
    [SerializeField, Tooltip("カードの見た目")] private Sprite _sprite;
    [SerializeField, Tooltip("カードの名前")] private string _name;
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