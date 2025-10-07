using UnityEngine;
using UnityEngine.UI;

public class TileSlot : MonoBehaviour
{
    [SerializeField] private GameObject _tileBoardPrefab;
    private bool _isOccupied = false;
    private GameObject _newCard;
    public void PlaceCard(Sprite cardSprite)
    {
        if(_isOccupied)return;
        _newCard = Instantiate(_tileBoardPrefab,transform);
        _newCard.GetComponent<Image>().sprite = cardSprite;
        _isOccupied = true;
    }
}
