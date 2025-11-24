using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TileMovement : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    public Sprite CardSprite;
    private GameObject _dropTarget,_cardPrefab,_newCard;
    private Transform _trOriginalParent,_trHandArea;
    private Canvas _canvas;
    private CanvasGroup _canvasGroup;
    private RectTransform _rectTransform;
    private TileMovement _tileMovement;
    private TileSlot _tileSlot;
    private UIManager_Battle _uiManager;
    private bool _isBoardCard = false;

    private void Start()
    {
        _uiManager = FindAnyObjectByType<UIManager_Battle>();
        _rectTransform = GetComponent<RectTransform>();
        _canvas = GetComponentInParent<Canvas>();
        _canvasGroup = GetComponent<CanvasGroup>();
        if(_canvasGroup == null)
        {
            _canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
        _trHandArea = _uiManager.HandArea;
        _cardPrefab = _uiManager.CardPrefab;
    }
    /// <summary>
    /// タイルが置かれた
    /// </summary>
    public void SetAsBoardCard()
    {
        _isBoardCard = true;
        _trOriginalParent = transform.parent;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        _trOriginalParent = transform.parent;
        transform.SetParent(_uiManager.DragLayer.transform);
        _canvasGroup.blocksRaycasts = false;
        _canvasGroup.alpha = 0.6f;
    }
    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        _canvasGroup.alpha = 1f;
        _canvasGroup.blocksRaycasts = true;
        _dropTarget = eventData.pointerCurrentRaycast.gameObject;
        if (_dropTarget != null && _dropTarget.GetComponent<TileSlot>() != null)
        {
            _tileSlot = _dropTarget.GetComponent<TileSlot>();
            //カードが存在するとき元に戻す
            if (_tileSlot.IsOccupied)
            {
                transform.SetParent(_trOriginalParent);
                _rectTransform.anchoredPosition = Vector2.zero;
                return;
            }
            //盤面上から動かされてたらスロットを空に
            if(_isBoardCard && _trOriginalParent.GetComponent<TileSlot>() != null)
            {
                _trOriginalParent.GetComponent<TileSlot>().ClearSlot();
            }
            _tileSlot.PlaceCard(CardSprite);
            Destroy(gameObject,0.05f);
        }
        else
        {
            transform.SetParent(_trOriginalParent);
            _rectTransform.anchoredPosition = Vector2.zero;
        }
    }
    /// <summary>
    /// 右クリックで削除
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Right && _isBoardCard)
        {
            //親がスロットなら中身を空に
            if (_trOriginalParent != null && _trOriginalParent.GetComponent<TileSlot>() != null)
            {
                _trOriginalParent.GetComponent <TileSlot>().ClearSlot();
            }
            #region 手札にカードを生成
            _newCard = Instantiate(_cardPrefab,_trHandArea);
            _newCard.GetComponent<Image>().sprite = CardSprite;
            _tileMovement = _newCard.GetComponent<TileMovement>();
            if(_tileMovement == null)_tileMovement = _newCard.AddComponent<TileMovement>();
            _tileMovement.CardSprite = CardSprite;
            var cg = _newCard.GetComponent<CanvasGroup>();
            if (cg == null) cg = _newCard.AddComponent<CanvasGroup>();
            cg.blocksRaycasts = true;
            cg.alpha = 1f;
            var rt = _newCard.GetComponent<RectTransform>();
            rt.anchoredPosition = Vector2.zero;
            #endregion
            Destroy(gameObject,0.05f);
        }
    }
}
