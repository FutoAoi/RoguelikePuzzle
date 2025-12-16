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
    [SerializeField, Tooltip("回転時間")] private float _duration = 0.8f;
    [SerializeField, Tooltip("カード前面")] private GameObject _cardFrontObj;
    [SerializeField, Tooltip("カードの裏面")] private GameObject _cardBackObj;

    public int CardID => _cardID;
    public bool IsFinish { get; private set; } = false;

    private CardData _data;
    private Vector3 normalScale;
    private Transform _tr;
    private float selectedScale = 1.1f;
    private float animationTime = 0.2f;
    private void Awake()
    {
        _tr = transform;
        _cardFrontObj.SetActive(false);
        _cardBackObj.SetActive(true);
    }

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
        normalScale = _tr.localScale;
    }

    public void Select()
    {
        _tr.DOKill();

        _tr.DOScale(normalScale * selectedScale, animationTime)
                 .SetEase(Ease.OutBack);
    }

    /// <summary>
    /// カードの選択を解除するときに呼ぶ
    /// </summary>
    public void Deselect()
    {
        _tr.DOKill();

        _tr.DOScale(normalScale, animationTime)
                 .SetEase(Ease.OutQuad);
    }
    /// <summary>
    /// カードをめくるアニメーション
    /// </summary>
    public void TurnCardAnimation()
    {
        Sequence seq = DOTween.Sequence();

        seq.Append(_tr.DORotate(Vector2.zero, _duration));
        seq.InsertCallback(_duration * 0.5f, () =>
        {
            _cardBackObj.SetActive(false);
            _cardFrontObj.SetActive(true);
        });
        seq.OnComplete(() =>
        {
            IsFinish = true;
        });
        
    }
}
