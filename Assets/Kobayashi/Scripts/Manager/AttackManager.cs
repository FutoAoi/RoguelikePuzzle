using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AttackManager : MonoBehaviour
{
    [Header("コンポーネント設定")]
    [SerializeField] private StageManager _stageManager;
    [SerializeField] private UIManager_Battle _uiManager;
    [SerializeField] private RectTransform _playerPos;

    [Header("数値設定")]
    [SerializeField, Tooltip("タイル間の移動時間")] private float _interval = 2.0f;
    [SerializeField, Tooltip("敵の攻撃時カットインアニメーション時間")] private float _duration = 3f;

    private GameManager _gameManager;
    private MagicObjectPool _magicPool;
    private AttackMagic _magic;
    private int _height, _width;
    private bool _isFinishEnemyAttack = false,_isVictory = false;
    private Vector2Int _enemyPos;
    private RectTransform _enemyRectTr;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _gameManager = GameManager.Instance;
        _gameManager.AttackManager = this;
        _magicPool = MagicObjectPool.Instance;
        _height = _gameManager.StageDataBase.GetStageData(_gameManager.StageID).Height;
        _width = _gameManager.StageDataBase.GetStageData(_gameManager.StageID).Width;
    }
    /// <summary>
    /// 攻撃指示
    /// </summary>
    /// <param name="isPlayer"></param>
    public void AttackTurn(bool isPlayer)
    {
        Debug.Log("攻撃開始！！！");
        _magic = _magicPool.GetAttackMagic();
        _magic.gameObject.SetActive(true);
        if (isPlayer)
        {
            StartCoroutine(_magic.Attack(new Vector2Int(_height / 2, 0),
                MagicVector.Right, _playerPos, _interval, true));
        }
        else
        {
            StartCoroutine(_magic.Attack(_enemyPos, 
                MagicVector.Left, _enemyRectTr, _interval, false));
        }
    }
    /// <summary>
    /// 攻撃終了時にフラグ出す
    /// </summary>
    /// <param name="isPlayer"></param>
    public void AttackFinish(bool isPlayer)
    {
        if (isPlayer)
        {
            _gameManager.IsEnemyAction = true;
        }
        else
        {
            _isFinishEnemyAttack = true;
        }
    }
    /// <summary>
    /// 敵の攻撃ターン
    /// </summary>
    /// <returns></returns>
    public IEnumerator EnemyTurn()
    {
        if (CheckEnemy())
        {
            _gameManager.CurrentPhase = BattlePhase.Reward;
            _isVictory = true;
        }

        if (!_isVictory)
        {
            _uiManager.CutInAnimation(_duration);
            yield return new WaitUntil(() => _uiManager._isFinishCutIn);
            _uiManager._isFinishCutIn = false;
            int count = 0;
            foreach (Enemy enemy in _stageManager.EnemyList)
            {
                enemy.ContractionAttackTurn(1);
                if (enemy.IsAttackTurn)
                {
                    _enemyPos = new Vector2Int(count, _width - 1);
                    _enemyRectTr = enemy.GetComponent<RectTransform>();
                    AttackTurn(false);
                    yield return new WaitUntil(() => _isFinishEnemyAttack);
                    enemy.FinishAttack();
                    _isFinishEnemyAttack = false;
                }
                count++;
            }
            if (CheckEnemy())
            {
                _gameManager.CurrentPhase = BattlePhase.Reward;
                _isVictory = true;
            }
            else
            {
                _gameManager.Reset = true;
            }
        }
        
    }
    private bool CheckEnemy()
    {
        foreach (Enemy enemy in _stageManager.EnemyList)
        {
            if (!enemy.IsDead) return false;
        }
        return true;
    }
}