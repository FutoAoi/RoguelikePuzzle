using DG.Tweening;
using NUnit.Framework;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public bool IsAttackTurn => _isAttackTurn;
    public bool IsDead => _isDead;

    [Header("エネミー詳細")]
    [SerializeField, Tooltip("エネミーの画像")] private Image _enemyImage;
    [SerializeField, Tooltip("攻撃ターンの表示")] private TextMeshProUGUI _attackTurnTMP;
    [SerializeField, Tooltip("エネミーのHP")] private int _enemyHP;
    [SerializeField, Tooltip("エネミーの攻撃力")] private int _enemyAP;
    [SerializeField, Tooltip("エネミーの攻撃までのターン数")] private int _enemyAT;

    private EnemyData _enemy;
    private int _id;
    private bool _isAttackTurn = false;
    private bool _isDead = false;

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
        _enemyImage.sprite = _enemy.Sprite;
        _attackTurnTMP.text = _enemyAT.ToString();
        if (enemyID == 0) _enemyImage.color = new Color(1f,1f,1f,0f);
        if (_enemyHP <= 0)
        {
            _isDead = true;
            StartCoroutine(Dead());
        }
    }
    /// <summary>
    /// エネミーに攻撃
    /// </summary>
    /// <param name="damage"></param>
    public void Damaged(int damage)
    {
        if (_isDead) return;
        _enemyHP -= damage;
        Debug.Log($"{damage}を与えた");
        if(_enemyHP <= 0)
        {
            _isDead = true;
            StartCoroutine(Dead());
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
        _enemyImage.DOFade(0f,0.1f);
        Debug.Log($"{name}を倒した！");
    }
}
