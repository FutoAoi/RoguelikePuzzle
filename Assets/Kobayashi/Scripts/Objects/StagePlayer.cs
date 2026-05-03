using System.Collections;
using UnityEngine;

public class StagePlayer : CharacterBase
{
    public int MaxCost => _maxCost;
    public int CurrentCost => _currentCost;
    public int Money => _money;
    private int _money = 0;

    [SerializeField] private int _maxCost = 8;
    private int _currentCost;

    protected override void Start()
    {
        base.Start();

        _gameManager.Player = this;
        Debug.Log("Player" + $"{CurrentHP}");
    }

    public override void Damaged(int damage)
    {
        base.Damaged(damage);
    }

    /// <summary>
    /// コスト上限値の上昇
    /// </summary>
    /// <param name="plus"></param>
    public void IncreaseMaxCost(int plus)
    {
        _maxCost += plus;
    }
    /// <summary>
    /// コストの初期化
    /// </summary>
    public void SetCost()
    {
        _currentCost = _maxCost;
    }
    /// <summary>
    /// コスト消費できるかどうか
    /// </summary>
    /// <param name="cost"></param>
    /// <returns></returns>
    public bool ConsumeCost(int cost)
    {
        return _currentCost >= cost;
    }
    /// <summary>
    /// コストの更新
    /// </summary>
    /// <param name="cost"></param>
    /// <param name="isConsume"></param>
    public void ChangeCost(int cost, bool isConsume)
    {
        if (isConsume)
        {
            _currentCost -= cost;
        }
        else
        {
            _currentCost += cost;
        }
    }
    /// <summary>
    /// お金を支払う
    /// </summary>
    /// <param name="pay"></param>
    public void PayMoney(int pay)
    {

        if (_money - pay <= 0)
        {
            Debug.Log("お金が足りないよ！！！");
            return;
        }
        _money -= pay;
    }
    /// <summary>
    /// お金を手に入れる
    /// </summary>
    /// <param name="plus"></param>
    public void GetMoney(int plus)
    {
        _money += plus;
    }

    public override void Dead()
    {
        GameManager.Instance.CurrentUIManager.GetComponent<UIManager_Battle>().DisplayGameOverPanel();
    }
}
