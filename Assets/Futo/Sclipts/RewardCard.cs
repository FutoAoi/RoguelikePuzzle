using DG.Tweening;
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
    private Vector3 normalScale;
    private float selectedScale = 1.1f;
    private float animationTime = 0.2f;


    public void OnPointerDown(PointerEventData eventData)
    {
        _rewardManager.SetRewardNumber(_rewardNumber);
        foreach(var rewardCard in _rewardManager.RewardCards)
        {
            rewardCard.Deselect();
        }
        Select();
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
        normalScale = transform.localScale;
    }

    public void Select()
    {
        transform.DOKill();

        transform.DOScale(normalScale * selectedScale, animationTime)
                 .SetEase(Ease.OutBack);
    }

    /// <summary>
    /// カードの選択を解除するときに呼ぶ
    /// </summary>
    public void Deselect()
    {
        transform.DOKill();

        transform.DOScale(normalScale, animationTime)
                 .SetEase(Ease.OutQuad);
    }
}
