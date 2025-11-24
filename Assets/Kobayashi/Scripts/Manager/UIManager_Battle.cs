using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager_Battle : UIManagerBase
{
    [Header("èD")] public List<Image> HandTile = new List<Image>();

    [Header("ƒRƒ“ƒ|[ƒlƒ“ƒgİ’è")]
    [SerializeField, Tooltip("êŠ")] private RectTransform _playerHandTr;
    [SerializeField, Tooltip("èD‚ÌêŠ")] public Transform HandArea;
    [SerializeField, Tooltip("")] public GameObject CardPrefab;
    [SerializeField, Tooltip("")] public RectTransform DragLayer;

    public override void InitUI()
    {
        GameManager.Instance.CurrentPhase = BattlePhase.Draw;
    }
    /// <summary>
    /// èD‚ğ•À‚×‚é
    /// </summary>
    public void HandOrganize()
    {
        foreach (var tile in HandTile)
        {
            tile.transform.SetParent(_playerHandTr, false);
        }
        LayoutRebuilder.ForceRebuildLayoutImmediate(_playerHandTr);
    }
}
