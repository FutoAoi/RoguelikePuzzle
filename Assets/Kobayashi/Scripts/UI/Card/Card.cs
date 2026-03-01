using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Card : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    [Header("カードの見た目")]
    [SerializeField, Tooltip("見た目の親")] private RectTransform _view;
    [SerializeField, Tooltip("挿絵")] private Image _cardImage;
    [SerializeField, Tooltip("カード名")] private TextMeshProUGUI _nameText;
    [SerializeField, Tooltip("コスト表示")]private TextMeshProUGUI _costText;

    [Header("効果表示パネル")]
    [SerializeField, Tooltip("パネル")] private RectTransform _panel;
    [SerializeField, Tooltip("表示画像")] private Image _image;
    [SerializeField, Tooltip("効果テキスト")] private TextMeshProUGUI _effectText;

    [Header("数値設定")]
    [SerializeField,Tooltip("表示アニメーション時間")] private float _duration = 0.2f;
    [SerializeField, Tooltip("非表示アニメーション時間")] private float _hideSpeed = 0.1f;

    public int CardID;

    private Tween _activeTween;
    private CardDataBase _cardData;
    private Vector2 _defaultScale,_targetScale;
    private int _cardCost;
    private bool _ignorePointer = false;
    private float _displayTime;
    private string _description;
    public void SetCard(int id,RectTransform descriptionArea)
    {
        _cardData = GameManager.Instance.CardDataBase;
        CardID = id;
        _cardImage.sprite = _cardData.GetCardData(CardID).Sprite;
        _nameText.text = _cardData.GetCardData(CardID).Name;
        _cardCost = _cardData.GetCardData(CardID).Cost;
        _costText.text = _cardCost.ToString();
        _description = _cardData.GetCardData(CardID).Description;

        _panel.SetParent(descriptionArea,false);
        _defaultScale = _panel.localScale;
        _panel.localScale = Vector2.zero;
        _image.sprite = _cardData.GetCardData(CardID).Sprite;
        _effectText.text = _description;

        #region ドローアニメーション
        _view.localScale = Vector3.zero;
        _view.anchoredPosition = new Vector2(0, -200);
        _view.DOScale(1f, 0.25f).SetEase(Ease.OutBack);
        _view.DOAnchorPos(Vector2.zero, 0.25f).SetEase(Ease.OutCubic);
        #endregion
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        DisplayPanel(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        DisplayPanel(false);
    }
    /// <summary>
    /// 効果説明パネルの表示/非表示
    /// </summary>
    /// <param name="show"></param>
    public void DisplayPanel(bool show)
    {
        if (_ignorePointer) return;

        if (show)
        {
            _targetScale = _defaultScale;
            _displayTime = _duration;
        }
        else
        {
            _targetScale = Vector2.zero;
            _displayTime = _hideSpeed;
        }

        _activeTween?.Kill();
        _activeTween = _panel.DOScale(_targetScale, _displayTime)
            .SetEase(Ease.OutBack);
    }
    /// <summary>
    /// カーソルが一定時間重なってるかフラグ
    /// </summary>
    /// <param name="time"></param>
    public void IgnorePointerFor(float time)
    {
        _ignorePointer = true;
        Invoke(nameof(EnablePointer), time);
    }

    private void EnablePointer()
    {
        _ignorePointer = false;
    }
}
