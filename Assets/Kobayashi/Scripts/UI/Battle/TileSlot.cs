using System;
using UnityEngine;
using UnityEngine.UI;

public class TileSlot : MonoBehaviour
{
    [SerializeField] private GameObject _tileBoardPrefab;
    [SerializeField] private Sprite[] _tileSprites;
    [NonSerialized] public int ID;
    public bool IsLastTimeCard = false;//前のフェーズで置かれたものかどうか
    public bool IsOccupied { get; private set; } = false;//すでに置かれているか
    private GameObject _newCard;
    private CardMovement _tileMovement;
    private Image _img;
    private int _index,_currentnumber;

    private void Start()
    {
        _img = GetComponent<Image>();
        _index = UnityEngine.Random.Range(0, _tileSprites.Length - 1);
        _img.sprite = _tileSprites[_index];
        IsLastTimeCard = false;
    }
    /// <summary>
    /// カードを置く
    /// </summary>
    /// <param name="cardSprite"></param>
    public void PlaceCard(int id)
    {
        if(IsOccupied)return;
        ID = id;
        _newCard = Instantiate(_tileBoardPrefab,transform);
        _newCard.GetComponent<Image>().sprite = GameManager.Instance.CardDataBase.GetCardData(id).Sprite;
        _tileMovement = _newCard.GetComponent<CardMovement>();
        _tileMovement.ID = id;
        _tileMovement.SetAsBoardCard();
        _currentnumber = GameManager.Instance.CardDataBase.GetCardData(id).MaxTimes;
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
    /// <summary>
    /// スロットの使用回数減少
    /// </summary>
    /// <param name="times"></param>
    public void DecreaseTimes(int times)
    {
        if (!IsOccupied) return;

        _currentnumber -= times;
        if(_currentnumber <= 0)
        {
            ClearSlot();
        }
    }
}
