using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
/// <summary>
/// カードにカーソル重なったときの拡大縮小アニメーション
/// </summary>
public class CardHoverAnimation : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    [Header("数値設定")]
    [SerializeField, Tooltip("効果時間")] private float _duration = 0.2f;
    [SerializeField, Tooltip("拡大比率")] private float _magnificationRatio = 1.2f;
    private RectTransform _rect;
    private Tweener _tween;
    private Vector3 _defaultScale;
    private void Awake()
    {
        _rect = GetComponent<RectTransform>();
        _defaultScale = _rect.localScale;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        _tween?.Kill();
        _tween = _rect.DOScale(_defaultScale * _magnificationRatio, _duration)
            .SetEase(Ease.OutBack);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        _tween?.Kill();
        _tween = _rect.DOScale(_defaultScale,_duration)
            .SetEase(Ease.OutBack);
    }
}
