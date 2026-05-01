using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : CharacterBase
{
    public bool IsAttackTurn => _isAttackTurn;
    public bool IsSpecialAttack => _isSpecialAttack;
    public bool IsDead => _isDead;

    [Header("エネミー詳細")]
    [SerializeField, Tooltip("エネミーの画像")] private Image _enemyImage;
    [SerializeField, Tooltip("攻撃ターンの表示")] private TextMeshProUGUI _attackTurnTMP;
    [SerializeField, Tooltip("特殊攻撃ターンの表示")] private TextMeshProUGUI _specialTMP;
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
    private bool _isSpecialAttack = false;
    private bool _isDead = false;
    private RectTransform _rect;
    private int _currentHp = 0;
    private int _currentSAT;
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
        if (_enemy.IsSpecialAttack)
        {
            _currentSAT = _enemy.EnemySAT;
            _specialTMP.text = _currentSAT.ToString();
        }
        else
        {
            _specialTMP.text = null;
        }
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
            _specialTMP.text = null;
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
        if (_enemy.IsSpecialAttack)
        {
            _currentSAT -= reductionTurn;
            _specialTMP.text = _currentSAT.ToString();
            
            if(_currentSAT <= 0)
            {
                _isSpecialAttack = true;
                _currentSAT = _enemy.EnemySAT;
            }
        }
        if(_enemyAT <= 0)
        {
            _isAttackTurn = true;
            _enemyAT = _enemy.EnemyAT;
        }
    }

    /// <summary>
    /// バフ付与
    /// </summary>
    /// <returns></returns>
    public IEnumerator BuffCast()
    {
        if (_enemy.CanBuff && !_isDead)
        {
            foreach (IBuff buff in _enemy.Buffs)
            {
                buff.Excute(this);
                yield return null;
            }
        }
        _gameManager.AttackManager.FinishEnemySpecialAttack(true);
    }
    /// <summary>
    /// 盤面干渉攻撃
    /// </summary>
    /// <returns></returns>
    public IEnumerator SpecialAttack()
    {
        if (_enemy.CanBoardInterference && !_isDead)
        {
            StageData stageData = _gameManager.StageManager.Stage;

            //空のSlotのリスト作成
            List<TileSlot> emptyTiles = new();
            for (int i = 0; i < stageData.Height; i++)
            {
                for (int j = 0; j < stageData.Width; j++)
                {
                    if (_gameManager.StageManager.SlotList[i][j].
                        TryGetComponent<TileSlot>(out var tile))
                    {
                        if (!tile.IsOccupied)
                            emptyTiles.Add(tile);
                    }
                }
            }

            //空Slotリストの中から抽選
            foreach (CardData card in _enemy.CardEffects)
            {
                if (emptyTiles.Count == 0)
                {
                    Debug.LogWarning("空きマスが足りませんでした");
                    break;
                }

                int index = Random.Range(0, emptyTiles.Count);
                emptyTiles[index].PlaceCard(card.CardID);
                emptyTiles[index].IsLastTimeCard = true;
                emptyTiles.RemoveAt(index);

                yield return null;
            }
        }
        
        _gameManager.AttackManager.FinishEnemySpecialAttack(false);
    }

    /// <summary>
    /// 攻撃終了
    /// </summary>
    public void FinishAttack()
    {
        _isAttackTurn = false;
        _isSpecialAttack = false;
        if(_isDead)return ;
        _attackTurnTMP.text = _enemyAT.ToString();
        if(_enemy.IsSpecialAttack)
            _specialTMP.text = _currentSAT.ToString();
    }

    /// <summary>
    /// 死亡コルーチン
    /// </summary>
    /// <returns></returns>
    private IEnumerator Dead()
    {
        yield return null;
        _attackTurnTMP.text = null;
        _specialTMP.text = null;
        _enemyHpText.text = null;
        _backGround.SetActive(false);
        _enemyImage.DOFade(0f,0.1f);

        _gameManager.PlayerStatus.GetMoney(_enemy.RandomReword());
    }
}
