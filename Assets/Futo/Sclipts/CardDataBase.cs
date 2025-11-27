using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DataBase/CardDataBase")]
public class CardDataBase : ScriptableObject
{
    [SerializeField] private List<CardData> _cards = new();

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