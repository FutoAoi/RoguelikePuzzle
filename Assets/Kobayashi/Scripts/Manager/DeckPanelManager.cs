using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class DeckPanelManager : MonoBehaviour
{
    [Header("各エリア")]
    [SerializeField, Tooltip("山札")] private RectTransform _deckArea;
    [SerializeField, Tooltip("捨て札")] private RectTransform _discardArea;
    [SerializeField, Tooltip("除外札")] private RectTransform _removeArea;

    [Header("参照")]
    [SerializeField, Tooltip("生成するカード")] private GameObject _cardPrefab;

    private DeckManager _deckManager;
    private GameManager _gameManager;
    private UIManager_Battle _uiManager;

    private Dictionary<int, List<GameObject>> _deckDict = new();
    private Dictionary<int, List<GameObject>> _discardDict = new();
    private Dictionary<int, List<GameObject>> _removeDict = new();
    private void Start()
    {
        if (_deckManager == null)
        {
            Init();
        }
    }

    public void Init()
    {
        _gameManager = GameManager.Instance;

        if (_gameManager.CurrentUIManager.TryGetComponent(out UIManager_Battle ui))
            _uiManager = ui;

        _deckManager = DeckManager.Instance;

        for (int i = 0; i < _deckManager.DeckMain.Count; i++)
        {
            int id = _deckManager.DeckMain[i];
            InstantiateCard(InGameDeckType.Deck, id);
            InstantiateCard(InGameDeckType.Discard, id);

            if (_gameManager.CardDataBase.GetCardData(id).IsDestruction)
            {
                InstantiateCard(InGameDeckType.Remove, id);
            }
        }
    }
    private void OnEnable()
    {
        if (_deckManager == null)
        {
            Init();
        }
        UpdateDeckPanel();
    }

    public void UpdateDeckPanel()
    {
        DisableAll(_deckDict);
        DisableAll(_discardDict);
        DisableAll(_removeDict);

        EnableCards(_uiManager.DeckCard, _deckDict);
        EnableCards(_uiManager.DiscardCard, _discardDict);
        EnableCards(_uiManager.RemoveCard, _removeDict);
    }
    void DisableAll(Dictionary<int, List<GameObject>> dict)
    {
        foreach (var list in dict.Values)
        {
            foreach (var obj in list)
            {
                obj.SetActive(false);
            }
        }
    }
    void EnableCards(List<int> ids, Dictionary<int, List<GameObject>> dict)
    {
        Dictionary<int, int> counter = new();

        foreach (var id in ids)
        {
            if (!counter.ContainsKey(id))
                counter[id] = 0;

            int index = counter[id];

            if (dict.TryGetValue(id, out var list))
            {
                if (index < list.Count)
                {
                    list[index].SetActive(true);
                    counter[id]++;
                }
            }
        }
    }
    /// <summary>
    /// 指定のカードを生成し、
    /// 対応した場所の子オブジェクトに設定し、
    /// 非表示にする
    /// </summary>
    /// <param name="type">種類分け</param>
    /// <param name="id">どのIDのカードか</param>
    public void InstantiateCard(InGameDeckType type,int id)
    {
        Dictionary<int, List<GameObject>> dict = null;
        RectTransform rect = null;
        switch (type)
        {
            case InGameDeckType.Deck:
                dict = _deckDict;
                rect = _deckArea;
                break;

            case InGameDeckType.Discard:
                dict = _discardDict;
                rect = _discardArea;
                break;

            case InGameDeckType.Remove:
                dict = _removeDict;
                rect = _removeArea;
                break;

            default:
                break;
        }

        if (!dict.ContainsKey(id))
        {
            dict[id] = new List<GameObject>();
        }

        GameObject card = Instantiate(_cardPrefab);
        card.transform.SetParent(rect,false);
        Debug.Log("a");
        if (card.TryGetComponent(out Card cardData))
        {
            cardData.CardID = id;
            cardData.SetCard(id, false);
        }

        dict[id].Add(card);
        card.SetActive(false);
    }
}
