using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// èD
/// </summary>
public class TileHand : MonoBehaviour
{
    [Header("èD")] public List<Image> HandTile = new List<Image>();

    [Header("ƒRƒ“ƒ|[ƒlƒ“ƒgİ’è")]
    [SerializeField,Tooltip("êŠ")] private RectTransform _playerHandTr;

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
