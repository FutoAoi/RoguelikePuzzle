using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// インゲームのバトル時のUIManager
/// </summary>
public class UIManager_Battle : UIManagerBase, IBattleUI
{
    [Header("カード")] 
    [Tooltip("手札")] public List<GameObject> HandCard = new List<GameObject>();
    [Tooltip("捨て札")] public List<int> DiscardCard = new List<int>();
    [Tooltip("除外札")] public List<int> RemoveCard = new List<int>();

    [Header("数値設定")]
    [SerializeField, Tooltip("手札の数")] private int _handRange = 5;
    [SerializeField, Tooltip("ドロー間隔")] private float _distance = 0.1f;
    [SerializeField, Tooltip("数字が増える演出時間")] private float _valueDuration = 0.2f;

    [Header("コンポーネント設定")]
    [SerializeField, Tooltip("場所")] private RectTransform _playerHandTr;
    [SerializeField, Tooltip("手札の場所")] public Transform HandArea;
    [SerializeField, Tooltip("カードの基盤")] public GameObject CardPrefab;
    [SerializeField, Tooltip("ドラッグ時の場所")] public RectTransform DragLayer;
    [SerializeField, Tooltip("効果説明パネル")] public RectTransform DescriptionArea;
    [SerializeField, Tooltip("カットインパネル")] private GameObject _enemyAttackPanel;
    [SerializeField, Tooltip("リザルトパネル")] private GameObject _resultPanel;
    [SerializeField, Tooltip("攻撃場所選択パネル")] private GameObject _attackPosPanel;
    [SerializeField, Tooltip("フェード用のパネル")] private Image _fadePanel;
    [SerializeField, Tooltip("消費コストテキスト")] private TextMeshProUGUI _costText;
    [SerializeField, Tooltip("最大コストテキスト")] private TextMeshProUGUI _maxCostText;

    public bool _isFinishCutIn = false;

    private PlayerStatus _status;
    private DeckManager _deckManager;
    private RewardManager _rewardManager;
    private GameObject _card;
    private TextMeshProUGUI _text;
    private Image _panelimg;
    private RectTransform _panelRectTr;
    private Color _defaultColor;
    private int _currentNumber;
    public override void InitUI()
    {
        _deckManager = DeckManager.Instance;
        GameManager.Instance.CurrentPhase = BattlePhase.BuildStage;
        HandCard.Clear();
        DiscardCard.Clear();
        RemoveCard.Clear();
        _text = _enemyAttackPanel.GetComponentInChildren<TextMeshProUGUI>();
        _panelimg = _enemyAttackPanel.GetComponent<Image>();
        _panelRectTr = _enemyAttackPanel.GetComponent<RectTransform>();
        _status = GameManager.Instance.PlayerStatus;
        _defaultColor = _panelimg.color;
        _enemyAttackPanel.SetActive(false);
        _fadePanel.gameObject.SetActive(false);
        _attackPosPanel.gameObject.SetActive(true);
    }
    public IEnumerator DrawCard()
    {
        for (int i = 0; i < _handRange; i++)
        {
            CreateCard();
            yield return new WaitForSeconds(_distance);
        }
        SetupCostText();
    }

    public void HandOrganize()
    {
        foreach (var tile in HandCard)
        {
            tile.transform.SetParent(_playerHandTr, false);
        }
        LayoutRebuilder.ForceRebuildLayoutImmediate(_playerHandTr);
    }

    public void ClearCard()
    {
        foreach(GameObject hand in HandCard)
        {
            Card card = hand.GetComponent<Card>();
        }
    }
    public void ResisterDiscardCard(int id)
    {
        DiscardCard.Add(id);
    }
    private void CreateCard()
    {
        _card = Instantiate(CardPrefab, _playerHandTr);
        Card card = _card.GetComponent<Card>();
        card.SetCard(_deckManager.DrawCard(), DescriptionArea);
        HandCard.Add(_card);
    }
    /// <summary>
    /// 敵攻撃時のカットイン
    /// </summary>
    /// <param name="duration"></param>
    public void CutInAnimation(float duration)
    {
        _enemyAttackPanel.SetActive(true);
        _panelRectTr.anchoredPosition = Vector2.right * 1500f;
        _defaultColor = _panelimg.color;
        _panelimg.color = new Color(_defaultColor.r, _defaultColor.g, _defaultColor.b, 0f);
        _panelRectTr.transform.localScale = Vector3.one;
        _text.alpha = 0f;
        _text.transform.localScale = Vector3.one * 0.8f;

        Sequence seq = DOTween.Sequence();
        seq.Append(_panelimg.DOFade(1f, duration * 0.12f)).SetEase(Ease.OutQuad);
        seq.Join(_panelRectTr.DOAnchorPos(Vector2.zero, duration * 0.38f).SetEase(Ease.OutExpo));
        seq.Join(_text.DOFade(1f, duration * 0.25f).SetEase(Ease.OutQuad));
        seq.Join(_text.transform.DOScale(1.25f, duration * 0.25f).SetEase(Ease.OutBack));
        seq.AppendInterval(duration * 0.05f);
        seq.Append(_text.transform.DOScale(1f, duration * 0.1f).SetEase(Ease.OutQuad));
        seq.Append(_panelRectTr.DOAnchorPos(Vector2.left * 2500f, duration * 0.2f).SetEase(Ease.InQuad));
        seq.Join(_panelimg.DOFade(0f, duration * 0.15f));
        seq.Join(_text.DOFade(0f, duration * 0.15f));
        seq.OnComplete(() =>
        {
            _isFinishCutIn = true;
            _enemyAttackPanel.SetActive(false);
        });
    }
    /// <summary>
    /// 報酬画面の表示
    /// </summary>
    public void DisplayReward()
    {
        _fadePanel.gameObject.SetActive(true);

        Sequence seq = DOTween.Sequence();
        seq.AppendInterval(0.3f)
            .Append(_fadePanel.DOFade(1f, 0.4f).SetEase(Ease.OutQuad))
            .AppendCallback(() =>
            {
                _resultPanel.SetActive(true);
                _rewardManager = _resultPanel.GetComponent<RewardManager>();
                _rewardManager.Reward();
            })
            .AppendInterval(0.2f)
            .Append(_fadePanel.DOFade(0f, 0.4f))
            .OnComplete(() =>
            {
                _fadePanel.gameObject.SetActive(false);
                StartCoroutine(_rewardManager.RewardAnimation());
            });
    }
    /// <summary>
    /// コスト表記の初期化
    /// </summary>
    public void SetupCostText()
    {
        _currentNumber = _status.MaxCost;
        _costText.text = _status.MaxCost.ToString();
        _maxCostText.text = _status.MaxCost.ToString();
    }
    /// <summary>
    /// コストテキストの更新
    /// </summary>
    /// <param name="targetValue"></param>
    public void UpdateCostText(int targetValue)
    {
        DOTween.To(() => _currentNumber, 
            x =>
            {
                _currentNumber = x;
                _costText.text = _currentNumber.ToString();
            },
            targetValue,
            _valueDuration
        );
    }
}
