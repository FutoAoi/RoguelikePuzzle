using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TileMovement : MonoBehaviour
{
    public Sprite CardSprite;
    private GameObject _dropTarget;
    private Transform _trOriginalParent;
    private Canvas _canvas;
    private CanvasGroup _canvasGroup;
    private RectTransform _rectTransform;

    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _canvas = GetComponentInParent<Canvas>();
        _canvasGroup = GetComponent<CanvasGroup>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        _trOriginalParent = transform.parent;
        transform.SetParent(_canvas.transform);
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
        _dropTarget = eventData.pointerEnter;
        if(_dropTarget != null && _dropTarget.GetComponent<TileSlot>() != null)
        {
            _dropTarget.GetComponent<TileSlot>().PlaceCard(CardSprite);
            Destroy(gameObject);
        }
        else
        {
            transform.SetParent(_trOriginalParent);
            _rectTransform.anchoredPosition = Vector2.zero;
        }
    }
}
