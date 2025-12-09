using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Image _enemyImage;
    [SerializeField] private int _enemyHP;
    [SerializeField] private int _enemyAP;
    [SerializeField] private int _enemyAT;

    private int _currentEnemyHP;

    private EnemyData _enemy;


    public void SetEnemyStatus(int enemyID)
    {
        _enemy = GameManager.Instance.EnemyDataBase.GetEnemyData(enemyID);
        _enemyHP = _enemy.EnemyHP;
        _enemyAP = _enemy.EnemyAP;
        _enemyAT = _enemy.EnemyAT;
        _enemyImage.sprite = _enemy.Sprite;
    }

    public void Hit(int damage)
    {
        _enemyHP -= damage;
        Debug.Log($"{damage}‚ð—^‚¦‚½");
    }
}
