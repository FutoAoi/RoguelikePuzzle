using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    public static DeckManager Instance { get; private set; }

    [Header("メインデッキ")]
    [SerializeField] private List<int> _deckMain = new List<int>();

    private int _randomIndex;
    private int _temp;
    private List<int> _deck;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    /// <summary>
    /// 報酬などでメインのデッキに入れるために使用予定
    /// </summary>
    /// <param name="id"></param>
    public void AddDeck(int id)
    {
        _deckMain.Add(id);
    }

    /// <summary>
    /// デッキのシャッフルメソット
    /// </summary>
    public void ShuffleDeck()
    {
        _deck = new List<int>(_deckMain);
        for(int i = 0; i < _deck.Count; i++)
        {
            _randomIndex = Random.Range(i, _deck.Count);

            _temp = _deck[i];
            _deck[i] = _deck[_randomIndex];
            _deck[_randomIndex] = _temp;
        }
        Debug.Log("デッキをシャッフルしました");
    }

    /// <summary>
    /// デッキドローメッソト
    /// </summary>
    /// <returns></returns>
    public int DrawCard()
    {
        if (_deck.Count == 0)
        {
            ShuffleDeck();
        }

        int _topCard = _deck[0];
        _deck.RemoveAt(0);
        return _topCard;
    }
}
