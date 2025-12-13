using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RewardCard : MonoBehaviour, IPointerDownHandler
{
    [Header("カード詳細")]
    [SerializeField, Tooltip("カードのレアリティ")] private CardRarity _rarity;
    [SerializeField, Tooltip("カードID")] private int _cardID;
    [SerializeField, Tooltip("カードの見た目")] private Image _image;
    [SerializeField, Tooltip("カードの名前")] private TMP_Text _name;
    [SerializeField, Tooltip("カードの説明")] private TMP_Text _description;
    [SerializeField, Tooltip("カードのコスト")] private TMP_Text _cost;
    [SerializeField, Tooltip("最大回数")] private TMP_Text _maxTimes;
    [SerializeField, Tooltip("報酬番号")] private int _rewardNumber;
    [SerializeField, Tooltip("RewardManager")] private RewardManager _rewardManager;

    public int CardID => _cardID;

    private CardData _data;

    public void OnPointerDown(PointerEventData eventData)
    {
        _rewardManager.SetRewardNumber(_rewardNumber);
    }

    public void SetCard(int ID)
    {
        _data = GameManager.Instance.CardDataBase.GetCardData(ID);
        _cardID = ID;
        _rarity = _data.Rarity;
        _image.sprite = _data.Sprite;
        _name.text = _data.Name;
        _description.text = _data.Description;
        _cost.text = $"{_data.Cost}";
        _maxTimes.text = $"{_data.MaxTimes}";
    }

}
