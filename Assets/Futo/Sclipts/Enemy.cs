using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Image _enemyImage;
    [SerializeField] private int _enemyHP;
    [SerializeField] private int _enemyAP;
    [SerializeField] private int _enemyAT;

    private EnemyData _enemy;

    public void SetEnemyStatus(int enemyID)
    {
        _enemy = GameManager.Instance.EnemyDataBase.GetEnemyData(enemyID);
        _enemyHP = _enemy.EnemyHP;
        _enemyAP = _enemy.EnemyAP;
        _enemyAT = _enemy.EnemyAT;
        _enemyImage.sprite = _enemy.Sprite;
    }
    /// <summary>
    /// エネミーに攻撃
    /// </summary>
    /// <param name="damage"></param>
    public void Hit(int damage)
    {
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
        _enemyAT -= reductionTurn;
        Debug.Log($"攻撃まで残り{_enemyAT}ターン");
        if(_enemyAT <= 0 && _enemyHP > 0)
        {
            StartCoroutine(Attack());
            _enemyAT = _enemy.EnemyAT;
        }
    }
    private IEnumerator Attack()
    {
        yield return null;
        Debug.Log($"{name}の攻撃！");
        //攻撃する（Enum分けしてオブジェクトをだす＆攻撃力付与）

        GameManager.Instance.Reset = true;
    }
    private IEnumerator Dead()
    {
        yield return null;
        Debug.Log($"{name}を倒した！");
    }
}
