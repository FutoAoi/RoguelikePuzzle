using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : CharacterBase
{
    public bool IsAttackTurn => _isAttackTurn;
    public bool IsDead => _isDead;

    [Header("エネミー詳細")]
    [SerializeField, Tooltip("エネミーの画像")] private Image _enemyImage;
    [SerializeField, Tooltip("攻撃ターンの表示")] private TextMeshProUGUI _attackTurnTMP;
    [SerializeField, Tooltip("エネミーのHP")] private int _enemyHP;
    [SerializeField, Tooltip("エネミーの攻撃力")] private int _enemyAP;
    [SerializeField, Tooltip("エネミーの攻撃までのターン数")] private int _enemyAT;

    [Header("HPアニメーション設定")]
    [SerializeField, Tooltip("背景イメージ")] private GameObject _backGround;
    [SerializeField, Tooltip("メインバー")] private Image _mainBar;
    [SerializeField, Tooltip("ゴーストバー")] private Image _ghostBar;
    [SerializeField, Tooltip("メインバーが減るスピード")] private float _mainSpeed = 0.2f;
    [SerializeField, Tooltip("ゴーストバーが動くまでの時間")] private float _ghostDelay = 0.5f;
    [SerializeField, Tooltip("ゴーストバーが動くスピード")] private float _ghostSpeed = 0.5f;
    [SerializeField, Tooltip("エネミーのHPテキスト")] private TMP_Text _enemyHpText;

    private EnemyData _enemy;
    private int _id;
    private bool _isAttackTurn = false;
    private bool _isDead = false;
    private RectTransform _rect;
    private int _currentHp = 0;
    private float _ghostHp;
    private float _hpRatio;
    private Tween _ghostTween;

    /// <summary>
    /// エネミーにステータスをセット
    /// </summary>
    /// <param name="enemyID"></param>
    public void SetEnemyStatus(int enemyID)
    {
        _enemy = GameManager.Instance.EnemyDataBase.GetEnemyData(enemyID);
        _enemyHP = _enemy.EnemyHP;
        _enemyAP = _enemy.EnemyAP;
        _enemyAT = _enemy.EnemyAT;
        _currentHp = _enemyHP;
        _rect = GetComponent<RectTransform>();
        _enemyImage.sprite = _enemy.Sprite;
        _attackTurnTMP.text = _enemyAT.ToString();
        _hpRatio = (float)_currentHp / _enemyHP;
        _mainBar.fillAmount = _hpRatio;
        _ghostBar.fillAmount = _hpRatio;
        _enemyHpText.text = $"{_currentHp}/{_enemyHP}";
        if (enemyID == 0)
        {
            _enemyImage.color = new Color(1f,1f,1f,0f);
            _attackTurnTMP.text = null;
            _enemyHpText.text = null;
            _isDead = true;
            _backGround.SetActive(false);
        }
    }
    /// <summary>
    /// エネミーに攻撃
    /// </summary>
    /// <param name="damage"></param>
    public override void Damaged(int damage)
    {
        if (_isDead) return;
        _currentHp -= damage;
        _currentHp = Mathf.Max(0, _currentHp);
        _hpRatio = (float)_currentHp / _enemyHP;
        DamagePopUpObjectPool.Instance.Get(_rect.anchoredPosition + new Vector2(Random.Range(-50f, 50f), 0f), damage);
        _enemyHpText.text = $"{_currentHp}/{_enemyHP}";
        _mainBar.DOFillAmount(_hpRatio, _mainSpeed);
        if(_ghostTween != null && _ghostTween.IsActive())
        {
            _ghostTween.Kill();
        }
        _ghostTween = _ghostBar.DOFillAmount(_hpRatio, _ghostSpeed).SetDelay(_ghostDelay);
        Debug.Log($"{damage}を与えた");
        if(_currentHp <= 0)
        {
            _isDead = true;
            _mainBar.DOFillAmount(0f, _mainSpeed).OnComplete(() => StartCoroutine(Dead()));
        }
    }

    /// <summary>
    /// 攻撃ターンを縮める
    /// </summary>
    /// <param name="reductionTurn"></param>
    public void ContractionAttackTurn(int reductionTurn)
    {
        if (_isDead)return;
        _enemyAT -= reductionTurn;
        _attackTurnTMP.text = _enemyAT.ToString();
        Debug.Log($"{name}の攻撃まで残り{_enemyAT}ターン");
        if(_enemyAT <= 0)
        {
            _isAttackTurn = true;
            _enemyAT = _enemy.EnemyAT;
        }
    }

    /// <summary>
    /// 攻撃終了
    /// </summary>
    public void FinishAttack()
    {
        _isAttackTurn = false;
        if(_isDead)return ;
        _attackTurnTMP.text = _enemyAT.ToString();
    }

    /// <summary>
    /// 死亡コルーチン
    /// </summary>
    /// <returns></returns>
    private IEnumerator Dead()
    {
        yield return null;
        _attackTurnTMP.text = null;
        _enemyHpText.text = null;
        _backGround.SetActive(false);
        _enemyImage.DOFade(0f,0.1f);

        GameManager.Instance.PlayerStatus.GetMoney(_enemy.RandomReword());

        Debug.Log($"{name}を倒した！");
    }
}
