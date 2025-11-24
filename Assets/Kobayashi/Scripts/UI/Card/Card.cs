using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [Header("カードの見た目")]
    [SerializeField, Tooltip("挿絵")] private Image _cardImage;
    [SerializeField, Tooltip("カード名")] private TextMeshProUGUI _nameText;
    [SerializeField,Tooltip("コスト表示")]private TextMeshProUGUI _costText;

    private CardDataBase _cardData;
    private int _cardID,_cardCost;
    private string _description;
    public void SetCard(int id)
    {
        _cardData = GameManager.Instance.CardData;
        _cardID = id;
        _cardImage.sprite = _cardData.GetCardData(_cardID).Sprit;
        _nameText.text = _cardData.GetCardData(_cardID).Name;
        _cardCost = _cardData.GetCardData(_cardID).Cost;
        _costText.text = _cardCost.ToString();
        _description = _cardData.GetCardData(_cardID).Description;
    }
}
