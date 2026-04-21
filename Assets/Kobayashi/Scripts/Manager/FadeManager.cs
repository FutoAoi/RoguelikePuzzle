using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour
{
    public static FadeManager Instance { get; private set; }

    [Header("-----参照-----")]
    [Header("フェードパネル"), SerializeField] private Image _panel;
    [Header("-----演出設定-----")]
    [Header("演出時間"),SerializeField] private float _fadeTime = 0.3f;

    private Canvas _canvas;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        _canvas = GetComponent<Canvas>();
        _canvas.sortingOrder = 9999;
        DontDestroyOnLoad(gameObject);
    }
    /// <summary>
    /// フェードIN/OUTさせる
    /// </summary>
    /// <param name="isFadeIN">フェードインかどうか</param>
    /// <param name="duration">演出時間</param>
    /// <param name="ease">イージング</param>
    public void FadePanel(bool isFadeIN,float duration = 0f,Ease ease = Ease.Unset)
    {
        if (duration == 0f) duration = _fadeTime;
        float to = isFadeIN? 0f : 1f;
        _panel.DOFade(to,duration).SetEase(ease);
    }
}
