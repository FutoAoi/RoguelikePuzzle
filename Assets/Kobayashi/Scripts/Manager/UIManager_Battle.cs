using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager_Battle : UIManagerBase,IBattleUI
{
    [Header("手札")] public List<Image> HandTile = new List<Image>();

    [Header("コンポーネント設定")]
    [SerializeField, Tooltip("場所")] private RectTransform _playerHandTr;
    [SerializeField, Tooltip("手札の場所")] public Transform HandArea;
    [SerializeField, Tooltip("カードの基盤")] public GameObject CardPrefab;
    [SerializeField, Tooltip("ドラッグ時の場所")] public RectTransform DragLayer;

    private GameObject _card;
    public override void InitUI()
    {
        GameManager.Instance.CurrentPhase = BattlePhase.Draw;
    }
    /// <summary>
    /// 手札を並べる
    /// </summary>
    public void HandOrganize()
    {
        foreach (var tile in HandTile)
        {
            tile.transform.SetParent(_playerHandTr, false);
        }
        LayoutRebuilder.ForceRebuildLayoutImmediate(_playerHandTr);
    }
    
    public void CreateCard()
    {
        _card = Instantiate(CardPrefab);
        
    }
}
