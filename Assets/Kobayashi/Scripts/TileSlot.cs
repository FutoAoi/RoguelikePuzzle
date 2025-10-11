using UnityEngine;
using UnityEngine.UI;

public class TileSlot : MonoBehaviour
{
    [SerializeField] private GameObject _tileBoardPrefab;
    public bool IsOccupied { get; private set; } = false;//すでに置かれているか
    private GameObject _newCard;
    private TileMovement _tileMovement;
    /// <summary>
    /// カードを置く
    /// </summary>
    /// <param name="cardSprite"></param>
    public void PlaceCard(Sprite cardSprite)
    {
        if(IsOccupied)return;
        _newCard = Instantiate(_tileBoardPrefab,transform);
        _newCard.GetComponent<Image>().sprite = cardSprite;

        _tileMovement = _newCard.GetComponent<TileMovement>();
        if(_tileMovement == null)
        {
            _tileMovement = _newCard.AddComponent<TileMovement>();
        }
        _tileMovement.CardSprite = cardSprite;
        _tileMovement.SetAsBoardCard();
        IsOccupied = true;
    }
    /// <summary>
    /// スロットの中を無くす
    /// </summary>
    public void ClearSlot()
    {
        if(_newCard != null)
        {
            Destroy(_newCard);
            _newCard = null;
        }
        IsOccupied = false;
    }
}
