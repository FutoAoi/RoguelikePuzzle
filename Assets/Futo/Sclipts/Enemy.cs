using NUnit.Framework;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Image _enemyImage;
    [SerializeField] private TextMeshProUGUI _attackTurnTMP;
    [SerializeField] private int _enemyHP;
    [SerializeField] private int _enemyAP;
    [SerializeField] private int _enemyAT;

    private bool _isAttackTurn = false;
    public bool IsAttackTurn => _isAttackTurn;

    private EnemyData _enemy;
    private bool _isDead = false;
    private int _id;
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
        if (_enemyHP <= 0) StartCoroutine(Dead());
    }
    /// <summary>
    /// エネミーに攻撃
    /// </summary>
    /// <param name="damage"></param>
    public void Hit(int damage)
    {
        if (_isDead) return;
        _enemyHP -= damage;
        Debug.Log($"{damage}を与えた");
        if(_enemyHP <= 0)
        {
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
        _attackTurnTMP.text = _enemyAT.ToString();
        _isAttackTurn = false;
    }
    private IEnumerator Dead()
    {
        yield return null;
        _attackTurnTMP.text = null;
        Debug.Log($"{name}を倒した！");
        _isDead = true;
    }
}
