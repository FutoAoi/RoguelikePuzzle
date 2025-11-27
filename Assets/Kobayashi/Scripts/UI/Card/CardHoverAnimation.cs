using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
/// <summary>
/// カードにカーソル重なったときの拡大縮小アニメーション
/// </summary>
public class CardHoverAnimation : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    [Header("数値設定")]
    [SerializeField, Tooltip("効果時間")] private float _duration = 0.2f;
    [SerializeField, Tooltip("拡大比率")] private float _magnificationRatio = 1.2f;
    [SerializeField, Tooltip("上昇量")] private float _upper = 50f;

    private RectTransform _rect;
    private Tweener _scaleTween,_rectTween;
    private Vector3 _defaultScale;
    private void Awake()
    {
        _rect = transform.Find("View").GetComponent<RectTransform>();
        _defaultScale = _rect.localScale;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        _scaleTween?.Kill();
        _scaleTween = _rect.DOScale(_defaultScale * _magnificationRatio, _duration)
            .SetEase(Ease.OutBack);
        _rectTween?.Kill();
        _rectTween = _rect.DOAnchorPos(new Vector2(0, _upper), _duration)
            .SetEase(Ease.OutBack);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        _scaleTween?.Kill();
        _scaleTween = _rect.DOScale(_defaultScale,_duration)
            .SetEase(Ease.OutBack);
        _rectTween?.Kill();
        _rectTween = _rect.DOAnchorPos(Vector2.zero, _duration)
            .SetEase(Ease.OutBack);
    }
}
