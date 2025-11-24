using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager_Battle : UIManagerBase,IBattleUI
{
    [Header("手札")] public List<GameObject> HandCard = new List<GameObject>();

    [Header("数値設定")]
    [SerializeField, Tooltip("手札の数")] private int _handRange = 5;

    [Header("コンポーネント設定")]
    [SerializeField, Tooltip("場所")] private RectTransform _playerHandTr;
    [SerializeField, Tooltip("手札の場所")] public Transform HandArea;
    [SerializeField, Tooltip("カードの基盤")] public GameObject CardPrefab;
    [SerializeField, Tooltip("ドラッグ時の場所")] public RectTransform DragLayer;

    private DeckManager _deckManager;
    private GameObject _card;
    public override void InitUI()
    {
        _deckManager = DeckManager.Instance;
        GameManager.Instance.CurrentPhase = BattlePhase.Draw;
        HandCard.Clear();
    }
    public void DrawCard()
    {
        for (int i = 0; i < _handRange; i++)
        {
            CreateCard();
        }
    }

    public void HandOrganize()
    {
        foreach (var tile in HandCard)
        {
            tile.transform.SetParent(_playerHandTr, false);
        }
        LayoutRebuilder.ForceRebuildLayoutImmediate(_playerHandTr);
    }
    
    private void CreateCard()
    {
        _card = Instantiate(CardPrefab);
        _card.AddComponent<Card>().SetCard(_deckManager.DrawCard());
        //後で戻すときにカードを生成するようにする
        _card.AddComponent<CardMovement>();
        HandCard.Add(_card);
    }
}
